using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderS : MonoBehaviour
{
    // Start is called before the first frame update

    public bool active = false;
   

    // Update is called once per frame

     public void OnTriggerEnter(Collider other)
     {
            ModTemplate mod = other.gameObject.GetComponent<ModTemplate>();

        if (mod.stats.mod == ModTemplate.modType.Shoulder)
        {
            active = true;
            Debug.Log("Socket Shoulder Active");
        }
        else { active = false; }

     }
}
