using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPump : MonoBehaviour
{
    //Pump
    [SerializeField]
    public Transform pump;
    [SerializeField]
    public Rigidbody pumpDock;
    public Rigidbody DisDock;
    [SerializeField]
    public GameObject boxDis;
    public bool shootState = false; 
    // Start is called before the first frame update

    public void Update()
    {
        var Pumpanchor = GetComponent<ConfigurableJoint>();
        if (!shootState)
        {
            Pumpanchor.connectedBody = pumpDock;
        }
        else { Pumpanchor.connectedBody = DisDock; }
    }
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == boxDis && shootState == false)
        {
            Debug.Log("pump");
            shootState = true;
}
    }
}
