using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;

public class Level4 : MonoBehaviour
{
    //外部數據 (public)
    public TextMeshProUGUI plaintext_Show ;
    public TextMeshProUGUI key_Show ;
    public TextMeshProUGUI round_Show ;
    public InputString level_Data ;
    public CSVManager csvManager;    //更改存檔資料 {Menu -> Load用}


    //內部資料 (private) {測試用時，會打開成public查看數據}
    private string[] plaintext_Data = {"a1234567\n        89abcdef"
                                      ,"aedcba98\n        76543210"
                                      ,"x2345678\n        90abcdef"
                                      ,"jknmsl08\n        09449000"
                                      ,"08000920\n        00qwerty"};
    private string[] key_Data = {"YTEyMzQ1Njc4\n     OWFiY2RlZg=="
                                ,"YWVkY2JhOTg3\n     NjU0MzIxMA=="
                                ,"eDIzNDU2Nzg5\n     MGFiY2RlZg==" 
                                ,"ZHN4bnhqczE1\n     MTM1NjF4ZA=="
                                ,"MTEyczVkd3Nh\n     ZHp4NTIzNQ=="};
    private string[] ciphertext_Data = {"LKSWyFR4haml39Fs" ,"eeAnZsCJqGh5u0K4" ,"OBUAloP4YHM="
                                        ,"MUQhTxO5UAU9PAg6" ,"2Y0Y3tQztYk0BLE6" ,"X+Y1KUXEoVo=" 
                                        ,"cbgcWXOQp8xY+x6r" ,"r9IVT309JHsTsDLw" ,"ZCiRNCUXwt4=" 
                                        ,"YcNuXjEAKlmsMBXm" ,"Z0fk2C5y6UutBFE9" ,"NUythhjnTlw="
                                        ,"ck2n5aueJHJi/lYO" ,"NNAO5xUwhaYMvHr4" ,"hX1nG7f9p5I="};
    private string plaintext = "";
    private string key = "";
    private string ciphertext = "";
    private int round = -1;
    private int variabl = -1;
    private bool open_LoadValue = false;    //Menu -> Load


    private void Start()
    {
        //重製資料
        SetData();
    }
    public void Update()
    {
        //存檔->第四關卡開啟  {Menu -> Load用}
        if(!open_LoadValue)
        {
            csvManager.Change_OpenValue("Level4" ,true);
            open_LoadValue = true;
        }
    }


    public void SetData()
    {
        //產生亂數
        round = UnityEngine.Random.Range(1 ,4);     //1~3 (min ,max)
        variabl = UnityEngine.Random.Range(0 ,5);   //0~4

        //更新資料
        plaintext = plaintext_Data[variabl];
        key = key_Data[variabl];
        ciphertext = ciphertext_Data[variabl * 3 + round - 1];

        //更新變數
        round_Show.text = round.ToString();
        round_Show.text = "round" + " : " + round_Show.text;
        plaintext_Show.text = "plaintext" + " : \n    " + plaintext;
        key_Show.text = "key" + " : \n   " + key;
    }


    public void Determine_ciphertext(string ciphertext_Show)
    {
        if(ciphertext_Show == ciphertext)
        {
            level_Data.level.switchOpen =true ;
            level_Data.level.success =true ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
    }
}
