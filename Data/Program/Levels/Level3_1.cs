using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3_1 : MonoBehaviour
{
    //外部數據 (public)
    public InputString level_Data;
    public Animator animator;
    public CSVManager csvManager;    //更改存檔資料 {Menu -> Load用}


    //內部資料 (private) {測試用時，會打開成public查看數據}
    private bool open_animator = false;
    private bool open_windows = false;
    private bool open_LoadValue = false;    //Menu -> Load


    public void Start()
    {
        //重製資料
        SetData();
        
        //偵測是否滿足條件，若滿足則打開小提示跟下一關入口。 {啟用animator}
        if(!open_animator)
        {
            if(Determine_content())
            {
                animator.SetTrigger("Show") ;
                open_animator = true;
            }
            //這邊把time拿來當計數用。 {在Determine_content偵測}
            level_Data.level.time++;
        }
    }
    public void Update()
    {
        //存檔->第三關卡開啟  {Menu -> Load用}
        if(!open_LoadValue)
        {
            csvManager.Change_OpenValue("Level3" ,true);
            open_LoadValue = true;
        }
    }


    private void SetData()
    {
        //取得物件
        animator =GetComponent<Animator>() ;

        //變數重製
        open_windows = false;
    }

    //偵測是否滿足條件(找到三個線索)。
    private bool Determine_content()
    {
        //把Level3_1的time當全域變數計數用。當場景第六次以後被載入，則開啟提示跟下一關的按鈕。
        if(level_Data.level.time >= 5)
            return true;
        else
            return false;
    }

    //點擊Button下一關時，跳出關卡接續下面劇情。
    public void Determine(string inputString){
        if(level_Data.DetermineAnswer(inputString)){
            level_Data.level.switchOpen =true ;
            level_Data.level.success =true ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
    }

    //點擊Button小提示時，跳出關卡到說明文。
    public void Explain_Button()
    {
        open_windows = false;
        level_Data.level.switchOpen =true ;
        level_Data.level.success =false ;
        SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
    }

    //關卡提示的畫面顯示跟關閉。
    public void HintShow_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("Hint_Show") ;
            open_windows = true;
        }
    }
    public void HintHide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("Hint_Hide") ;
            open_windows = false;
        }
    }

    //關卡回答的畫面顯示跟關閉。
    public void AnswerShow_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("Answer_Show") ;
            open_windows = true;
        }
    }
    public void AnswerHide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("Answer_Hide") ;
            open_windows = false;
        }
    }
}
