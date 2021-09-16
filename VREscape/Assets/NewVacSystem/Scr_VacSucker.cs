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
        if(sucking && Vacbag[Vacbag.Length - 1] == null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                for (int i = 0; i <= Vacbag.Length; i++)
                {
                    if (Vacbag[i] == null )
                    {
                        // Item starts to shrink
                        StartCoroutine(other.gameObject.GetComponent<SuckableItems>().Shrink(other.gameObject, other.gameObject.transform.localScale, new Vector3(0, 0, 0), .2f));
                        
                        Vacbag[i] = Instantiate(other.gameObject);
                        Vacbag[i].SetActive(false);

                        
                        Debug.Log("slot " + i + " = " + Vacbag[i]);
                        ParticleSystem an = spark.GetComponent<ParticleSystem>();
                        an.Play();
                        break;

                    }
                }
            }
        }
    }
}
