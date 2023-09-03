using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3_2 : MonoBehaviour
{
    //外部數據 (public)
    public AudioClip up;
    public AudioClip down;
    public AudioClip left; 
    public AudioClip right;
    public AudioClip empty_Audio;
    public InputString level_Data ;


    //內部資料 (private) {測試用時，會打開成public查看數據}
    private int[] dataOrder = new int[] {-1 , -1 , -1};
    private int number_dataOrder = 0;
    private int times_Answer = 0;
    private AudioSource audio_Play;


    public void Start()
    {
        //重製資料
        SetData();
    }
    public void Update()
    {
        //如果偵測到(鍵盤)輸入方向鍵，則執行對應程式；回答正確則計數，錯誤則跳出關卡進說明。
        if(times_Answer < dataOrder.Length)
        {
            if(Determine_ArrowKey() == dataOrder[times_Answer])
            {
                times_Answer++;
            }
            else if(Determine_ArrowKey() != -1)
            {
                times_Answer = 0;
            }
        }

        //依序播放摩斯密碼資料。
        if(number_dataOrder < dataOrder.Length)
        {
            //如果沒有正在撥放音檔，則運行撥放。
            if(!audio_Play.isPlaying)
            {
                audio_Play.clip = dictionary_ArrowKey(dataOrder[number_dataOrder]);
                audio_Play.Play();
                // Invoke(nameof(audio_Play.Play) ,4);
                number_dataOrder++;
            }
        }
        //如果摩斯密碼音檔播放完，則進入檢測是否滿足通關條件。
        else
        {
            // Determine_Pass();
            Invoke(nameof(Determine_Pass) ,10);
        }
    }


    private void SetData()
    {
        //取得物件
        audio_Play = GetComponent<AudioSource>();

        //變數重製
        number_dataOrder = 0;
        times_Answer = 0;

        //隨機生成摩斯密碼資料順序
        generate_randomNumbers();
    }

    //亂數產生
    private void generate_randomNumbers()
    {
        for(int i = 0 ; i < dataOrder.Length ; i++)
        {
            dataOrder[i] = UnityEngine.Random.Range(0 ,4);     //0~3 (min ,max)，小於max。
        }
    }

    //自定義方向鍵涵義(int -> AudioClip)
    private AudioClip dictionary_ArrowKey(int serialNumber)
    {
        switch(serialNumber)
        {
            case 0 : return up;
            case 1 : return down;
            case 2 : return left;
            case 3 : return right;
            default : return empty_Audio;
        }
    }

    //偵測鍵盤方向鍵使用(ArrowKey -> int) {上下左右 -> 0123}
    private int Determine_ArrowKey()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            return 0;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            return 1;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return 2;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            return 3;
        }
        else
        {
            return -1;
        }
    }

    //判定是否通關，移動到對應場景。 (判斷回答正確次數是否等於dataOrder長度)
    private void Determine_Pass()
    {
        if(times_Answer == dataOrder.Length)
        {
            level_Data.level.switchOpen =true ;
            level_Data.level.success =true ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
        else
        {
            level_Data.level.switchOpen =true ;
            level_Data.level.success =false ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
    }
}