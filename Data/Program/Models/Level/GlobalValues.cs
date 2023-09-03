using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewGlobalValue" ,menuName ="Data/New Global Value")]
[System.Serializable]
public class GlobalValues : ScriptableObject
{
    //Menu
        //封面圖片
            public Sprite MenuSprite1;
            public Sprite MenuSprite2;
            public Sprite MenuSprite3;
            public Sprite MenuSprite4;

        //Load
            //遊戲場景，用以紀載切換之場景。
                public GameScene switchScene_GameGroup1;
                public GameScene switchScene_GameGroup2;
                public GameScene switchScene_GameGroup3;
                public GameScene switchScene_GameGroup4;
                public GameScene switchScene_GameGroup5;
                public GameScene switchScene_GameEnd;   //後記
                public string switchScene_LevelGroup1;
                public string switchScene_LevelGroup2;
                public string switchScene_LevelGroup3;
                public string switchScene_LevelGroup4;
                public string switchScene_LevelGroup5;
        
        //操作說明
            public GameScene switchScene_GameExplain;
}
