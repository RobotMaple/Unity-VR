using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;
public class SuckableItems : MonoBehaviour
{
    public bool beingSucked;
    public float speed = 1;
    public GameObject target;
    public GameObject nozzle;
    public float i = 0.0f;

    // Start is called before the first frame update
    //sucking vars
    public bool sucking;
    public GameObject vacSucker;
    public void Start()
    {
           
           target = gameObject;
        nozzle = GameObject.Find("Nozzle");
    }

    // Update is called once per frame
    public void Update()
    {
        beingSucked = false;
        i -= Time.deltaTime;
        //Sucked();
        Vector3 relativePos = nozzle.transform.position - transform.position;
        sucking = nozzle.GetComponent<Scr_VacSucker>().sucking;
        if (beingSucked && sucking)
        {
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 6 * Time.deltaTime);
           // lookAtSlowly(transform, new Vector3(0, 0, 0), 15);
            FollowTargetWithRotation(target.gameObject, nozzle.transform, speed);
        }
    }
    public void FollowTargetWithRotation(GameObject target, Transform endPos, float SuckSpeed)
    {
        
        Vector3 newPosition = target.transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);

    }
    public IEnumerator Shrink(GameObject item, Vector3 startScale, Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(startScale, targetScale, duration));

        yield return new WaitForSeconds(.5f); GameObject.Destroy(gameObject);
    }
    public void Sucked()
    {
        beingSucked = true;
        FollowTargetWithRotation(target.gameObject, nozzle.transform, speed);
        
        
    }
    public void ScaleToTarget(Vector3 startScale, Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(startScale,targetScale, duration));
        // Object.Destroy(gameObject);
        //gameObject.SetActive(false);yield return null;
        
    }

    private IEnumerator ScaleToTargetCoroutine(Vector3 startScale, Vector3 targetScale, float duration)
    {
        //Vector3 startScale = transform.localScale;
        //transform.localScale = new Vector3( 0,0,0);
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t); Debug.Log("Bul Size Scaling up" + startScale + transform.localScale);
            yield return null;
        }
        yield return null;
    }
}
