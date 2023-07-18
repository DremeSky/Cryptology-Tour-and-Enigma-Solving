using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;

public class Level1 : MonoBehaviour
{
    public InputString data ;
    public Timer timer ;
    private GameController gameController ;

    void Start()
    {
        timer.Begin(data.level.time) ;
    }

    void Update()
    {
        if(timer.End()){
            data.level.switchOpen =true ;
            data.level.success =false ;
            SceneManager.LoadScene(data.gameScene ,LoadSceneMode.Single) ;
        }
    }

    public void Determine(string inputString){
        if(data.DetermineAnswer(inputString)){
            data.level.switchOpen =true ;
            data.level.success =true ;
            SceneManager.LoadScene(data.gameScene ,LoadSceneMode.Single) ;
        }
    }
}
