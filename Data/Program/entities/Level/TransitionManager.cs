using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class TransitionManager : Singleton<TransitionManager>
{
    [SceneName] public string startScene ;

    public CanvasGroup  fadeCanvasGroup ;
    public float fadeDuration ;
    private bool isFade ;

    private void start()
    {
        StartCoroutine(TransitionToScene(string.Empty ,startScene)) ;
    }

    public void Transition(string from ,string to){
        if(!isFade)
            StartCoroutine(TransitionToScene(from ,to)) ;
    }

    private IEnumerator TransitionToScene(string from ,string to){
        //先讓場景變黑
        yield return Fade(1) ;

        //如果來源端(from)不為空值。
        if(from != string.Empty)
        {
            //傳入所有物品的狀態訊息(是否被使用)
            EventHandler.CallBeforeSceneUnloadEvent() ;
            //用協成方式，卸載當前場景。
            yield return SceneManager.UnloadSceneAsync(from) ;
        }
        //用協成方式，加載切換場景。
        yield return SceneManager.LoadSceneAsync(to ,LoadSceneMode.Additive) ;

        //將新場景設為激活場景
        Scene newScene =SceneManager.GetSceneAt(SceneManager.sceneCount -1) ;
        SceneManager.SetActiveScene(newScene) ;
        
        //傳入所有物品的狀態訊息(是否被使用)
        EventHandler.CallAfterSceneUnloadEvent() ;

        //最後讓場景亮起來
        yield return Fade(0) ;
    }

    /*設計淡入淡出場景，且讓螢幕一次只能進入一支。
        開關打開(true)，螢幕轉場，開關關閉(false)*/
    private IEnumerator Fade(float targetAlpha){
        isFade =true ;
        fadeCanvasGroup.blocksRaycasts =true ;

        float speed =Mathf.Abs(fadeCanvasGroup.alpha -targetAlpha) /fadeDuration ;

        while(!Mathf.Approximately(fadeCanvasGroup.alpha ,targetAlpha)){
            fadeCanvasGroup.alpha =Mathf.MoveTowards(fadeCanvasGroup.alpha
                                    ,targetAlpha ,speed *Time.deltaTime) ;
            yield return null ;
        }

        fadeCanvasGroup.blocksRaycasts =false ;
        isFade =false ;
    }
}
