using UnityEngine;
using System.Collections.Generic ;

[CreateAssetMenu(fileName ="NewLevel" ,menuName ="Data/New Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    public string answer ;
    public int time ;
    public bool switchOpen ;
    public bool success =true ;
    public GameScene nextSceneOfSuccess ;
    public GameScene nextSceneOfFail ;
}
