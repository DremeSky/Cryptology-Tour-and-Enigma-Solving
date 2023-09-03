using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class ItemClick : MonoBehaviour
{
    private Animator animator;
    public bool windowSprite = false ;
    private ItemName tempItem;

    void Start(){
        animator =GetComponent<Animator>() ;
    }

    public void ShowItem(ItemName itemName)
    {
        if(windowSprite == false)
        {
            //Level2
            if(itemName == ItemName.不明圖案５)
            {
                animator.SetTrigger("picture5-Show") ;
                windowSprite = true;
                tempItem = ItemName.不明圖案５;
            }
            if(itemName == ItemName.線索６)
            {
                animator.SetTrigger("clue6-Show") ;
                windowSprite = true;
                tempItem = ItemName.線索６;
            }
            if(itemName == ItemName.彩蛋２)
            {
                animator.SetTrigger("Easter_egg2-Show") ;
                windowSprite = true;
                tempItem = ItemName.彩蛋２;
            }

            //Level3
            if(itemName == ItemName.信箱圖案)
            {
                animator.SetTrigger("mail_picture-Show") ;
                windowSprite = true;
                tempItem = ItemName.信箱圖案;
            }
            if(itemName == ItemName.信箱地址)
            {
                animator.SetTrigger("mail_address-Show") ;
                windowSprite = true;
                tempItem = ItemName.信箱地址;
            }
            if(itemName == ItemName.碎紙片)
            {
                animator.SetTrigger("paper-Show") ;
                windowSprite = true;
                tempItem = ItemName.碎紙片;
            }
            if(itemName == ItemName.彩蛋１)
            {
                animator.SetTrigger("Easter_egg1-Show") ;
                windowSprite = true;
                tempItem = ItemName.彩蛋１;
            }
        }
    }

    public void HideItem()
    {
        if(windowSprite == true)
        {
            //Level2
            if(tempItem == ItemName.不明圖案５)
            {
                animator.SetTrigger("picture5-Hide") ;
                windowSprite = false;
            }
            if(tempItem == ItemName.線索６)
            {
                animator.SetTrigger("clue6-Hide") ;
                windowSprite = false;
            }
            if(tempItem == ItemName.彩蛋２)
            {
                animator.SetTrigger("Easter_egg2-Hide") ;
                windowSprite = false;
            }

            //Level3
            if(tempItem == ItemName.信箱圖案)
            {
                animator.SetTrigger("mail_picture-Hide") ;
                windowSprite = false;
            }
            if(tempItem == ItemName.信箱地址)
            {
                animator.SetTrigger("mail_address-Hide") ;
                windowSprite = false;
            }
            if(tempItem == ItemName.碎紙片)
            {
                animator.SetTrigger("paper-Hide") ;
                windowSprite = false;
            }
            if(tempItem == ItemName.彩蛋１)
            {
                animator.SetTrigger("Easter_egg1-Hide") ;
                windowSprite = false;
            }
        }
    }
    
    public bool DetermineState()
    {
        return windowSprite;
    }
}
