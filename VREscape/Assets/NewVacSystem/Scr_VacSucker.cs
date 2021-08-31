using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;

public class Scr_VacSucker : MonoBehaviour
{
    public GameObject[] Vacbag = new GameObject[10];
    // public GameObject[] bag ;
    // Start is called before the first frame update
   
    [SerializeField]
    public GameObject spark;
    [SerializeField] private float speed;
    public bool sucking;
    public GameObject vacSucker;
    public void Update()
    {
        sucking = vacSucker.GetComponent<Scr_VacSuckerZone>().suck; // bool state for if player is pulling trigger
    }
    public void OnTriggerEnter(Collider other)
    {
        if(sucking)
        { 
            SuckableItems item = other.GetComponent<SuckableItems>();
            Debug.Log("hit det");
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                for (int i = 0; i <= 10; i++)
                {
                    if (Vacbag[i] == null && Vacbag[-1] == null)
                    {
                        Vacbag[i] = other.gameObject;
                        Debug.Log("slot " + i + " = " + Vacbag[i]);
                        other.GetComponent<SuckableItems>().Sucked();
                        
                        ParticleSystem an = spark.GetComponent<ParticleSystem>();
                        an.Play();
                        break;

                    }

                }
            }
        }
    }
}
