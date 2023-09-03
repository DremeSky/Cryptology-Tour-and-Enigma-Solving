using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro ;

public class Timer : MonoBehaviour
{
    private int SumSecond ;
    private int minute ;
    private int second ;

    public TextMeshProUGUI timer ;

    public void Begin(int InputSecond){
        SumSecond =InputSecond ;
        minute =SumSecond /60 ;
        second =SumSecond %60 ;
        StartCoroutine(CountDown()) ;
    }

    IEnumerator CountDown(){
        timer.text =string.Format("{0}:{1}" ,minute.ToString("00") ,second.ToString("00")) ;
        while(SumSecond >0){
            yield return new WaitForSeconds(1) ;
            
            SumSecond-- ;
            second-- ;

            if(second <0 && minute >0){
                minute-=1 ;
                second =59 ;
            }
            else if(second <0 && minute==0){
                SumSecond =0 ;
                second =0 ;
            }
            
            timer.text =string.Format("{0}:{1}" ,minute.ToString("00") ,second.ToString("00")) ;
        }

        yield return new WaitForSeconds(1) ;
    }

    public bool End(){
        return SumSecond==0 ;
    }

    public int TimeLeft()
    {
        return SumSecond;
    }
}
