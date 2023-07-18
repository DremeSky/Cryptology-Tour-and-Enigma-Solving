using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems ;
using UnityEngine.UI ;

public class SlotUI : MonoBehaviour ,IPointerClickHandler ,IPointerEnterHandler ,IPointerExitHandler
{
    public Image itemImage ;
    public ItemTooltip tooltip ;
    public ItemDetails currentItem ;
    private bool isSelected ;

    public void SetItem(ItemDetails itemDetails){
        currentItem =itemDetails ;
        this.gameObject.SetActive(true) ;
        itemImage.sprite =itemDetails.itemSprite ;
        itemImage.SetNativeSize() ;     // 將每張讀入圖片，設定成自然大小 (圖片原本大小)
    }

    public void SetEmpty(){
        this.gameObject.SetActive(false) ;
    }

    //鼠標點擊、滑入、滑出
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected ;
        EventHandler.CallItemSelectedEvent(currentItem ,isSelected) ;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.activeInHierarchy)
        {
            tooltip.gameObject.SetActive(true) ;
            tooltip.UpdateItemName(currentItem.itemName) ;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false) ;
    }
}
