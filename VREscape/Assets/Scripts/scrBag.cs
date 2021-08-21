using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;

public class scrBag : MonoBehaviour
{
    //public GameObject[] bag = new GameObject[10];
    public List<GameObject> bag = new List<GameObject>(10);
    // Start is called before the first frame update
    [SerializeField]
    public GameObject spark;
    [SerializeField] private float speed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            for (int i = 0; i < bag.Count; i++)
            {
                if (bag[i] == null)
                {
                    bag[i] = other.gameObject;
                    Debug.Log("slot " + i + " = " + bag[i]);
                    other.gameObject.SetActive(false);
                    ParticleSystem an = spark.GetComponent<ParticleSystem>();
                    an.Play();
                    break;

                }

            }
        }
    }
}
