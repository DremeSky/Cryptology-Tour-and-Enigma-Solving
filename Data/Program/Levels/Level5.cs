using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level5 : MonoBehaviour
{
    //外部數據 (public)
        //參數 <- 用以顯示在螢幕上
            public InputString level_Data;
            public Timer timer;
            public Animator animator;
            public GameObject targets_group1;    //控制項圈物件的開關狀態
            public GameObject targets_group2;    //控制項圈物件的開關狀態
            public CSVManager csvManager;    //更改存檔資料 {Menu -> Load用}

            //項圈
                public TextMeshProUGUI Show_group1_one;
                public TextMeshProUGUI Show_group1_two;
                public TextMeshProUGUI Show_group1_three;
            //腳鍊
                public TextMeshProUGUI Show_group2_one;
                public TextMeshProUGUI Show_group2_two;
                public TextMeshProUGUI Show_group2_three;
    

    //內部資料 (private) {測試用時，會打開成public查看數據}
        //Data <- 建立資料庫，用以提取數據
            private bool open_windows = false;
            private bool group1_pass = false;
            private bool group2_pass = false;
            private bool open_LoadValue = false;

            //項圈 ； p+q 、 (p+1)*(q+1) 、 e 、 d
                private string[] data_group1_one = {"118" ,"30" ,"12" ,"186" ,"74"};
                private string[] data_group1_two = {"3456" ,"252" ,"48" ,"8820" ,"288"};
                private string[] data_group1_three = {"79" ,"29" ,"31" ,"37" ,"23"};
                private string[] data_group1_answer = {"1019" ,"53" ,"7" ,"685" ,"67"};
            //腳鍊 ； p 、 q 、 e 、 d
                private string[] data_group2_one = {"47" ,"13" ,"5" ,"89" ,"3"};
                private string[] data_group2_two = {"71" ,"17" ,"7" ,"97" ,"71"};
                private string[] data_group2_three = {"79" ,"29" ,"31" ,"37" ,"23"};
                private string[] data_group2_answer = {"1019" ,"53" ,"7" ,"685" ,"67"};

        //參數 <- 建立用以隨機抽取、儲存一組數據
            //項圈
                private string group1_one = "";
                private string group1_two = "";
                private string group1_three = "";
                private string group1_answer = "";
                private int group1_variabl = -1;    //隨機變數用
            //腳鍊
                private string group2_one = "";
                private string group2_two = "";
                private string group2_three = "";
                private string group2_answer = "";
                private int group2_variabl = -1;    //隨機變數用
    
    
    private void Start()
    {
        //變數重製
        SetData();
        
        //開始計時
        timer.Begin(level_Data.level.time);
    }
    public void Update()
    {
        //存檔->第五關卡開啟  {Menu -> Load用}
        if(!open_LoadValue)
        {
            csvManager.Change_OpenValue("Level5" ,true);
            open_LoadValue = true;
        }

        Determine_clear();
        Determine_time();
    }
    

    private void SetData()
    {
        //取得物件
        animator = GetComponent<Animator>();
        targets_group1 = GameObject.Find("項圈 {Button}");
        targets_group2 = GameObject.Find("腳鍊 {Button}");

        //重製資料
        open_windows = false;
        group1_pass = false;
        group2_pass = false;
        targets_group1.SetActive(true);
        targets_group2.SetActive(true);

        //產生亂數
        group1_variabl = UnityEngine.Random.Range(0 ,5);   //0~4
        group2_variabl = UnityEngine.Random.Range(0 ,5);   //0~4

        //更新對應變數
        group1_one = data_group1_one[group1_variabl];
        group2_one = data_group2_one[group2_variabl];
        group1_two = data_group1_two[group1_variabl];
        group2_two = data_group2_two[group2_variabl];
        group1_three = data_group1_three[group1_variabl];
        group2_three = data_group2_three[group2_variabl];
        group1_answer = data_group1_answer[group1_variabl];
        group2_answer = data_group2_answer[group2_variabl];

        //顯示對應變數
        Show_group1_one.text = group1_one;
        Show_group2_one.text = group2_one;
        Show_group1_two.text = group1_two;
        Show_group2_two.text = group2_two;
        Show_group1_three.text = group1_three;
        Show_group2_three.text = group2_three;
    }
    
    //項圈畫面的顯示跟關閉。
    public void Group1Show_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("Group1_Show") ;
            open_windows = true;
        }
    }
    public void Group1Hide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("Group1_Hide") ;
            open_windows = false;
        }
    }
    //腳鍊畫面的顯示跟關閉。
    public void Group2Show_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("Group2_Show") ;
            open_windows = true;
        }
    }
    public void Group2Hide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("Group2_Hide") ;
            open_windows = false;
        }
    }

    public void Determine_group1_answer(string Show_group1_answer)
    {
        if(Show_group1_answer == group1_answer)
        {
            group1_pass = true;
            targets_group1.SetActive(false);
            Group1Hide_Button();
        }
    }
    public void Determine_group2_answer(string Show_group2_answer)
    {
        if(Show_group2_answer == group2_answer)
        {
            group2_pass = true;
            targets_group2.SetActive(false);
            Group2Hide_Button();
        }
    }

    //檢測是否通關(項圈；腳鍊) <- 用在Update()
    private void Determine_clear()
    {
        if(group1_pass && group2_pass)
        {
            level_Data.level.switchOpen =true;
            level_Data.level.success =true;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
        }
    }
    
    //偵測時間是否歸零
    private void Determine_time()
    {
        if(timer.End()){
            level_Data.level.switchOpen =true;
            level_Data.level.success =false;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single);
        }
    }
}