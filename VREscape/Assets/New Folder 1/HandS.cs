using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandS : MonoBehaviour
{
    // Start is called before the first frame update

       public bool active = false;
        

    // Update is called once per frame

     public void OnTriggerEnter(Collider other)
     {
            ModTemplate mod = other.gameObject.GetComponent<ModTemplate>();

            if (mod.stats.mod == ModTemplate.modType.Hand)
            {
                active = true;
                Debug.Log("Socket Hand Active");
            }
        else { active = false; }

    }
}
