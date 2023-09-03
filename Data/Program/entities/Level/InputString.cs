using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;
using System ;

public class InputString : MonoBehaviour
{
    public Speaker name ;
    public Level level ;
    public string gameScene ;

    
    public void ChangeName(TextMeshProUGUI inputString){
        if(!String.IsNullOrEmpty(inputString.text))
            name.speakerName =inputString.text ;
    }

    public bool DetermineAnswer(string inputString){
        if(inputString==level.answer){
            return true ;
        }
        else{
            return false ;
        }
    }

    //更改切換場景  (從Menu -> Game)
    public void ChangeSwitchScene(GameScene inputScene)
    {
        //先將切換Scene設為Game(劇情)
        gameScene = "Game";

        //再根據讀入章節，進行調轉設定。
        level.nextSceneOfSuccess = inputScene;
    }
}
