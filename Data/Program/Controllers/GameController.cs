using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement ;
using System ;
using TMPro ;

public class GameController : MonoBehaviour
{
    //外部數據 (public)
        public GameScene currentScene ;
        public BottomBarController bottomBar ;
        public SpriteSwitcher backgroundController ;
        public ChooseController chooseController ;
        public AudioController audioController ;
        public DataHolder data ;
        public string menuScene ;
        public LevelState levelState ;
        public CSVManager csvManager;   //用來更新 Menu 的 Load狀態 -> Game1(遊戲剛開始)的部分
        public Animator animator_TextRecord;    //給對話紀錄用的
        public TextMeshProUGUI text_TextRecord; //給對話紀錄用的
        public GameObject targets_Play1;    //控制ButtonPlay的開關狀態
        public GameObject targets_Play2;    //控制ButtonPlay的開關狀態
        public GameObject targets_TextRecord;    //控制TextRecord的開關狀態


    //內部資料 (private) {測試用時，會打開成public查看數據}
        private enum State
        {
            IDLE ,ANIMATE ,CHOOSE
        }
        private State state =State.IDLE ;
        private List<StoryScene> history =new List<StoryScene>() ;
        private bool open_BottomBarDemo = true;
        private bool open_A = false;     //鍵盤A的自動撥放的開關
        private bool open_Auto = false;     //自動撥放的開關 (鍵盤A)
        private bool open_ToLevel = false;     //鍵盤左Alt關卡快轉的開關
        private List<string> list_StoryText;    //用以記載文字的對話紀錄(鍵盤Q、滑鼠中鍵)
        private bool open_TextRecord = false;   //給對話紀錄用的
        private bool open_LoadValue = false;    //Menu -> Load
    

    public void Start()
    {
        //變數重製
        SetData();
        
        //偵測切換到Game的來源場景，，撥放對應劇情。
        currentScene = levelState.Determine_LevelState();
        
        //如果現在場景為劇情故事，則進行對應設定。
        if(currentScene is StoryScene)
        {
            StoryScene storyScene =currentScene as StoryScene ;
            history.Add(storyScene) ;
            bottomBar.PlayScene(storyScene ,bottomBar.GetSentenceIndex()) ;
            backgroundController.SetImage(storyScene.background) ;
            PlayAudio(storyScene.sentences[bottomBar.GetSentenceIndex()]) ;
        }
    }
    public void Update()
    {
        //存檔開啟  {Menu -> Load用(Game的部分)}
        OpenLoadValue();

        //開始處裡Game
        if(state==State.IDLE)
        {
            //如果劇情章節，或者這句台詞設定為自動撥放(autoPlay = true)，則自動撥放。
            if((currentScene as StoryScene).autoPlay==true 
                    || (currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()].autoPlay==true)
            {
                //如果自動撥放(按鍵A)沒被開啟，則運行程式。
                if(!open_A)
                {
                    Determine_PlayStory();
                }
            }
            //如果點擊空白鍵，且對話框沒被縮小，則進行對話操作。
            if(Input.GetKeyDown(KeyCode.Space) && open_BottomBarDemo)
            {
                Button_Play();
            }
            //如果點擊滑鼠右鍵或左Shift，進行判斷。
            if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                //進行對話框放大縮小。
                Button_BottomBarDemo();
            }
            //如果點擊F5，則回主選單。
            if(Input.GetKeyDown(KeyCode.F5))
            {
                Button_GotoMenu();
            }
            //如果點擊左Ctrl，則跳轉到下一節劇情。
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Button_GotoNextStory();
            }
            //如果點擊Z，則跳轉到下一關卡。 (重複呼叫直到跳出Game，也就是到關卡為止)
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Button_GoToLevel();
            }
            if(open_ToLevel)
            {
                InvokeRepeating(nameof(Determine_PlayStory) ,0f ,1f);
            }
            //如果點擊滑鼠中鍵或鍵盤Q，則開啟對話紀錄。
            if(Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Q))
            {
                Button_Log();
            }
            //如果點擊A，則開啟/關閉 自動撥放。
            if(Input.GetKeyDown(KeyCode.A))
            {
                Button_Auto();
            }
            if(open_Auto)   //如果開關Auto啟動，等待0秒後，每隔1.5秒執行一次Function。 且關閉開關Auto
            {
                InvokeRepeating(nameof(Determine_PlayStory) ,0f ,1.5f);
                open_Auto = false;
            }
        }
    }


    private void SetData()
    {
        //取得物件
        list_StoryText = new List<string>();

        //重製資料
        open_BottomBarDemo = true;
        open_A = false;
        open_Auto = false;
        open_ToLevel = false;
        open_LoadValue = false;
        list_StoryText.Clear();
        open_TextRecord = false;
        targets_Play1.SetActive(true);
        targets_Play2.SetActive(true);
        targets_TextRecord.SetActive(true);
    }

    //場景播放與切換
    public void PlayScene(GameScene scene ,int sentenceIndex =-1 ,bool isAnimated =true)
    {
        StartCoroutine(SwitchScene(scene ,sentenceIndex ,isAnimated)) ;
    }
    private IEnumerator SwitchScene(GameScene scene ,int sentenceIndex =-1 ,bool isAnimated =true)
    {
        state =State.ANIMATE ;
        currentScene =scene ;
        if(isAnimated)
        {
            bottomBar.Hide() ;
            yield return new WaitForSeconds(1f) ;
        }
        if(scene is StoryScene)
        {
            //如果是劇情時，打開ButtonPlay的偵測
            targets_Play1.SetActive(true);
            targets_Play2.SetActive(true);
            targets_TextRecord.SetActive(true);


            StoryScene storyScene =scene as StoryScene ;
            history.Add(storyScene) ;
            PlayAudio(storyScene.sentences[sentenceIndex +1]) ;
            if(isAnimated)
            {
                backgroundController.SwitchImage(storyScene.background) ;
                yield return new WaitForSeconds(1f) ;
                bottomBar.ClearText() ;
                bottomBar.Show() ;
                yield return new WaitForSeconds(1f) ;
            }
            else
            {
                backgroundController.SetImage(storyScene.background) ;
                bottomBar.ClearText() ;
            }
            bottomBar.PlayScene(storyScene ,sentenceIndex ,isAnimated) ;
            state =State.IDLE ;

            //從bottomBar抓取當前的對話，並記載到list_StoryText裡，做對話紀錄用(Q、滑鼠中鍵)
            Cache_BottomBarText();


            //如果開關A是被打開的狀態。等待1秒後，每隔1.5秒執行一次Function(自動撥放)。  (按鍵A)
            if(open_A)
            {
                InvokeRepeating(nameof(Determine_PlayStory) ,1f ,1.5f);
            }
        }
        else if(scene is ChooseScene)
        {
            state =State.CHOOSE ;
            
            //如果是選項時，關閉ButtonPlay的偵測
            targets_Play1.SetActive(false);
            targets_Play2.SetActive(false);
            targets_TextRecord.SetActive(false);


            chooseController.SetupChoose(scene as ChooseScene) ;
        }
    }

    //播放媒體音庫(場景音樂與音效)
    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music ,sentence.sound) ;
    }

    //判斷台詞是否撥放完成，撥放下句或者加速撥放
    private void Determine_PlayStory()
    {
        //如果這句已經撥放完成，則撥放下一句。
        if(bottomBar.IsCompleted())
        {
            bottomBar.StopTyping() ;
            if(bottomBar.IsLastSentence())
            {
                //如果是該章節的最後一句話，且自動撥放打開，則進行處理  (先關閉，後在切換完下一章節再打開)
                if(open_A)
                {
                    CancelInvoke("Determine_PlayStory");
                }

                
                //如果後面接續不是Game(Menu或Level)，則跳轉場景。否則繼續撥放下一幕。
                if(!String.IsNullOrEmpty((currentScene as StoryScene).gameScene)){
                    SceneManager.LoadScene((currentScene as StoryScene).gameScene ,LoadSceneMode.Single) ;
                }
                else
                {
                    PlayScene((currentScene as StoryScene).nextScene) ;
                }
            }
            else
            {
                //播放下句話
                bottomBar.PlayNextSentence();
                //播放音樂音效(有的話)
                PlayAudio((currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()]);
                //從bottomBar抓取當前的對話，並記載到list_StoryText裡，做對話紀錄用(Q、滑鼠中鍵)
                Cache_BottomBarText();
            }
        }
        //如果這句話還沒撥放完，且自動撥放(按鍵A)沒啟用，則加速撥放。
        else if(!open_A)
        {
            bottomBar.SpeedUp() ;
        }
    }

    //判斷進行對話框放大縮小。(鍵盤左Shift)
    public void Button_BottomBarDemo()
    {
        //先偵測文字有沒有在自動撥放(鍵盤A)，有的話關閉。
        Determine_Auto();

        //再偵測對話紀錄有沒有打開(鍵盤Q)，有的話關閉。
        Determine_Log();
        
        if(open_BottomBarDemo)
        {
            bottomBar.Button_UnDemo();
            open_BottomBarDemo = false;
        }
        else
        {
            bottomBar.Button_Demo();
            open_BottomBarDemo = true;
        }
    }

    //回主選單(Menu)
    public void Button_GotoMenu()
    {
        List<int> historyIndicies =new List<int>() ;
        history.ForEach(scene =>
        {
            historyIndicies.Add(this.data.scenes.IndexOf(scene)) ;
        }) ;
        SceneManager.LoadScene(menuScene) ;
    }

    //跳轉到下一節劇情(鍵盤左Alt)
    public void Button_GotoNextStory()
    {
        //先偵測文字有沒有在自動撥放(鍵盤A)，有的話關閉。
        Determine_Auto();

        //再偵測對話紀錄有沒有打開(鍵盤Q)，有的話關閉。
        Determine_Log();
        
        bottomBar.StopTyping() ;
        bottomBar.HideSprites() ;
        if(!String.IsNullOrEmpty((currentScene as StoryScene).gameScene)){
            SceneManager.LoadScene((currentScene as StoryScene).gameScene ,LoadSceneMode.Single) ;
        }
        else
            PlayScene((currentScene as StoryScene).nextScene) ;
    }
    
    //對話紀錄(按鈕左ALT)
    public void Button_GoToLevel()
    {
        //先偵測文字有沒有在自動撥放(鍵盤A)，有的話關閉。
        Determine_Auto();

        //再偵測對話紀錄有沒有打開(鍵盤Q)，有的話關閉。
        Determine_Log();
        
        open_ToLevel = true;
    }

    //對話紀錄(按鈕Q)
    public void Button_Log()
    {
        //先偵測文字有沒有在自動撥放(鍵盤A)，有的話關閉。
        Determine_Auto();
        

        if(!open_TextRecord)
        {
            //如果打開Log，則關閉ButtonPlay的偵測
            targets_Play1.SetActive(false);
            targets_Play2.SetActive(false);

            animator_TextRecord.SetTrigger("TextRecord_Show");
            CaChe_list_StoryText();
            open_TextRecord = true;
        }
        else
        {    
            //如果關閉Log，則打開ButtonPlay的偵測
            targets_Play1.SetActive(true);
            targets_Play2.SetActive(true);

            animator_TextRecord.SetTrigger("TextRecord_Hide");
            open_TextRecord = false;
        }
    }
    //偵測是否正在查看對話紀錄(按鍵Q)
    private void Determine_Log()
    {
        if(open_TextRecord)
        {
            animator_TextRecord.SetTrigger("TextRecord_Hide");
            open_TextRecord = false;
        }
    }

    //自動撥放(按鍵A)
    public void Button_Auto()
    {
        //變數重製(關閉) (按鍵Q的Log)
        open_TextRecord = false;

        //如果開關A打開，開啟open_Auto，呼叫執行Function
        if(!open_A)
        {
            open_A = true;
            open_Auto = true;
        }
        //如果開關A關閉，停止呼叫Function
        else
        {
            open_A = false;
            CancelInvoke("Determine_PlayStory");
        }
    }
    //偵測是否正在自動撥放(按鍵A)open_TextRecord
    private void Determine_Auto()
    {
        if(open_A)
        {
            open_A = false;
            CancelInvoke("Determine_PlayStory");
        }
    }

    //從bottomBar抓取當前的對話，並記載到list_StoryText裡，做對話紀錄用(Q、滑鼠中鍵)
    private void Cache_BottomBarText()
    {
        if(bottomBar.return_BottomBarText() != " ")
        {
            list_StoryText.Add(bottomBar.return_BottomBarText());
        }
    }

    //把list_StoryText加到text_TextRecord中，做對話紀錄用(Q、滑鼠中鍵)
    private void CaChe_list_StoryText()
    {
        text_TextRecord.text = "";
        foreach(string string_temp in list_StoryText)
        {
            text_TextRecord.text = text_TextRecord.text + "\n　　" + string_temp + "\n\n"
                                        + "------------------------------------------"
                                        + "---------------------------------------\n";
        }
    }

    //代替滑鼠左鍵
    public void Button_Play()
    {
        //如果劇情章節，或者這句台詞沒有被設定為自動撥放(autoPlay = false)，則進入判斷。  <-  避免左鍵點太快造成卡頓。
        if((currentScene as StoryScene).autoPlay==false 
                || (currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()].autoPlay==false)
        {
            //先偵測文字有沒有在自動撥放(鍵盤A)，有的話關閉。
            Determine_Auto();

            //如果對話紀錄沒打開的話，則執行。
            if(!open_TextRecord)
            {
                Determine_PlayStory();
            }
        }
    }

    //存檔開啟  {Menu -> Load用(Game的部分)}
    private void OpenLoadValue()
    {
        if(!open_LoadValue)
        {
            if(currentScene == levelState.level1.nextSceneOfSuccess)
            {
                csvManager.Change_OpenValue("Game2" ,true);
                open_LoadValue = true;
            }
            else if(currentScene == levelState.level2.nextSceneOfSuccess)
            {
                csvManager.Change_OpenValue("Game3" ,true);
                open_LoadValue = true;
            }
            else if(currentScene == levelState.level3_2.nextSceneOfSuccess)
            {
                csvManager.Change_OpenValue("Game4" ,true);
                open_LoadValue = true;
            }
            else if(currentScene == levelState.level4.nextSceneOfSuccess)
            {
                csvManager.Change_OpenValue("Game5" ,true);
                open_LoadValue = true;
            }
            else if(currentScene == levelState.level5.nextSceneOfSuccess)
            {
                csvManager.Change_OpenValue("GameEnd" ,true);
                open_LoadValue = true;
            }
        }
    }
}
