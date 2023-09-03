using UnityEngine;
using UnityEngine.SceneManagement ;
using TMPro;

public class Level2 : MonoBehaviour
{
    //外部數據 (public)
    public InputString level_Data;
    public TextMeshProUGUI Placeholder;
    public TextMeshProUGUI Text;
    public CSVManager csvManager;    //更改存檔資料 {Menu -> Load用}
    
    
    //內部資料 (private) {測試用時，會打開成public查看數據}
    private Animator animator;
    private bool open_windows = false;
    private bool open_LoadValue = false;    //Menu -> Load


    public void Start()
    {
        //變數重製
        SetData();
    }
    public void Update()
    {
        //存檔->第二關卡開啟  {Menu -> Load用}
        if(!open_LoadValue)
        {
            csvManager.Change_OpenValue("Level2" ,true);
            open_LoadValue = true;
        }
    }


    private void SetData()
    {
        //取得物件
        animator =GetComponent<Animator>() ;
        
        //重製資料
        open_windows = false;
    }

    //偵測關卡輸入，如果正確，則跳轉下一關。
    public void DetermineInputString(string inputString){
        if(level_Data.DetermineAnswer(inputString)){
            level_Data.level.switchOpen =true ;
            level_Data.level.success =true ;
            SceneManager.LoadScene(level_Data.gameScene ,LoadSceneMode.Single) ;
        }
    }

    //如果沒有輸入關卡回答，則恢復原本樣貌(Enter text...)
    public void OnSelect(string inputString){
        if(Text.text == ""){
            Placeholder.text = "";
        }
    }
    public void OnDeselect(string inputString){
        if(Text.text == ""){
            Placeholder.text = "Enter text...";
        }
    }

    //關卡回答的畫面顯示跟關閉。
    public void AnswerShow_Button()
    {
        if(!open_windows)
        {
            animator.SetTrigger("Answer_Show") ;
            open_windows = true;
        }
    }
    public void AnswerHide_Button()
    {
        if(open_windows)
        {
            animator.SetTrigger("Answer_Hide") ;
            open_windows = false;
        }
    }

    //偵測彈出畫面是否正在使用
    public bool Determine_screen()
    {
        if(open_windows)
            return true;
        else
            return false;
    }
}
