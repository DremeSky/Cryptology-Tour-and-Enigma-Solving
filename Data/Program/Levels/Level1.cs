using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;

public class Level1 : MonoBehaviour
{
    //外部數據 (public)
    public InputString level_Data;
    public Animator animator;
    public Timer timer;
    public TextMeshProUGUI HintText;
    public CSVManager csvManager;    //更改存檔資料 {Menu -> Load用}

        //控制小提示按鈕的開關(顯示)狀態
            public GameObject targets_Button1;
            public GameObject targets_Button2;
            public GameObject targets_Button3;
            public GameObject targets_Button4;
            public GameObject targets_Button5;
            public GameObject targets_Button6;


    //內部資料 (private) {測試用時，會打開成public查看數據}
    private bool open_windows = false;
    private bool open_windows_Answer = false;
    private bool open_LoadValue = false;    //Menu -> Load


    public void Start()
    {
        //重製資料
        SetData();

        //開始計時
        timer.Begin(level_Data.level.time);
    }
    public void Update()
    {
        //存檔->第一關卡開啟  {Menu -> Load用}
        if(!open_LoadValue)
        {
            csvManager.Change_OpenValue("Level1" ,true);
            open_LoadValue = true;
        }

        //偵測剩餘時間，產生對應互動。
        Determine_time();
    }


    private void SetData()
    {
        //取得物件
        animator = GetComponent<Animator>();

        //變數重製
        open_windows = false;
        open_windows_Answer = false;
        HintText.text = "";
        targets_Button1.SetActive(false);
        targets_Button2.SetActive(false);
        targets_Button3.SetActive(false);
        targets_Button4.SetActive(false);
        targets_Button5.SetActive(false);
        targets_Button6.SetActive(false);
    }


    //關卡提示的畫面顯示跟關閉。
    private void HintShow_Function()
    {
        //如果關卡輸入打開，先將其關閉。
        if(open_windows_Answer)
        {
            open_windows_Answer = false;
        }

        //再進行關卡提示。
        if(!open_windows)
        {
            animator.SetTrigger("Hint_Show") ;
            open_windows = true;
        }
    }
    private void HintHide_Function()
    {
        if(open_windows)
        {
            animator.SetTrigger("Hint_Hide") ;
            open_windows = false;
        }
    }
    public void HintShow_Button1()
    {
        HintText.text = "有點像....對聯啊....";
        HintShow_Function();
    }
    public void HintShow_Button2()
    {
        HintText.text = "左右兩聯似乎...\n個別代表一種加密方式..?";
        HintShow_Function();
    }
    public void HintShow_Button3()
    {
        HintText.text = "庭院守護者...\n是說仰賴什麼在保護嗎..?";
        HintShow_Function();
    }
    public void HintShow_Button4()
    {
        HintText.text = "支配者...好像是個人..?";
        HintShow_Function();
    }
    public void HintShow_Button5()
    {
        HintText.text = "守護者...難道\n是在庭院的外圍..?";
        HintShow_Function();
    }
    public void HintShow_Button6()
    {
        HintText.text = "有誰支配過高盧呢..?";
        HintShow_Function();
    }
    public void HintHide_Button()
    {
        HintHide_Function();
        HintText.text = "";
    }
    //關卡回答的畫面顯示跟關閉。
    public void AnswerShow_Button()
    {
        //如果關卡提示打開，先將其關閉。
        if(open_windows)
        {
            open_windows = false;
        }

        //再進行關卡輸入。
        if(!open_windows_Answer)
        {
            animator.SetTrigger("Answer_Show") ;
            open_windows_Answer = true;
        }
    }
    public void AnswerHide_Button()
    {
        if(open_windows_Answer)
        {
            animator.SetTrigger("Answer_Hide") ;
            open_windows_Answer = false;
        }
    }


    //偵測關卡答案
    public void Determine_Answer(string inputString){
        if(level_Data.DetermineAnswer(inputString)){
            level_Data.level.switchOpen =true ;
            level_Data.level.success =true ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
    }
    //偵測時間是否歸零
    private void Determine_time()
    {
        //如果剩餘對應時間，打開對應提示。
        switch(timer.TimeLeft())
        {
            case 270 : targets_Button1.SetActive(true);
                        break;
            case 210 : targets_Button2.SetActive(true);
                        break;
            case 150 : targets_Button3.SetActive(true);
                        break;
            case  90 : targets_Button4.SetActive(true);
                        break;
            case  60 : targets_Button5.SetActive(true);
                        break;
            case  30 : targets_Button6.SetActive(true);
                        break;
            default : break;
        }

        //如果時間結束，跳轉(失敗)劇情。
        if(timer.End()){
            level_Data.level.switchOpen =true;
            level_Data.level.success =false;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
        }
    }
}