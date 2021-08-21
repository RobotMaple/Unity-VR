using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{

    [SerializeField]
    private Transform Key;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public GameObject Boom;


    Transform spawnPoint;
    void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        //if (other.gameObject.layer == LayerMask.NameToLayer("Grab"))
        // {
        if (other.gameObject.layer == LayerMask.NameToLayer("Destroy"))
        {
            Debug.Log("Rock Hit");
            SmallExplode(other.transform);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Vase")
            {
            Debug.Log("Vase Hit");
            Vector3 spawnPoint = other.transform.position;
                Instantiate(Key,spawnPoint,other.transform.rotation);
                SmallExplode(other.transform);
                Destroy(other.gameObject);

                
            }
             
       // }
    }

    void SmallExplode(Transform hitpos)
    {
        audioSource.Play();
        GameObject firework = Instantiate(Boom, hitpos.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }
}
