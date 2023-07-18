using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData ;
    public Canvas itemlist_Click;
    
    //建立序列化物品清單。
    // [SerializeField] private List<ItemName> itemList =new List<ItemName>() ;
    [SerializeField] public List<ItemName> itemList =new List<ItemName>() ;

    private void OnEnable()
    {
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
    }

    private void OnDisable()
    {
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
    }

    private void OnAfterSceneUnloadEvent()
    {
        if(itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null ,-1);
        else
        {
            for(int i = 0 ;i < itemList.Count ;i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]) ,i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if(index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item ,index);
        }
    }

    public void AddItem(ItemName itemName){
        //如果物品清單內不包含該物品(之後如果要做不唯一物品 ex:蘋果 ，要再調整)。
        if(!itemList.Contains(itemName)){
            itemList.Add(itemName) ;
            //UI對應顯示
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName) ,itemList.Count -1) ;
        }
    }

    // public void ClickItem(ItemName itemName)
    // {
    //     if(inventoryManager.itemList.Contains(ItemName.不明圖案５))
    //     {
    //         if(itemName == ItemName.不明圖案５)
    //         {

    //         }
    //     }
    //     if(inventoryManager.itemList.Contains(ItemName.線索６))
    // }
}
