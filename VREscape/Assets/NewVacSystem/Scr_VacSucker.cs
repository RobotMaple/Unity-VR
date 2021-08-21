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


    public void OnTriggerEnter(Collider other)
    {
        SuckableItems item = other.GetComponent<SuckableItems>();
        Debug.Log("hit det");
        if (other.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            for (int i = 0; i < Vacbag.Length; i++)
            {
                if (Vacbag[i] == null)
                {
                    Vacbag[i] = other.gameObject;
                    Debug.Log("slot " + i + " = " + Vacbag[i]);
                    item.Sucked();
                    //other.gameObject.SetActive(false);
                    ParticleSystem an = spark.GetComponent<ParticleSystem>();
                    an.Play();
                    break;

                }

            }
        }//
    }
}
