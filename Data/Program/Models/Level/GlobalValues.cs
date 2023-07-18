using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewGlobalValue" ,menuName ="Data/New Global Value")]
[System.Serializable]
public class GlobalValues : ScriptableObject
{
    public bool openMenuLoading =true ;
}
