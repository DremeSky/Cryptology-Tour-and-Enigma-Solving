using UnityEngine;
using UnityEngine.SceneManagement ;

public class LevelState : MonoBehaviour
{
    //外部數據 (public)
    public Level level1;
    public Level level2;
    public Level level3_1;
    public Level level3_2;
    public Level level4;
    public Level level5;
    public Level menu;
    public Level Bug;


    //關卡切換劇情時，從關卡資料，代入特定劇情。 {在GameController使用}
    public GameScene Determine_LevelState()
    {
        //Level1
        if(Level1State())
        {
            if(level1.success==true){
                return level1.nextSceneOfSuccess;
            }
            else{
                return level1.nextSceneOfFail;
            }
        }
        //Level2
        else if(Level2State()){
            if(level2.success==true){
                return level2.nextSceneOfSuccess;
            }
            else
                return Bug.nextSceneOfSuccess;
        }
        //Level3
        else if(Level3_1State()){
            if(level3_1.success==true){
                return level3_1.nextSceneOfSuccess;
            }
            else{
                return level3_1.nextSceneOfFail;
            }
        }
        else if(Level3_2State()){
            if(level3_2.success==true){
                return level3_2.nextSceneOfSuccess;
            }
            else{
                return level3_2.nextSceneOfFail;
            }
        }
        //Level4
        else if(Level4State()){
            if(level4.success==true){
                return level4.nextSceneOfSuccess;
            }
            else
                return Bug.nextSceneOfSuccess;
        }
        //Level5
        else if(Level5State())
        {
            if(level5.success==true){
                return level5.nextSceneOfSuccess;
            }
            else
                return level5.nextSceneOfFail;
        }
        //Menu
        else if(MenuState()){
            if(menu.success==true){
                return menu.nextSceneOfSuccess;
            }
            else
                return Bug.nextSceneOfSuccess;
        }
        //Bug {報錯用，理論上不該出現}
        else
        {
            return Bug.nextSceneOfSuccess;
        }
    }

    //各關關卡的狀態設定    {在LevelState使用}
    public bool Level1State(){
        if(level1.switchOpen==true){
            level1.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool Level2State(){
        if(level2.switchOpen==true){
            level2.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool Level3_1State(){
        if(level3_1.switchOpen==true){
            level3_1.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool Level3_2State(){
        if(level3_2.switchOpen==true){
            level3_2.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool Level4State(){
        if(level4.switchOpen==true){
            level4.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool Level5State(){
        if(level5.switchOpen==true){
            level5.switchOpen =false ;
            return true ;
        }
        return false ;
    }
    public bool MenuState(){
        if(menu.switchOpen==true){
            menu.switchOpen =false ;
            return true ;
        }
        return false ;
    }
}