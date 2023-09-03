using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using UnityEngine.Audio ;
using TMPro ;

public class MenuController : MonoBehaviour
{
    //外部數據 (public)
        public InputString level_Data;
        public GlobalValues globalValues ;
        public CSVManager csvManager;
        public GameObject targets_LoadingCanvas;    //控制讀取畫面的開關狀態
        public Image MenuSprite;    //切換封面圖片用

        //LoadButton
            public Button loadButton_GameGroup1;
            public Button loadButton_GameGroup2;
            public Button loadButton_GameGroup3;
            public Button loadButton_GameGroup4;
            public Button loadButton_GameGroup5;
            public Button loadButton_GameEnd;
            public Button loadButton_LevelGroup1;
            public Button loadButton_LevelGroup2;
            public Button loadButton_LevelGroup3;
            public Button loadButton_LevelGroup4;
            public Button loadButton_LevelGroup5;
            
        //OptionButton
            public TextMeshProUGUI musicValue;
            public AudioMixer musicMixer;
            public TextMeshProUGUI soundsValue;
            public AudioMixer soundsMixer;
    

    //內部資料 (private) {測試用時，會打開成public查看數據}
        private Animator animator;
        private bool open_windows = false;
        private bool open_Loading = false;
        private bool open_MenuSprite = false;   //偵測主選單圖片切換開關用


    public void Start()
    {
        //變數重製
        SetData();
    }
    public void Update()
    {
        //執行動作
        Loading();

        //偵測讀取進度 (看Load目前解鎖進度)
        Determine_Load();
    }


    private void SetData()
    {
        //取得物件
        animator = GetComponent<Animator>();

        //重製資料
        open_windows = false;
        open_Loading = false;
        open_MenuSprite = false;
    }

    //剛開啟遊戲，進入Menu使用
    private void Loading()      //{LoadCanvas}
    {
        if(!open_Loading)
        {
            //畫面讀取
            if(!csvManager.Determine_OpenValue("MenuLoading"))
            {
                targets_LoadingCanvas.SetActive(true);
                animator.SetTrigger("LoadCanvas");
                Invoke(nameof(LoadingCanvas_Close) ,10.5f);    //等待10.5秒後，執行Function
                csvManager.Change_OpenValue("MenuLoading" ,true);
            }
            else
            {
                animator.SetTrigger("Loading");
            }

            open_Loading = true;
        }
    }
    private void LoadingCanvas_Close()
    {
        targets_LoadingCanvas.SetActive(false);
    }

    //偵測讀取進度 (Load_Button用)
    private void Determine_Load()
    {
        //主選單封面判斷更新
        if(csvManager.Determine_OpenValue("Level5"))
        {
            if(!open_MenuSprite)
            {
                if(MenuSprite.sprite != globalValues.MenuSprite4)
                {
                    MenuSprite.sprite = globalValues.MenuSprite4;
                }
                open_MenuSprite = true;
            }
            loadButton_LevelGroup5.interactable = true;
        }
        if(csvManager.Determine_OpenValue("Level4"))
        {
            if(!open_MenuSprite)
            {
                if(MenuSprite.sprite != globalValues.MenuSprite3)
                {
                    MenuSprite.sprite = globalValues.MenuSprite3;
                }
                open_MenuSprite = true;
            }
            loadButton_LevelGroup4.interactable = true;
        }
        if(csvManager.Determine_OpenValue("Game2"))
        {
            if(!open_MenuSprite)
            {
                if(MenuSprite.sprite != globalValues.MenuSprite2)
                {
                    MenuSprite.sprite = globalValues.MenuSprite2;
                }
                open_MenuSprite = true;
            }
            loadButton_GameGroup2.interactable = true;
        }

        //其他正常偵測
        if(csvManager.Determine_OpenValue("Game1"))
            loadButton_GameGroup1.interactable = true;
        if(csvManager.Determine_OpenValue("Game3"))
            loadButton_GameGroup3.interactable = true;
        if(csvManager.Determine_OpenValue("Game4"))
            loadButton_GameGroup4.interactable = true;
        if(csvManager.Determine_OpenValue("Game5"))
            loadButton_GameGroup5.interactable = true;
        if(csvManager.Determine_OpenValue("GameEnd"))
            loadButton_GameEnd.interactable = true;
        if(csvManager.Determine_OpenValue("Level1"))
            loadButton_LevelGroup1.interactable = true;
        if(csvManager.Determine_OpenValue("Level2"))
            loadButton_LevelGroup2.interactable = true;
        if(csvManager.Determine_OpenValue("Level3"))
            loadButton_LevelGroup3.interactable = true;
    }


    //新建遊戲 (NewGame)
    public void NewGame_Button()
    {
        csvManager.Change_OpenValue("Game1" ,true);     //開啟Game1 (Load)
        level_Data.level.switchOpen = true;
        level_Data.level.success = true;
        level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup1);
        SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
    }


    //讀取遊戲 (Load)
    public void LoadShow_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("LoadShow");
            open_windows = true;
        }
    }
    public void LoadHide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("LoadHide");
            open_windows = false;
        }
    }

        //切換上下頁
        public void NextPage_Button()
        {
            animator.SetTrigger("LoadNextPage");
        }
        public void PrevPage_Button()
        {
            animator.SetTrigger("LoadPrevPage");
        }
        
        //讀取關卡
        public void GameGroup1_Button()
        {
            if(csvManager.Determine_OpenValue("Game1"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup1);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void GameGroup2_Button()
        {
            if(csvManager.Determine_OpenValue("Game2"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup2);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void GameGroup3_Button()
        {
            if(csvManager.Determine_OpenValue("Game3"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup3);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void GameGroup4_Button()
        {
            if(csvManager.Determine_OpenValue("Game4"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup4);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void GameGroup5_Button()
        {
            if(csvManager.Determine_OpenValue("Game5"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameGroup5);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void GameEnd_Button()
        {
            if(csvManager.Determine_OpenValue("GameEnd"))
            {
                level_Data.ChangeSwitchScene(globalValues.switchScene_GameEnd);
                level_Data.level.switchOpen = true;
                level_Data.level.success = true;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void LevelGroup1_Button()
        {
            if(csvManager.Determine_OpenValue("Level1"))
            {
                level_Data.gameScene = globalValues.switchScene_LevelGroup1;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void LevelGroup2_Button()
        {
            if(csvManager.Determine_OpenValue("Level2"))
            {
                level_Data.gameScene = globalValues.switchScene_LevelGroup2;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void LevelGroup3_Button()
        {
            if(csvManager.Determine_OpenValue("Level3"))
            {
                level_Data.gameScene = globalValues.switchScene_LevelGroup3;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void LevelGroup4_Button()
        {
            if(csvManager.Determine_OpenValue("Level4"))
            {
                level_Data.gameScene = globalValues.switchScene_LevelGroup4;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }
        public void LevelGroup5_Button()
        {
            if(csvManager.Determine_OpenValue("Level5"))
            {
                level_Data.gameScene = globalValues.switchScene_LevelGroup5;
                SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
            }
        }


    //調整設定 (Option)
    public void OptionsShow_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("OptionsShow");
            open_windows = true;
        }
    }
    public void OptionsHide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("OptionsHide");
            open_windows = false;
        }
    }

        //音量、音效大小調整
        public void OnMusicChanged(float value)
        {
            musicValue.SetText(value +"%") ;
            musicMixer.SetFloat("volume" ,-50 +value /2) ;
        }
        public void OnSoundsChanged(float value)
        {
            soundsValue.SetText(value +"%") ;
            soundsMixer.SetFloat("volume" ,-50 +value /2) ;
        }

        //操作說明
        public void Explain_Button()
        {
            level_Data.ChangeSwitchScene(globalValues.switchScene_GameExplain);
            level_Data.level.switchOpen = true;
            level_Data.level.success = true;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
        }

        //全解放
        public void Unlock_Button()
        {
            //全部參數設為開啟。
            csvManager.Change_OpenValue("Game1" ,true);
            csvManager.Change_OpenValue("Game2" ,true);
            csvManager.Change_OpenValue("Game3" ,true);
            csvManager.Change_OpenValue("Game4" ,true);
            csvManager.Change_OpenValue("Game5" ,true);
            csvManager.Change_OpenValue("GameEnd" ,true);
            csvManager.Change_OpenValue("Level1" ,true);
            csvManager.Change_OpenValue("Level2" ,true);
            csvManager.Change_OpenValue("Level3" ,true);
            csvManager.Change_OpenValue("Level4" ,true);
            csvManager.Change_OpenValue("Level5" ,true);

            //更新Menu Load狀態
            Determine_Load();
        }


    //離開遊戲 (Quit)
    public void Quit_Button()
    {
        csvManager.Change_OpenValue("MenuLoading" ,false);
        Application.Quit(); 
    }
}