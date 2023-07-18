using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText ;
    public TextMeshProUGUI personNameText ;

    private int sentenceIndex =-1 ;
    private StoryScene currentScene ;
    private State state =State.COMPLETED ;
    private Animator animator ;
    private bool isHidden =false ;

    public Dictionary<Speaker ,SpriteController> sprites ;
    public GameObject spritesPrefab ;

    private Coroutine typingCoroutine ;
    private float speedFactor =1f ;

    private enum State
    {
        PLAYING ,SPEEDED_UP ,COMPLETED 
    }

    private void Start()
    {
        sprites =new Dictionary<Speaker ,SpriteController>() ;
        animator =GetComponent<Animator>() ;
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

    public void GoBack()
    {
        sentenceIndex-- ;
        StopTyping() ;
        HideSprites() ;
        PlaySentence(false) ;
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
        speedFactor =1f ;
        typingCoroutine =StartCoroutine(TypeText(currentScene.sentences[sentenceIndex].text)) ;
        personNameText.text =currentScene.sentences[sentenceIndex].speaker.speakerName ;
        personNameText.color =currentScene.sentences[sentenceIndex].speaker.textColor ;
        ActSpeakers(isAnimated) ;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text ="" ;
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
}
