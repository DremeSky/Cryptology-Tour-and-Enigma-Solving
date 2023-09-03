using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //建立變數，直接獲取(鼠標)世界座標
        /*=>是拉姆達表達式，在每次調用 mouseWorldPosition 的時候都會計算一次。
        如果是=那麼單詞賦值之後數據不會變化了。*/
    private Vector3 mouseWorldPosition => Camera.main.ScreenToWorldPoint
                (new Vector3(Input.mousePosition.x ,Input.mousePosition.y ,0)) ;
    private ItemName currentItem ;
    private bool holdItem ;
    private bool canClick ;

    public ItemClick itemClick;

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent ;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent ;
    }

    private void Update(){
        canClick =ObjectAtMousePosition() ;
        
        if(canClick && Input.GetMouseButtonDown(0)){
            //檢測鼠標互動情況
            ClickAction(ObjectAtMousePosition().gameObject) ;
        }

        //背包系統的物品使用
        //物品被點擊
        if(Input.GetMouseButtonDown(0))
        {
            //Level2
            if(currentItem == ItemName.線索６)
            {
                itemClick.ShowItem(ItemName.線索６);
                currentItem = ItemName.None;
            }
            else if(currentItem == ItemName.不明圖案５)
            {
                itemClick.ShowItem(ItemName.不明圖案５);
                currentItem = ItemName.None;
            }
            else if(currentItem == ItemName.彩蛋２)
            {
                itemClick.ShowItem(ItemName.彩蛋２);
                currentItem = ItemName.None;
            }

            //level3
            else if(currentItem == ItemName.信箱圖案)
            {
                itemClick.ShowItem(ItemName.信箱圖案);
                currentItem = ItemName.None;
            }
            else if(currentItem == ItemName.信箱地址)
            {
                itemClick.ShowItem(ItemName.信箱地址);
                currentItem = ItemName.None;
            }
            else if(currentItem == ItemName.碎紙片)
            {
                itemClick.ShowItem(ItemName.碎紙片);
                currentItem = ItemName.None;
            }
            else if(currentItem == ItemName.彩蛋１)
            {
                itemClick.ShowItem(ItemName.彩蛋１);
                currentItem = ItemName.None;
            }
        }
        //物品返回
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            itemClick.HideItem();
        }
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails ,bool isSelected)
    {
        holdItem = isSelected ;
        if(isSelected)
        {
            currentItem = itemDetails.itemName ;
        }
    }

    private void ClickAction(GameObject clickObject){
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport =clickObject.GetComponent<Teleport>() ;
                teleport?.TeleportToScene() ;
                break ;
            case "Item":
                var item =clickObject.GetComponent<Item>() ;
                item?.ItemClicked() ;
                break ;
        }
    }

    //檢測鼠標點擊範圍的碰撞體
    private Collider2D ObjectAtMousePosition(){
        return Physics2D.OverlapPoint(mouseWorldPosition) ;
    }
}
