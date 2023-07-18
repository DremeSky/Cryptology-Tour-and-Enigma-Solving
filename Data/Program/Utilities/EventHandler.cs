using System ;
using UnityEngine;

public static class EventHandler
{
    public static event Action<ItemDetails ,int> UpdateUIEvent ;

    public static void CallUpdateUIEvent(ItemDetails itemDetails ,int index){
        UpdateUIEvent?.Invoke(itemDetails ,index) ;
    }
    
    //建立偵測物件被調用前後。
    public static event Action BeforeSceneUnloadEvent ;
    public static void CallBeforeSceneUnloadEvent(){
        BeforeSceneUnloadEvent?.Invoke() ;
    }

    public static event Action AfterSceneUnloadEvent ;
    public static void CallAfterSceneUnloadEvent(){
        AfterSceneUnloadEvent?.Invoke() ;
    }

    public static event Action<ItemDetails ,bool> ItemSelectedEvent ;
    public static void CallItemSelectedEvent(ItemDetails itemDetails ,bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails ,isSelected) ;
    }

    public static event Action<int> ChangeItemEvent ;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }
}