using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewStoryScene" ,menuName ="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence> sentences ;
    public Sprite background ;
    public GameScene nextScene ;
    public string gameScene ;
    public bool autoPlay =false ;
    
    [System.Serializable]
    public struct Sentence
    {
        public string text ;
        public Speaker speaker ;
        public List<Action> actions ;
        public bool autoPlay ;

        public AudioClip music ;
        public AudioClip sound ;

        [System.Serializable]
        public struct Action
        {
            public Speaker speaker ;
            public int spriteIndex ;
            public Type actionType ;
            public Vector2 coords ;
            public float moveSpeed ;

            [System.Serializable]

            public enum Type
            {
                NONE ,APPEAR ,MOVE ,DISAPPEAR
            }
        }
    }
}

public class GameScene : ScriptableObject{  }