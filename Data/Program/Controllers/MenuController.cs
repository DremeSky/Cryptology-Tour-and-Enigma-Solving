using UnityEngine;
using UnityEngine.SceneManagement ;
using UnityEngine.Audio ;
using TMPro ;
using UnityEngine.UI ;

public class MenuController : MonoBehaviour
{
    public string gameScene ;

    public TextMeshProUGUI musicValue ;
    public AudioMixer musicMixer ;
    public TextMeshProUGUI soundsValue ;
    public AudioMixer soundsMixer ;
    public Button loadButton ;
    // public GameController gameController ;
    // public GameScene nextScene ;

    private Animator animator ;
    private int _window =0 ;
    public GlobalValues globalValues ;

    public void Start()
    {
        animator =GetComponent<Animator>() ;
        if(globalValues.openMenuLoading){
            animator.SetTrigger("LoadCanvas") ;
            globalValues.openMenuLoading =false ;
        }
        loadButton.interactable =SaveManager.IsGameSaved() ;
    }

    public void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && _window==1)
        {
            animator.SetTrigger("HideOptions") ;
            _window =0 ;
        }
    }

    public void NewGame()
    {
        SaveManager.ClearSavedGame() ;
        Load() ;
    }

    public void Load()
    {
        SceneManager.LoadScene(gameScene ,LoadSceneMode.Single) ;
    }

    public void ShowOptions()
    {
        animator.SetTrigger("ShowOptions") ;
        _window =1 ;
    }

    public void Quit()
    {
        Application.Quit() ;
    }

    public void OnMusicChanged(float value)
    {
        musicValue.SetText(value +"%") ;
        musicMixer.SetFloat("volume" ,-50 +value /2) ;
    }

    public void OnSoundsChanged(float value)
    {
        soundsValue.SetText(value +"%") ;
        soundsMixer.SetFloat("volume" ,-50 +value /2) ;
    }
}
