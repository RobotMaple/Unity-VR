using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_EnemyAI : MonoBehaviour
{

 /*Scr_EnemyAI
 * Purpose: Controls Enemy Statemachine
 * Date: 14/7/2021
 */
    public bool ragdollP = false;
    public float targetTime = 5;
    [SerializeField]  public float enemyHealth = 500;
    public bool isDead = false;
    [SerializeField] public float fadePerSecond = 0.5f;
    [SerializeField]  public GameObject parent;
    [SerializeField] public Animator AnimatorController;
    [SerializeField] public GameObject Guide;
    [SerializeField] public float attackDis = 1.5f;
    float timer = 0f;
    public float timerT;
    [SerializeField] public ConfigurableJoint stablizer;

    private void Update()
    {
        JointDrive AdriveJoint = new JointDrive();
        AdriveJoint.positionSpring = 1000000.0f;
        AdriveJoint.maximumForce = 100000000.0f;
        AdriveJoint.positionDamper = 10.0f;
        JointDrive DdriveJoint = new JointDrive();
        DdriveJoint.positionSpring = 0.0f;
        DdriveJoint.maximumForce = 0.0f;
        DdriveJoint.positionDamper = 0.0f;
        //stablizer.angularXDrive = AdriveJoint;
        //stablizer.angularYZDrive = AdriveJoint;
        if (ragdollP) // checks to see if ragdoll state is active
        {

             
            stablizer.angularXDrive = DdriveJoint;
            stablizer.angularYZDrive = DdriveJoint;
            targetTime -= Time.deltaTime;

            if (enemyHealth > 0 && isDead == false)
            {
                if (targetTime <= 0.0f)
                {
                    AnimatorController.SetBool("ragdoll", true);
                    
                    timerT =timer += Time.deltaTime;
                    if (timerT >= 3)
                    {

                        stablizer.angularXDrive = AdriveJoint;
                        stablizer.angularYZDrive = AdriveJoint;
                        AnimatorController.SetBool("ragdoll", false);
                            AnimatorController.SetBool("attack", false);

                        timer = 0;
                            ragdollP = false;
                        
                    }
                    /*      ragdollP = false;
                     *      AnimatorController.SetBool("ragdoll", false);
                    AnimatorController.SetBool("attack", false);*/
                    //stablizer.angularXDrive = AdriveJoint;
                    //stablizer.angularYZDrive = AdriveJoint;
                }
            }

            // gameObject.GetComponentInChildren<copyanimation>().ragdoll = true;

        }
        if (enemyHealth <= 0)
        {
            isDead = true;
            dying();
        }
        var En = Guide.GetComponent<Scr_GuideSystem>().enemyPar;
        var Tar = Guide.GetComponent<Scr_GuideSystem>().target;
        var distance = Vector3.Distance(En.transform.position, Tar.transform.position);
      //  Debug.Log("asd" + distance);

        

        if (distance <= attackDis)
        {


            AnimatorController.SetBool("attack", true);
        }
        else
        { AnimatorController.SetBool("attack", false); }

    }
    public void GotHit()
    {

        enemyHealth -= 30;
        ragdollP = true;
        Debug.Log("gothit");
        targetTime = 5;

    }
    public void dying()
    {
        ragdollP = true;
        Debug.Log("gothit");
        var skin = GetComponentInChildren<Renderer>().material;
        var color = skin.color;
        MaterialModify.SetMaterialRenderingMode(skin, MaterialModify.RenderingMode.Fade);
        float time; time = fadePerSecond * Time.deltaTime;
        skin.color = new Color(color.r, color.g, color.b, color.a - time);
        if (skin.color.a <= 0) { Destroy(parent); }
    }
}
