using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform boxPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    GameObject Button;
    bool isOpen = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Button)
        {

            Transform t = Instantiate(boxPrefab);

             t.position = spawnPoint.position;

        }
    }
}
