using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //導入UI API。
using UnityEngine.SceneManagement ;

public class FullScreenControl : MonoBehaviour {
    //外部數據 (public)
    public CSVManager csvManager;


    void Update ()
    {
        //偵測是否為全螢幕 (或者為視窗螢幕)
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Switch_Screen();
        }

        //如果點擊F5，則回主選單。
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("Menu" ,LoadSceneMode.Single);
        }
    }

    //切換全螢幕與視窗螢幕
    public void Switch_Screen()
    {
        //新增一個bool，用以紀載狀態
        bool temp = csvManager.Determine_OpenValue("screen");


        //執行切換狀態(bool)
        if(temp)
            temp = false;
        else
            temp = true;
        csvManager.Change_OpenValue("screen" ,temp);
        

        //全螢幕
        if(temp)
        {
            Screen.fullScreen = true;
        }
        //視窗螢幕
        else
        {
            Screen.fullScreen = false;
        }
    }
}