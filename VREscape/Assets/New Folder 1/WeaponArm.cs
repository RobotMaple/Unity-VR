using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponArm : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    [SerializeField]
    private GameObject Bag;
    [SerializeField]
    private GameObject pump;
    [SerializeField]
    private XRController controller;

    // public RaycastHit hit;
    private LineRenderer lr;
    
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        Physics.IgnoreLayerCollision(7, 3);
    }
    public void Update()
    {
        //Draw Laser Sights
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit)){
            if (hit.collider){
                lr.SetPosition(1, hit.point);
            }
        }else{lr.SetPosition(1, transform.forward * 5000);}
        
    }
    public void Fire()
    {
        Debug.Log("Trigger Pulled - Fire Script Ran");
       WeaponPump pumpstate = pump.GetComponent<WeaponPump>();
        scrBag Currentarrow = Bag.GetComponent<scrBag>();
       Debug.Log("Shoot State: " + pumpstate.shootState);

      if (pumpstate.shootState == true)
        {
            if (Currentarrow.bag[0] != null)
            {
                Debug.Log("Shooting: "+ Currentarrow.bag[0].gameObject.name);
                bullet = Instantiate(Currentarrow.bag[0].gameObject);
                Destroy(Currentarrow.bag[0]);
                Currentarrow.bag.RemoveAt(0);
                
                bullet.transform.position = barrel.transform.position;
                bullet.transform.rotation = barrel.transform.rotation;
                bullet.gameObject.SetActive(true);
            //   bullet.GetComponent<Rigidbody>().isKinematic = false;
                bullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
                pumpstate.shootState = false;
            }

        }
            /*
            if (pumpstate.shootState == true) { 
                for (int i = 0; i < 10; i++)
                {

                    if (Currentarrow.bag[i] != null)
                    {
                        bullet = Instantiate(Currentarrow.bag[i].gameObject);
                        bullet.gameObject.SetActive(true);
                        bullet.transform.position = barrel.transform.position;
                        bullet.transform.rotation = barrel.transform.rotation;
                        bullet.GetComponent<Rigidbody>().isKinematic = false;
                        bullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
                        Destroy(Currentarrow.bag[i]);
                        Currentarrow.bag.RemoveAt(i);//Currentarrow.bag[i] = null;


                        break;
                    }
                }
                pumpstate.shootState = false;
            }*/

        }




}

