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
    public float maxCharge = 20.0f;
    public bool charging = false;
    public bool fired = false;
    public float i = 0.0f; //temp var
    private float shootTimer = 2.0f;
    public float targetTime = 0.0f;
    public float speed = 20;
    public GameObject bullet;
    public GameObject bul;
    public GameObject b;
    public Transform barrel;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        Physics.IgnoreLayerCollision(7, 3);
    }
    public void Update()
    {   
        /*Charging*/
        if (charging)
        {
            charge += Time.deltaTime;
            b = bullet;
            b.transform.position = barrel.transform.position;
            b.transform.rotation = barrel.transform.rotation;
            if (charge >= maxCharge)
            {
                charge = maxCharge;
            }
        }

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
        }
        else { lr.enabled = false; }
        #endregion
        /*In Hand*/
        if (!inHand)
        {
            transform.rotation = Quaternion.identity;
            Transform Targetpos = rightHolster.transform;
            transform.position = Targetpos.position;
        }

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
        laser = true;
        inHand = true;
    }
    public void LetGo() // Letting go VacSystem Shooter
    {
        laser = false;
        inHand = false;
    }

    public void chargingUp() // 
    {
        
        if (targetTime <= 0.0f)
        {
            charging = true;
            Scr_VacSucker Currentarrow = VacBag.GetComponent<Scr_VacSucker>();
            if (Currentarrow.Vacbag[0] != null)
            {
                i = 1.0f;
                Debug.Log("Shooting: " + Currentarrow.Vacbag[0].gameObject.name);
                bullet = Instantiate(Currentarrow.Vacbag[0].gameObject);
                SuckableItems curbullet = bullet.GetComponent<SuckableItems>();
                curbullet.ScaleToTarget(new Vector3(1, 1, 1), 1f); // GROW Feature
                Destroy(Currentarrow.Vacbag[0]);
                RemoveAt(Currentarrow.Vacbag, 0); // removes Color.white.

                bullet.GetComponent<Rigidbody>().isKinematic = true;
                bullet.SetActive(enabled);
                b = bullet;
            }
        }

    }

    public void release()
    {
        if (targetTime <= 0.0f)
        {
                charging = false;
                bul = b;
                bul.GetComponent<Rigidbody>().isKinematic = false;
                Fire(bul, charge);
                charge = 0.0f;    
        }
    }
    public void Fire(GameObject item, float charge)
    {
        item.GetComponent<Rigidbody>().velocity = (speed * charge) * barrel.forward;
        fired = true;
        bul = null;
        b = null;
        bullet = null;
        targetTime = shootTimer;
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

