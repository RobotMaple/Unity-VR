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
    public bool sucking, isFull;
    public GameObject vacSucker;//

    public void Update()//
    {
        sucking = vacSucker.GetComponent<Scr_VacSuckerZone>().suck; // bool state for if player is pulling trigger
        if (Vacbag[Vacbag.Length - 1] == null) { isFull = false; } else { isFull = true; }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (sucking && !isFull)//
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                for (int i = 0; i <= Vacbag.Length; i++)
                {
                    if (Vacbag[i] == null)
                    {
                        // Item starts to shrink
                        if (!other.GetComponent<SuckableItems>().beingSucked && other.GetComponent<SuckableItems>().ItemState == SuckableItems.itemState.Following)
                        {
                            Vacbag[i] = other.gameObject;
                            //Vacbag[i].GetComponent<SuckableItems>().ItemState = SuckableItems.itemState.hidden;
                            Vacbag[i].GetComponent<SuckableItems>().ItemState = SuckableItems.itemState.Shrink;
                            ParticleSystem an = spark.GetComponent<ParticleSystem>();
                            an.Play();
                            this.GetComponent<AudioSource>().Play();
                        }
                        Debug.Log("slot " + i + " = " + Vacbag[i]);
                        break;
                    }
                }
            }
        }
    }
}
