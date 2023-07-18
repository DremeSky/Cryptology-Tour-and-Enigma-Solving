using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;
using System ;

public class GameController : MonoBehaviour
{
    public GameScene currentScene ;
    public BottomBarController bottomBar ;
    public SpriteSwitcher backgroundController ;
    public ChooseController chooseController ;
    public AudioController audioController ;

    public DataHolder data ;
    public string menuScene ;
    public LevelState levelState ;
    
    private State state =State.IDLE ;
    private List<StoryScene> history =new List<StoryScene>() ;

    private enum State
    {
        IDLE ,ANIMATE ,CHOOSE
    }

    void Start()
    {
        if(levelState.Level1State()){
            if(levelState.level1.success==true){
                currentScene =levelState.level1.nextSceneOfSuccess ;
            }
            else{
                currentScene =levelState.level1.nextSceneOfFail ;
            }
        }
        else if(SaveManager.IsGameSaved())
        {
            SaveData data =SaveManager.LoadGame() ;
            data.prevScenes.ForEach(scene =>
            {
                history.Add(this.data.scenes[scene] as StoryScene) ;
            }) ;
            currentScene =history[history.Count -1] ;
            history.RemoveAt(history.Count -1) ;
            bottomBar.SetSentenceIndex(data.sentence -1) ;
        }
        if(currentScene is StoryScene)
        {
            StoryScene storyScene =currentScene as StoryScene ;
            history.Add(storyScene) ;
            bottomBar.PlayScene(storyScene ,bottomBar.GetSentenceIndex()) ;
            backgroundController.SetImage(storyScene.background) ;
            PlayAudio(storyScene.sentences[bottomBar.GetSentenceIndex()]) ;
        }
    }

    void Update()
    {
        if(state==State.IDLE)
        {
            if((currentScene as StoryScene).autoPlay==true 
                    || (currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()].autoPlay==true)
            {
                if(bottomBar.IsCompleted())
                {
                    bottomBar.StopTyping() ;
                    if(bottomBar.IsLastSentence())
                    {
                        if(!String.IsNullOrEmpty((currentScene as StoryScene).gameScene)){
                            SaveManager.ClearSavedGame() ;
                            SceneManager.LoadScene((currentScene as StoryScene).gameScene ,LoadSceneMode.Single) ;
                        }
                        else
                            PlayScene((currentScene as StoryScene).nextScene) ;
                    }
                    else
                    {
                        bottomBar.PlayNextSentence() ;
                        PlayAudio((currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()]) ;
                    }
                }
                else
                {
                    bottomBar.SpeedUp() ;
                }
            }
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if(bottomBar.IsCompleted())
                {
                    bottomBar.StopTyping() ;
                    if(bottomBar.IsLastSentence())
                    {
                        if(!String.IsNullOrEmpty((currentScene as StoryScene).gameScene)){
                            SaveManager.ClearSavedGame() ;
                            SceneManager.LoadScene((currentScene as StoryScene).gameScene ,LoadSceneMode.Single) ;
                        }
                        else
                            PlayScene((currentScene as StoryScene).nextScene) ;
                    }
                    else
                    {
                        bottomBar.PlayNextSentence() ;
                        PlayAudio((currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()]) ;
                    }
                }
                else
                {
                    bottomBar.SpeedUp() ;
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                if(bottomBar.IsFirstSentence())
                {
                    if(history.Count >1)
                    {
                        bottomBar.StopTyping() ;
                        bottomBar.HideSprites() ;
                        history.RemoveAt(history.Count -1) ;
                        StoryScene scene =history[history.Count -1] ;
                        history.RemoveAt(history.Count -1) ;
                        PlayScene(scene ,scene.sentences.Count -2 ,false) ;
                    }
                }
                else
                {
                    bottomBar.GoBack() ;
                }
            }
            if(Input.GetKeyDown(KeyCode.F5))
            {
                List<int> historyIndicies =new List<int>() ;
                history.ForEach(scene =>
                {
                    historyIndicies.Add(this.data.scenes.IndexOf(scene)) ;
                }) ;
                SaveData data =new SaveData
                {
                    sentence =bottomBar.GetSentenceIndex() ,
                    prevScenes =historyIndicies 
                } ;
                SaveManager.SaveGame(data) ;
                SceneManager.LoadScene(menuScene) ;
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
                bottomBar.StopTyping() ;
                bottomBar.HideSprites() ;
                if(!String.IsNullOrEmpty((currentScene as StoryScene).gameScene)){
                    SaveManager.ClearSavedGame() ;
                    SceneManager.LoadScene((currentScene as StoryScene).gameScene ,LoadSceneMode.Single) ;
                }
                else
                    PlayScene((currentScene as StoryScene).nextScene) ;
            }
        }
    }

    public void PlayScene(GameScene scene ,int sentenceIndex =-1 ,bool isAnimated =true)
    {
        StartCoroutine(SwitchScene(scene ,sentenceIndex ,isAnimated)) ;
    }

    private IEnumerator SwitchScene(GameScene scene ,int sentenceIndex =-1 ,bool isAnimated =true)
    {
        state =State.ANIMATE ;
        currentScene =scene ;
        if(isAnimated)
        {
            bottomBar.Hide() ;
            yield return new WaitForSeconds(1f) ;
        }
        if(scene is StoryScene)
        {
            StoryScene storyScene =scene as StoryScene ;
            history.Add(storyScene) ;
            PlayAudio(storyScene.sentences[sentenceIndex +1]) ;
            if(isAnimated)
            {
                backgroundController.SwitchImage(storyScene.background) ;
                yield return new WaitForSeconds(1f) ;
                bottomBar.ClearText() ;
                bottomBar.Show() ;
                yield return new WaitForSeconds(1f) ;
            }
            else
            {
                backgroundController.SetImage(storyScene.background) ;
                bottomBar.ClearText() ;
            }
            bottomBar.PlayScene(storyScene ,sentenceIndex ,isAnimated) ;
            state =State.IDLE ;
        }
        else if(scene is ChooseScene)
        {
            state =State.CHOOSE ;
            chooseController.SetupChoose(scene as ChooseScene) ;
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music ,sentence.sound) ;
    }
}
