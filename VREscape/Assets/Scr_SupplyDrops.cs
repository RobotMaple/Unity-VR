using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SupplyDrops : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] itemDrops; // Array of items launched in collision
    public GameObject enemyDrop; // Enemy created on hit
    public GameObject CurItem;
    [SerializeField]
    public int item_amount = 5;
    public int enemy_amount = 5;
    public int ir, iri;

    public void OnHit()
    {
        for (int i = 0; i <= item_amount; i++)
        {
           ir = Random.Range(0, itemDrops.Length);
            CurItem = Instantiate(itemDrops[ir]);
            CurItem.transform.position = transform.position;
            OnLaunch(CurItem);
        }
    }

    public void OnLaunch(GameObject itemL)
    {

        itemL.GetComponent<Rigidbody>().AddForce(itemL.transform.up * 10);
        itemL.GetComponent<Rigidbody>().AddForce(itemL.transform.right * Random.Range(-10,10));
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit();
    }

}
