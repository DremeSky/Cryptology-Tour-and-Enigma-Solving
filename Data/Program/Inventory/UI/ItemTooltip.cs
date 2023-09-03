using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using TMPro ;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText ;

    public void UpdateItemName(ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
            //Level2
            ItemName.不明圖案１ => "不明圖案的碎片。從外觀上\n來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案２ => "不明圖案的碎片。從外觀上\n來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案３ => "不明圖案的碎片。從外觀上\n來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案４ => "不明圖案的碎片。從外觀上\n來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案５ => "不明的圖案，或許可以從中\n看出甚麼規律?" ,
            ItemName.線索１ => "墓碑上的模糊字跡...\n再找找其他線索吧?" ,
            ItemName.線索２ => "奇怪的圖案，似乎隱含甚麼\n意義...再找找其他線索吧?" ,
            ItemName.線索３ => "奇怪的圖案，似乎隱含甚麼\n意義...再找找其他線索吧?" ,
            ItemName.線索４ => "奇怪的圖案，似乎隱含甚麼\n意義...再找找其他線索吧?" ,
            ItemName.線索５ => "密密麻麻的圖表...\n再找找其他線索\n來對比如何?" ,
            ItemName.線索６ => "整理出來的線索，來看看吧?" ,

            //Level3
            ItemName.信箱圖案 => "......這不是信箱嗎?!" ,
            ItemName.信箱地址 => "意義不明的文字...或許\n可以再找找其他線索?" ,
            ItemName.碎紙片 => "意義不明的文字...或許\n可以再找找其他線索?" ,
            ItemName.空氣１ => "...這裡啥也沒有，塗了個寂寞。" ,
            ItemName.空氣２ => "...這裡除了灰塵啥也沒有。" ,
            ItemName.空氣３ => "恭喜獲得空氣餅乾一塊^^" ,
            ItemName.彩蛋１ => "作者的大秘寶之一，話說\n原來板磚是可以吃的嗎?" ,
            ItemName.彩蛋２ => "作者的大秘寶之一，\n出門放放鬆。" ,

            //默認(未指定)情況，回傳空("")
            _ => ""    
        };
    }
}
