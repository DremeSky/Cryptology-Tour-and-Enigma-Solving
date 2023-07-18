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
            ItemName.不明圖案１ => "不明圖案的碎片。從外觀上來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案２ => "不明圖案的碎片。從外觀上來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案３ => "不明圖案的碎片。從外觀上來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案４ => "不明圖案的碎片。從外觀上來看，或許可以與其他碎片拼在一起?" ,
            ItemName.不明圖案５ => "不明的圖案，或許可以從中看出甚麼規律?" ,
            ItemName.線索１ => "墓碑上的模糊字跡...再找找其他線索吧?" ,
            ItemName.線索２ => "奇怪的圖案，似乎隱含甚麼意義...再找找其他線索吧?" ,
            ItemName.線索３ => "奇怪的圖案，似乎隱含甚麼意義...再找找其他線索吧?" ,
            ItemName.線索４ => "奇怪的圖案，似乎隱含甚麼意義...再找找其他線索吧?" ,
            ItemName.線索５ => "密密麻麻的圖表...再找找其他線索來對比如何?" ,
            ItemName.線索６ => "整理出來的線索，來看看吧?" ,
            //默認(未指定)情況，回傳空("")
            _ => ""    
        };
    }
}
