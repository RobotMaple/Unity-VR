using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Scr_VacShooter : MonoBehaviour
{
    [SerializeField]
    public GameObject VacBag;
    private bool laser = false;
    public GameObject rightHolster;

    // public RaycastHit hit;
    private LineRenderer lr;
    public bool inHand;

    //Gun Vars & Charging Mechanic
    public float charge = 0.0f;
    public float maxCharge = 10.0f;

    public bool charging = false;
    public bool fired = false;
    public float i = 0.0f; //temp var
    public float shootTimer = 2.0f;
    public float targetTime = 0.0f;
    public float speed = 20;
    public ParticleSystem shootParticle;
    //Currentarrow.Vacbag[0] obj temp vars
    public GameObject bul;
    public GameObject b;
    public Transform barrel;
    public Scr_VacSucker Currentarrow;// = VacBag.GetComponent<Scr_VacSucker>();
    //next in mag
    public GameObject Mag;
    public GameObject Nextitems;
    public AudioSource shootClipSource,ClipSource;
    private void Start()
    {
        ClipSource.Play();
        Currentarrow = VacBag.GetComponent<Scr_VacSucker>();
        lr = GetComponent<LineRenderer>();
        Physics.IgnoreLayerCollision(7, 3);
    }

    public void Update()
    {
        showNext(Currentarrow.Vacbag[0]);

        if (!inHand)
        {
            transform.rotation = Quaternion.identity;
            Transform Targetpos = rightHolster.transform;
            transform.position = Targetpos.position;
        }

            
        
        /*Charging*/
        if (charging)
        {
            
            if (Currentarrow.Vacbag[0] != null)
            {
                charge += Time.deltaTime;
                b = Currentarrow.Vacbag[0];
                Currentarrow.Vacbag[0].transform.position = barrel.transform.position;
                Currentarrow.Vacbag[0].transform.rotation = barrel.transform.rotation;
                if (charge >= maxCharge)
                {
                    charge = maxCharge;
                } 
            }
        }else { if (ClipSource.volume != 0) { StartCoroutine(AudioFade.StartFade(ClipSource, .2f, 0f)); }  }

        if (fired)
        { 
            b = null;
            fired = false;
        }
        /*Laser System*/
        #region "Laser System"
        if (laser == true)
        {
            lr.enabled = true;
            //Draw Laser Sights
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                }
            }
            else { lr.SetPosition(1, transform.forward * 5000); }

            lr.SetWidth(0.1f * (charge / maxCharge), 0.1f * (charge / maxCharge));
            Color newColor = lr.material.color;
            newColor.a = 0.7f * (charge / maxCharge);
            lr.material.color = newColor;
            //lr.material.color.a = charge / maxCharge;
        }
        else { lr.enabled = false; }
        #endregion
        /*In Hand*/


        /*Gun Delay*/
        targetTime -= Time.deltaTime;
        i -= Time.deltaTime;
        
    }
    public static void RemoveAt(GameObject[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }
    public void PickedUp() // Picking up VacSystem Shooter
    {
        
        inHand = true;
    }
    public void LetGo() // Letting go VacSystem Shooter
    {
       
        inHand = false;
    }

    public void showNext(GameObject item)
    {
        if (Currentarrow.Vacbag[0] != null)
        {



            Nextitems = item;
            MeshFilter Items = Nextitems.GetComponent<MeshFilter>();
            Mag.GetComponent<MeshFilter>().mesh = Items.mesh;
            Mag.transform.localScale = new Vector3(.15f, .15f, .15f);
            Mag.GetComponent<MeshRenderer>().material = Nextitems.GetComponent<MeshRenderer>().material;
            Mag.transform.Rotate(1f, 1f, 1f);
            Nextitems = null;
        }
        else {

            Nextitems = item;
            MeshFilter Items = null;
            Mag.GetComponent<MeshFilter>().mesh = null;
            Mag.transform.localScale = new Vector3(.15f, .15f, .15f);
            Mag.GetComponent<MeshRenderer>().material = null;
            Nextitems = null;

        }
    }

    public void chargingUp() // 
    {
        laser = true;

        StartCoroutine(AudioFade.StartFade(ClipSource, 1f, .5f));
        charging = true;
            
            if (Currentarrow.Vacbag[0] != null)
            {

                //Storing Scale of item
               // Vector3 itemSize 
                // Currentarrow.Vacbag[0].transform.localScale = new Vector3(0, 0, 0);
                //i = 1.0f;//
                Debug.Log("Shooting: " + Currentarrow.Vacbag[0].gameObject.name);
               // SuckableItems Currentarrow.Vacbag[0] = Currentarrow.Vacbag[0].GetComponent<SuckableItems>();
               
                //GameObject.Destroy(Currentarrow.Vacbag[0]);
                 // removes Color.white.

                Vector3 itemSize = new Vector3(1,1,1);
                Currentarrow.Vacbag[0].SetActive(true);
                Currentarrow.Vacbag[0].GetComponent<SuckableItems>().ItemState = SuckableItems.itemState.Grow;
                //Currentarrow.Vacbag[0].GetComponent<SuckableItems>().Grow(new Vector3(0, 0, 0), itemSize, .5f); // GROW Feature
                Currentarrow.Vacbag[0].GetComponent<Rigidbody>().isKinematic = true;
               
                

            }
        

    }

    public void release()
    {
        StartCoroutine(AudioFade.StartFade(ClipSource, .2f, 0f));
        laser = false;
        if (Currentarrow.Vacbag[0] != null)
        {
                charging = false;

                //Currentarrow.Vacbag[0].GetComponent<Rigidbody>().isKinematic = false;
                if (charge <= 1.0f) { charge = 1f; }
                Fire(Currentarrow.Vacbag[0], charge);
                Debug.Log("Charge: " + charge);
                charge = 0.0f;
                i = 1.0f;//
            
        }
        if (Currentarrow.Vacbag[0]!= null)
        {
           
        }
    }
    public void Fire(GameObject item, float charge)
    {
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Rigidbody>().velocity = (speed * charge) * barrel.forward;
        fired = true;

        for (int a = 0; a < Currentarrow.Vacbag.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            Currentarrow.Vacbag[a] = Currentarrow.Vacbag[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref Currentarrow.Vacbag, Currentarrow.Vacbag.Length - 1);
        Array.Resize(ref Currentarrow.Vacbag, Currentarrow.Vacbag.Length + 1);
        targetTime = shootTimer;
        shootClipSource.Play();
        shootClipSource.volume = charge / maxCharge;
        shootParticle.Play();
    }

    public void ScaleToTarget(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(targetScale, duration));
    }

    private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        yield return null;
    }
}

