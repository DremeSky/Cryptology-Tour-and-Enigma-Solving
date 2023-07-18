using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //建立儲存物品的可見狀態。
    private Dictionary<ItemName ,bool> itemAvailabelDict =new Dictionary<ItemName ,bool>() ;

    //當被啟用
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent ;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent ;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent ;
    }

    //當被禁用
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent ;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent ;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent ;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if(!itemAvailabelDict.ContainsKey(item.itemName))
                itemAvailabelDict.Add(item.itemName ,true) ;
        }
    }
    
    private void OnAfterSceneUnloadEvent()
    {
        //去Item裡搜尋是否存在該物品
        foreach (var item in FindObjectsOfType<Item>())
        {
            //如果Item字典沒有此物品，將此物品天入(Key為該物品名稱)。
            if(!itemAvailabelDict.ContainsKey(item.itemName))
                itemAvailabelDict.Add(item.itemName ,true) ;
            //如果有此物品(也就是第二次以後被呼叫)，去Item字典尋找該物品的bool值(看是否被使用過)。
            else
                item.gameObject.SetActive(itemAvailabelDict[item.itemName]) ;
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails ,int arg2)
    {
        //如果在拾取物品後，將對應物品狀態更改為禁用(false)。
        if(itemDetails != null)
        {
            itemAvailabelDict[itemDetails.itemName] = false ;
        }
    }
}