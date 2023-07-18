using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro ;
using System ;

public class InputString : MonoBehaviour
{
    public Speaker name ;
    public Level level ;
    public string gameScene ;
    // public StoryScene nextSceneOfTrue ;
    // public StoryScene nextSceneOfFalse ;
    // public DataHolder nextScene ;
    // private SaveData saveData ;
    // private GameController gameController ;
    
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

    // public void SetSaveData(){
    //     List<int> historyIndicies =new List<int>() ;
    //     gameController.history.ForEach(scene =>
    //     {
    //         historyIndicies.Add(saveData.scenes.IndexOf(scene)) ;
    //     });
    //     SaveData saveData =new SaveData{
    //         sentence =1 ,
    //         prevScenes =nextScene.scenes ;
    //     };
    //     SaveManager.SaveGame(saveData) ;
    // }
}
