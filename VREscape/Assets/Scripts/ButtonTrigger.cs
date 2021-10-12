using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] boxPrefab;
    [SerializeField]
    private Transform[] spawnPoint;
    [SerializeField]
    GameObject Button;
    bool isOpen = false;
    public int ir,iri;
    void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject == Button)
        {
            ir = Random.Range(0, spawnPoint.Length);
            iri = Random.Range(0, boxPrefab.Length);
            GameObject t = Instantiate(boxPrefab[iri]);

             t.transform.position = spawnPoint[ir].position;

        }
    }
}
