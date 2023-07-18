using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;

public class Level2 : MonoBehaviour
{
    public InputString data ;
    private GameController gameController ;

    private Animator animator ;
    private int _windowSprite0 =0 ,_windowSprite1 =0 ,_windowSprite2 =0 ,_windowSprite3 =0
                ,_windowSprite4 =0 ,_windowSprite51 =0 ,_windowSprite52 =0 ,_windowSprite53 =0 
                ,_windowSprite54 =0 ,IntOfCanvas =8 ;

    public Timer timer ;    //之後刪掉
    void Start(){
        animator =GetComponent<Animator>() ;
        timer.Begin(data.level.time) ;      //之後刪掉
    }

    void Update(){
        DetermineShow() ;
    }

    public void DetermineInputString(string inputString){
        if(data.DetermineAnswer(inputString)){
            data.level.switchOpen =true ;
            data.level.success =true ;
            SceneManager.LoadScene(data.gameScene ,LoadSceneMode.Single) ;
        }
    }

    private void DetermineShow(){
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite0==1){
            animator.SetTrigger("sprite0-Hide") ;
            _windowSprite0 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite1==1){
            animator.SetTrigger("sprite1-Hide") ;
            _windowSprite1 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite2==1){
            animator.SetTrigger("sprite2-Hide") ;
            _windowSprite2 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite3==1){
            animator.SetTrigger("sprite3-Hide") ;
            _windowSprite3 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite4==1){
            animator.SetTrigger("sprite4-Hide") ;
            _windowSprite4 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite51==1){
            animator.SetTrigger("sprite51-Hide") ;
            _windowSprite51 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite52==1){
            animator.SetTrigger("sprite52-Hide") ;
            _windowSprite52 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite53==1){
            animator.SetTrigger("sprite53-Hide") ;
            _windowSprite53 =0 ;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _windowSprite54==1){
            animator.SetTrigger("sprite54-Hide") ;
            _windowSprite54 =0 ;
        }
    }

    public void ShowSprite0(){
        if(_windowSprite0==0){
            animator.SetTrigger("sprite0-Show") ;
            _windowSprite0 =1 ;
        }
    }
    public void ShowSprite1(){
        if(_windowSprite1==0){
            animator.SetTrigger("sprite1-Show") ;
            _windowSprite1 =1 ;
        }
    }
    public void ShowSprite2(){
        if(_windowSprite2==0){
            animator.SetTrigger("sprite2-Show") ;
            _windowSprite2 =1 ;
        }
    }
    public void ShowSprite3(){
        if(_windowSprite3==0){
            animator.SetTrigger("sprite3-Show") ;
            _windowSprite3 =1 ;
        }
    }
    public void ShowSprite4(){
        if(_windowSprite4==0){
            animator.SetTrigger("sprite4-Show") ;
            _windowSprite4 =1 ;
        }
    }
    public void ShowSprite51(){
        if(_windowSprite51==0){
            animator.SetTrigger("sprite51-Show") ;
            _windowSprite51 =1 ;
        }
    }
    public void ShowSprite52(){
        if(_windowSprite52==0){
            animator.SetTrigger("sprite52-Show") ;
            _windowSprite52 =1 ;
        }
    }
    public void ShowSprite53(){
        if(_windowSprite53==0){
            animator.SetTrigger("sprite53-Show") ;
            _windowSprite53 =1 ;
        }
    }
    public void ShowSprite54(){
        if(_windowSprite54==0){
            animator.SetTrigger("sprite54-Show") ;
            _windowSprite54 =1 ;
        }
    }

    public void ClickLeft(){
        if(IntOfCanvas >4){
            if(IntOfCanvas==5){
                animator.SetTrigger("5-background-Hide") ;
                animator.SetTrigger("4-background-Show") ;
            }
            else if(IntOfCanvas==6){
                animator.SetTrigger("6-background-Hide") ;
                animator.SetTrigger("5-background-Show") ;
            }
            else if(IntOfCanvas==7){
                animator.SetTrigger("7-background-Hide") ;
                animator.SetTrigger("6-background-Show") ;
            }
            else if(IntOfCanvas==8){
                animator.SetTrigger("level2-background-Hide") ;
                animator.SetTrigger("7-background-Show") ;
            }
            IntOfCanvas-- ;
        }
    }
    public void ClickRight(){
        if(IntOfCanvas <8){
            if(IntOfCanvas==7){
                animator.SetTrigger("7-background-Hide") ;
                animator.SetTrigger("level2-background-Show") ;
            }
            else if(IntOfCanvas==6){
                animator.SetTrigger("6-background-Hide") ;
                animator.SetTrigger("7-background-Show") ;
            }
            else if(IntOfCanvas==5){
                animator.SetTrigger("5-background-Hide") ;
                animator.SetTrigger("6-background-Show") ;
            }
            else if(IntOfCanvas==4){
                animator.SetTrigger("4-background-Hide") ;
                animator.SetTrigger("5-background-Show") ;
            }
            IntOfCanvas++ ;
        }
    }
}
