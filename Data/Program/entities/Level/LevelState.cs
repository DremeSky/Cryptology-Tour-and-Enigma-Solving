using UnityEngine;
using UnityEngine.SceneManagement ;

public class LevelState : MonoBehaviour
{
    public Level level1 ;

    public bool Level1State(){
        if(level1.switchOpen==true){
            level1.switchOpen =false ;
            return true ;
        }
        return false ;
    }
}
