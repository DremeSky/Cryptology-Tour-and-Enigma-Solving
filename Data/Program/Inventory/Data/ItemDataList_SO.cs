using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDataList_SO" ,menuName ="Inventory/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject{
    public List<ItemDetails> itemDetailsList ;

    public ItemDetails GetItemDetails(ItemName itemName){
        //創建一個變數i ，在每次呼叫時，尋找背包清單內是否有該物品(用名稱查詢)。
        return itemDetailsList.Find(i => i.itemName == itemName) ;
    }
}

//定義背包中的物品資料(細節屬性)。 (背包物品是可序列化的)
[System.Serializable]
public class ItemDetails{
    public ItemName itemName ;
    public Sprite itemSprite ;
}