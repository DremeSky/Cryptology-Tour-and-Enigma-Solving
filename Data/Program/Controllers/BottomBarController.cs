using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;

public class BottomBarController : MonoBehaviour
{
    //外部數據 (public)
        public TextMeshProUGUI barText ;
        public TextMeshProUGUI personNameText ;
        public Dictionary<Speaker ,SpriteController> sprites ;
        public GameObject spritesPrefab ;


    //內部資料 (private) {測試用時，會打開成public查看數據}
        private int sentenceIndex =-1 ;
        private StoryScene currentScene ;
        private enum State
        {
            PLAYING ,SPEEDED_UP ,COMPLETED 
        }
        private State state =State.COMPLETED ;
        private Animator animator ;
        private bool isHidden =false ;
        private Coroutine typingCoroutine ;
        private float speedFactor =1f ;
        private bool open_windows = false;  //Shift、右鍵 : 對話關閉
        private string bottomBar_Text = ""; //用來回傳給GameController做對話紀錄用
    

    private void Start()
    {
        //變數重製
        SetData();
    }


    private void SetData()
    {
        //取得物件
        sprites =new Dictionary<Speaker ,SpriteController>() ;
        animator =GetComponent<Animator>() ;
        
        //變數重製
        open_windows = false;
        bottomBar_Text = "";
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex ;
    }

    public void SetSentenceIndex(int sentenceIndex)
    {
        this.sentenceIndex =sentenceIndex ;
    }

    public void Hide()
    {
        if(!isHidden)
        {
            animator.SetTrigger("Hide") ;
            isHidden =true ;
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show") ;
        isHidden =false ;
    }

    public void ClearText()
    {
        barText.text ="" ;
        personNameText.text ="" ;
    }

    public void PlayScene(StoryScene scene ,int sentenceIndex =-1 ,bool isAnimated =true)
    {
        currentScene =scene ;
        this.sentenceIndex =sentenceIndex ;
        PlayNextSentence(isAnimated) ;
    }

    public void PlayNextSentence(bool isAnimated =true)
    {
        sentenceIndex++ ;
        PlaySentence(isAnimated) ;
    }

    public bool IsCompleted()
    {
        return state==State.COMPLETED || state==State.SPEEDED_UP ;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex +1==currentScene.sentences.Count ;
    }

    public bool IsFirstSentence()
    {
        return sentenceIndex==0 ;
    }

    public void SpeedUp()
    {
        state =State.SPEEDED_UP ;
        speedFactor =0.35f ;
    }

    public void StopTyping()
    {
        state =State.COMPLETED ;
        StopCoroutine(typingCoroutine) ;
    }

    public void HideSprites()
    {
        while(spritesPrefab.transform.childCount >0)
        {
            DestroyImmediate(spritesPrefab.transform.GetChild(0).gameObject) ;
        }
        sprites.Clear() ;
    }

    private void PlaySentence(bool isAnimated =true)
    {
        //對話紀錄用 {GameController}
        if(currentScene.sentences[sentenceIndex].speaker.speakerName != "")
        {
            bottomBar_Text = currentScene.sentences[sentenceIndex].speaker.speakerName + " : "
                        + currentScene.sentences[sentenceIndex].text;
        }
        else
        {
            bottomBar_Text = currentScene.sentences[sentenceIndex].text;
        }

        speedFactor =1f ;
        typingCoroutine =StartCoroutine(TypeText(currentScene.sentences[sentenceIndex].text)) ;
        personNameText.text =currentScene.sentences[sentenceIndex].speaker.speakerName ;
        personNameText.color =currentScene.sentences[sentenceIndex].speaker.textColor ;
        ActSpeakers(isAnimated) ;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "" ;
        state =State.PLAYING ;
        int wordIndex =0 ;

        while(state!=State.COMPLETED)
        {
            barText.text+=text[wordIndex] ;
            yield return new WaitForSeconds(speedFactor *0.07f) ;
            if(++wordIndex ==text.Length)
            {
                state =State.COMPLETED ;
                break ;
            }
        }
    }

    private void ActSpeakers(bool isAnimated =true)
    {
        List<StoryScene.Sentence.Action> actions =currentScene.sentences[sentenceIndex].actions ;
        for(int i =0 ;i <actions.Count ;i++)
        {
            ActSpeaker(actions[i] ,isAnimated) ;
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action ,bool isAnimated =true)
    {
        SpriteController controller ;
        if(!sprites.ContainsKey(action.speaker))
        {
            controller =Instantiate(action.speaker.prefab.gameObject ,spritesPrefab.transform)
                .GetComponent<SpriteController>() ;
            sprites.Add(action.speaker ,controller) ;
        }
        else
        {
            controller =sprites[action.speaker] ;
        }
        switch(action.actionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                controller.Setup(action.speaker.sprites[action.spriteIndex]) ;
                controller.Show(action.coords ,isAnimated) ;
                return ;
            case StoryScene.Sentence.Action.Type.MOVE:
                controller.Move(action.coords ,action.moveSpeed ,isAnimated) ;
                break ;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                controller.Hide(isAnimated) ;
                break ;
        }
        controller.SwitchSprite(action.speaker.sprites[action.spriteIndex] ,isAnimated) ;
    }


    //Shift、右鍵 : 對話關閉
    public void Button_UnDemo()
    {
        if(!open_windows)
        {
            animator.SetTrigger("UnDemo") ;
            open_windows = true;
        }
    }
    public void Button_Demo()
    {
        if(open_windows)
        {
            animator.SetTrigger("Demo") ;
            open_windows = false;
        }
    }

    //從bottomBar回傳bottomBar_Text給GameController，做對話紀錄用(Q、滑鼠中鍵)。
    public string return_BottomBarText()
    {
        return bottomBar_Text;
    }
}