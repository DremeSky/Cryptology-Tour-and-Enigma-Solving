using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDataHolder" ,menuName ="Data/New Data Holder")]
[System.Serializable]
public class DataHolder : ScriptableObject
{
    public List<GameScene> scenes ;
}
