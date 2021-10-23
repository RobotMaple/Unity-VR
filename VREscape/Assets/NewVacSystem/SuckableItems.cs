using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;
public class SuckableItems : MonoBehaviour
{
    public bool beingSucked;
    public float speed = 6; // Suck Speed
    public GameObject target;
    public GameObject nozzle;
    public float i = 0.0f;
    // Start is called before the first frame update
    //sucking vars
    public bool sucking;
    public GameObject vacSucker;

    public enum itemState
    { 
        Idle,
        Following,
        Shrink,
        Grow,
        hidden
    };

    public itemState ItemState;
    public void Start()
    {
        ItemState = itemState.Idle;
        target = gameObject;
        nozzle = GameObject.Find("Nozzle");
    }
    Collider objCollider;// = GetComponent<Collider>();
    // Update is called once per frame
    public void Update()
    {
        objCollider = GetComponent<Collider>();
        switch (ItemState)
        {
            case itemState.Idle:
                
                break;
            case itemState.Following:


                Sucked();
                break;
            case itemState.Shrink:
                 Shrink(gameObject.transform.localScale, new Vector3(0, 0, 0), .5f);
                beingSucked = true;
                this.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case itemState.Grow:
                gameObject.SetActive(true);
                Grow(new Vector3(0, 0, 0), new Vector3(1, 1, 1), .5f);
                break;
            case itemState.hidden:
                beingSucked = false;
                gameObject.SetActive(false);
                break;

        }


        beingSucked = false;
        i -= Time.deltaTime;

        
        sucking = nozzle.GetComponent<Scr_VacSucker>().sucking; // Bool State 

        // 
        if (beingSucked && sucking)
        {

            FollowTargetWithRotation(target.gameObject, nozzle.transform, speed);
        }
    }
    public void FollowTargetWithRotation(GameObject target, Transform endPos, float SuckSpeed)
    {
        float tempSpeed = 0;
        if (!nozzle.GetComponent<Scr_VacSucker>().isFull)
        {
            Vector3 relativePos = nozzle.transform.position - transform.position; // getting angle for shooting
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 2 * Time.deltaTime);
            Vector3 newPosition = target.transform.position;
            float step = tempSpeed + (speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);
        }else { ItemState = SuckableItems.itemState.Idle; }
    }
    public void Shrink(Vector3 startScale, Vector3 targetScale, float duration)
    {
        
        objCollider.enabled = false;
        StartCoroutine(ShrinkScaleToTargetCoroutine(startScale, targetScale, duration));
        

    }
    public void Sucked()
    {
        
        FollowTargetWithRotation(target.gameObject, nozzle.transform, speed);
    }
    public void Grow(Vector3 startScale, Vector3 targetScale, float duration)
    {
        objCollider.enabled = true;
        //GrowShrink = true;
        StartCoroutine(GrowScaleToTargetCoroutine(startScale,targetScale, duration)); 
    }

    public IEnumerator ShrinkScaleToTargetCoroutine(Vector3 startScale, Vector3 targetScale, float duration)
    {
        float timer = 0.0f;
        while (timer < duration)//
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t); Debug.Log("Bul Size Scaling up" + startScale + transform.localScale);
            yield return null ; 
        } 
        yield return new WaitForSeconds(duration);
        ItemState = SuckableItems.itemState.hidden;
    }

    public IEnumerator GrowScaleToTargetCoroutine(Vector3 startScale, Vector3 targetScale, float duration)
    {
        float timer = 0.0f;
        while (timer < duration)//
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t); Debug.Log("Bul Size Scaling up" + startScale + transform.localScale);
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        
        ItemState = itemState.Idle;
    }

}
