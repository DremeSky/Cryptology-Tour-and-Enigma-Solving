using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class SpriteSwitcher : MonoBehaviour
{
    public bool isSwitched =false ;
    public Image image1 ;
    public Image image2 ;
    private Animator animator ;

    private void Awake()
    {
        animator =GetComponent<Animator>() ;
    } 

    public void SwitchImage(Sprite sprite)
    {
        if(!isSwitched)
        {
            image2.sprite =sprite ;
            image1.sprite =sprite ;
            animator.SetTrigger("SwitchFirst") ;
        }
        else
        {
            image1.sprite =sprite ;
            image2.sprite =sprite ;
            animator.SetTrigger("SwitchSecond") ;
        }
        isSwitched =!isSwitched ;
    }

    public void SetImage(Sprite sprite)
    {
        if(!isSwitched)
        {
            image1.sprite =sprite ;
            image2.sprite =sprite ;
        }
        else
        {
            image2.sprite =sprite ;
            image1.sprite =sprite ;
        }
    }

    public void SyncImage()
    {
        if(!isSwitched)
        {
            image2.sprite =image1.sprite ;
        }
        else
        {
            image1.sprite =image2.sprite ;
        }
    }

    public Sprite GetImage()
    {
        if(!isSwitched)
        {
            return image1.sprite ;
        }
        else
        {
            return image2.sprite ;
        }
    }
}
