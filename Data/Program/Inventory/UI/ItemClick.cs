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
        }
    }

    public void HideItem()
    {
        if(windowSprite == true)
        {
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
        }
    }
    
    public bool DetermineState()
    {
        return windowSprite;
    }
}
