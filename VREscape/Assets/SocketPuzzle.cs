using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SocketPuzzle : MonoBehaviour
{
    
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject doorpos;
    [SerializeField]
    private AudioClip audioData;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public GameObject FireworksAll;

    bool key;
    private void Start()
    {
        key = true;
    }

    void OnTriggerEnter(Collider other)
    {

        key = true;
        Debug.Log("Key True");
        door.transform.position += new Vector3(0, 4, 0);
    }
    void OnTriggerExit(Collider other)
    {
        key = false;

        Debug.Log("Key False");
        Explode();
        door.transform.position = doorpos.transform.position;
    }
    void Explode()
    {
        audioSource.Play();
        GameObject firework = Instantiate(FireworksAll, doorpos.transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }
    /*void Update()
    {
        if (key == true)
        { door.transform.position += new Vector3(0, 4, 0); }
        else
        { door.transform.position = doorpos.transform.position;
            
            Explode();
        }
    }*/
}
