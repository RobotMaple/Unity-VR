using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_EnemyAI : MonoBehaviour
{

    /*Scr_EnemyAI
    * Purpose: Controls Enemy Statemachine
    * Date: 14/10/2021
    */
    public bool ragdollP = false;
    public float targetTime = 5;
    [SerializeField] public float enemyHealth = 500;
    public float maxHealth = 500;
    public GameObject HealthBar;
    public Slider slider;
    public bool isDead = false;
    [SerializeField] public float fadePerSecond = 0.5f;
    [SerializeField] public GameObject parent;
    [SerializeField] public Animator AnimatorController;
    [SerializeField] public GameObject Guide;
    [SerializeField] public float attackDis = 2.5f;
    float timer = 0f;
    public float timerT;

    public AudioSource audioData, HaudioData;
    public RandomAudioClip aLibAudio, HLibAudio;
    public AudioClip HitSound; 
    [SerializeField] public ConfigurableJoint stablizer;

    JointDrive AdriveJoint = new JointDrive();
    JointDrive DdriveJoint = new JointDrive();

    // 
    public GameObject Tar;
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float m_CurrentClipLength;
    float i = 0;

    //NewState Enum 
    public enum EnemyState
    {
        Idle = 1,
        Walking = 2,
        Attack = 3,
        Ragdoll = 4 ,
        Dead = 5
    };

    public EnemyState enemyState;
    public void Start()
    {
        HealthBar.SetActive(true);
        
        enemyState = EnemyState.Idle;
    }
    void Update()
    {
        slider.value = enemyHealth / maxHealth;
        AdriveJoint.positionSpring = 100.0f;
        AdriveJoint.maximumForce = 100.0f;
        AdriveJoint.positionDamper = 100.0f;
        

        DdriveJoint.positionSpring = 0.0f;
        DdriveJoint.maximumForce = 0.0f;
        DdriveJoint.positionDamper = 00.0f;
        //stablizer.angularXDrive = AdriveJoint;
        //stablizer.angularYZDrive = AdriveJoint;

        var En = Guide.GetComponent<Scr_GuideSystem>().enemyPar;
        Tar = Guide.GetComponent<Scr_GuideSystem>().target;
        var distance = Vector3.Distance(En.transform.position, Tar.transform.position);


        if (ragdollP) // checks to see if ragdoll state is active
        { enemyState = EnemyState.Ragdoll; }

        //DeadCheck
        if (enemyHealth <= 0)
        { enemyState = EnemyState.Dead; }
        //Enemy State Machine
        switch (enemyState) 
        {
            //////////////////////////////////////#IDLE#/////////////////////////////////////////////////////////////////
            case EnemyState.Idle: 
                if (Tar != null)
                { enemyState = EnemyState.Walking; }
                else { enemyState = EnemyState.Idle; }
                AnimatorController.SetBool("ragdoll", false);
                AnimatorController.SetBool("Idle", true);
                AnimatorController.SetBool("attack", false);
                AnimatorController.SetBool("Getting up", false);
                break;

            //////////////////////////////////////#Walking#/////////////////////////////////////////////////////////////////
            case EnemyState.Walking:
                
                AnimatorController.SetBool("Idle", false);
                if (distance <= attackDis)
                {
                    enemyState = EnemyState.Attack;
                }
                else
                { AnimatorController.SetBool("attack", false); }
                break;
            //////////////////////////////////////#Attack#/////////////////////////////////////////////////////////////////
            case EnemyState.Attack:
                AnimatorController.SetBool("attack", true);
                // Get them_Animator, which you attach to the GameObject you intend to animate.
                m_Animator = AnimatorController.gameObject.GetComponent<Animator>();
                //Fetch the current Animation clip information for the base layer
                m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);

                //Access the current length of the clip
                m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
                //Access the Animation clip name

                Debug.Log(m_ClipName);
                i += Time.deltaTime;
                if (i >= m_CurrentClipLength && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    attack();
                }
                if (distance > attackDis)
                { AnimatorController.SetBool("attack", false); enemyState = EnemyState.Walking; }
                break;
            //////////////////////////////////////#Ragdoll#/////////////////////////////////////////////////////////////////
            case EnemyState.Ragdoll:
                stablizer.angularXMotion = ConfigurableJointMotion.Free;
                stablizer.angularZMotion = ConfigurableJointMotion.Free;
                stablizer.angularYMotion = ConfigurableJointMotion.Free;
                stablizer.angularXDrive = DdriveJoint;
                    stablizer.angularYZDrive = DdriveJoint; AnimatorController.SetBool("ragdoll", true);
                targetTime -= Time.deltaTime;
                    if (enemyHealth > 0 && isDead == false)
                    {
                        if (targetTime <= 0.0f)
                        {
                            timerT = timer += Time.deltaTime; // THIS Shit is weird, why do we have 3 vars for a ragdoll timer. Also shouldn't we be using the animation check instead
                            if (timerT >= 3)
                            {
                                AnimatorController.SetBool("ragdoll", false);
                                AnimatorController.SetBool("Getting up", true);
                                AnimatorController.SetBool("attack", false);
                                StartCoroutine(getup());
   
                            }
                        }
                    }
                
                break;

            case EnemyState.Dead:
                HealthBar.SetActive(false);
                isDead = true;
                dying();
                break;

        }


       

    }
    public IEnumerator getup()
    {

        Debug.Log("getup running");

        if (this.AnimatorController.GetCurrentAnimatorStateInfo(0).IsName("getup"))
        {
            AnimatorController.SetBool("Getting up", false);
            
            stablizer.angularXDrive = AdriveJoint;
            stablizer.angularYZDrive = AdriveJoint;
            stablizer.angularXMotion = ConfigurableJointMotion.Limited;
            stablizer.angularZMotion = ConfigurableJointMotion.Limited;
            stablizer.angularYMotion = ConfigurableJointMotion.Limited;
            Debug.Log("getup finished");

            if (Tar != null)
            { AnimatorController.SetBool("Idle", false); enemyState = EnemyState.Walking; }
            else { AnimatorController.SetBool("Idle", true); enemyState = EnemyState.Idle; }
        }

        timer = 0;
        ragdollP = false;
        yield return null;
    }
    public void attack()
    {
        Tar.GetComponent<scr_playerVars>().gotHitP();
        Debug.Log("gothitplayer");
        i = 0;
    }
    public void GotHit()
    {
        enemyState = EnemyState.Attack;
        if (!ragdollP)
        {

            enemyHealth -= 30;
            ragdollP = true;
            Debug.Log("gothit");
            // Audio Stuff
            AudioClip aClip = aLibAudio.GetComponent<RandomAudioClip>().GetRandomAudioClip();
            audioData.clip = aClip;
            audioData.Play();

            targetTime = 5;
        }

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