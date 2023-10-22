using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton ,rightButton ;
    public SlotUI slotUI ;
    public int currentIndex ;   //UI當前物品的序號
    public InventoryManager inventoryManager ;
    private ItemName tempItem;

    private void OnEnable(){
        EventHandler.UpdateUIEvent += OnUpdateUIEvent ;
    }
    private void OnDisable(){
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent ;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails ,int index){
        if(itemDetails == null){    //如果沒有物品 (背包為空)
            slotUI.SetEmpty() ;
            currentIndex =-1 ;
            leftButton.interactable =false ;    //左鍵設為無法點擊
            rightButton.interactable =false ;   //右鍵設為無法點擊
        }
        else{
            currentIndex =index ;
            slotUI.SetItem(itemDetails) ;
            tempItem = inventoryManager.itemList[index];

            if(index > 0)
                leftButton.interactable = true;
            if(index == inventoryManager.itemList.Count - 1)
                rightButton.interactable = false;
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }

            //Level2
            //如果背包數量超過4樣，偵測圖案碎片是否齊全。去除碎片且合成圖案5。
            if(inventoryManager.itemList.Count >= 4)
            {
                int amount = 0;

                if(inventoryManager.itemList.Contains(ItemName.不明圖案１))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.不明圖案２))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.不明圖案３))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.不明圖案４))
                    amount++;

                if(amount == 4)
                {
                    for(int i = 0 ;i <inventoryManager.itemList.Count ;     )
                    {
                        if(inventoryManager.itemList[i] == ItemName.不明圖案１)
                            inventoryManager.itemList.Remove(ItemName.不明圖案１);
                        else if(inventoryManager.itemList[i] == ItemName.不明圖案２)
                            inventoryManager.itemList.Remove(ItemName.不明圖案２);
                        else if(inventoryManager.itemList[i] == ItemName.不明圖案３)
                            inventoryManager.itemList.Remove(ItemName.不明圖案３);
                        else if(inventoryManager.itemList[i] == ItemName.不明圖案４)
                            inventoryManager.itemList.Remove(ItemName.不明圖案４);
                        else
                            i++;
                    }

                    inventoryManager.AddItem(ItemName.不明圖案５) ;
                    currentIndex = inventoryManager.itemList.Count -1;
                    EventHandler.CallUpdateUIEvent(inventoryManager.itemData.GetItemDetails(ItemName.不明圖案５) ,currentIndex);
                    SwitchItem(0);
                }
            }
            
            //如果背包數量超過5樣，偵測線索是否齊全。去除其他線索且合成線索6。
            if(inventoryManager.itemList.Count >= 5)
            {
                int amount = 0;

                if(inventoryManager.itemList.Contains(ItemName.線索１))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.線索２))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.線索３))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.線索４))
                    amount++;
                if(inventoryManager.itemList.Contains(ItemName.線索５))
                    amount++;

                if(amount == 5)
                {
                    for(int i = 0 ;i <inventoryManager.itemList.Count ;     )
                    {
                        if(inventoryManager.itemList[i] == ItemName.線索１)
                            inventoryManager.itemList.Remove(ItemName.線索１);
                        else if(inventoryManager.itemList[i] == ItemName.線索２)
                            inventoryManager.itemList.Remove(ItemName.線索２);
                        else if(inventoryManager.itemList[i] == ItemName.線索３)
                            inventoryManager.itemList.Remove(ItemName.線索３);
                        else if(inventoryManager.itemList[i] == ItemName.線索４)
                            inventoryManager.itemList.Remove(ItemName.線索４);
                        else if(inventoryManager.itemList[i] == ItemName.線索５)
                            inventoryManager.itemList.Remove(ItemName.線索５);
                        else
                            i++;
                    }

                    inventoryManager.AddItem(ItemName.線索６) ;
                    currentIndex = inventoryManager.itemList.Count -1;
                    EventHandler.CallUpdateUIEvent(inventoryManager.itemData.GetItemDetails(ItemName.線索６) ,currentIndex);
                    SwitchItem(0);
                }
            }
        }
    }

    //切換物品欄，最左邊最右邊以及中間(else，超過兩個物品的情況)
    public void SwitchItem(int amount)
    {
        var index = currentIndex + amount ;

        if(index == 0)
        {
            leftButton.interactable = false ;
            rightButton.interactable = true ;
        }
        else if(index == inventoryManager.itemList.Count - 1)
        {
            leftButton.interactable = true ;
            rightButton.interactable = false ;
        }
        else
        {
            leftButton.interactable = true ;
            rightButton.interactable = true ;
        }

        EventHandler.CallChangeItemEvent(index);
    }
}
