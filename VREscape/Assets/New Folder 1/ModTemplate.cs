using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


using UnityEngine;
public class ModTemplate : MonoBehaviour
{
    public enum modType {Hand,Arm,Shoulder};

    [Serializable]
    public class ModTypes
    {
        [Tooltip("Type of Mod")]
        public modType mod;
    }
    [SerializeField]
    public ModTypes stats;

}




