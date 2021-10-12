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
    [SerializeField] public float enemyHealth = 500;
    public bool isDead = false;
    [SerializeField] public float fadePerSecond = 0.5f;
    [SerializeField] public GameObject parent;
    [SerializeField] public Animator AnimatorController;
    [SerializeField] public GameObject Guide;
    [SerializeField] public float attackDis = 1.5f;
    float timer = 0f;
    public float timerT;
    [SerializeField] public ConfigurableJoint stablizer;
    AudioSource audioData;
    public GameObject Tar;
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float m_CurrentClipLength;
    float i = 0;

    private void Start()
    {
        
    }
    private void Update()
    {
        JointDrive AdriveJoint = new JointDrive();
        AdriveJoint.positionSpring = 0.0f;
        AdriveJoint.maximumForce = 0.0f;
        AdriveJoint.positionDamper = 50.0f;
        JointDrive DdriveJoint = new JointDrive();
        DdriveJoint.positionSpring = 10.0f;
        DdriveJoint.maximumForce = 10.0f;
        DdriveJoint.positionDamper = 2.0f;

        var En = Guide.GetComponent<Scr_GuideSystem>().enemyPar;
        Tar = Guide.GetComponent<Scr_GuideSystem>().target;

        if (ragdollP) // checks to see if ragdoll state is active
        {
            AnimatorController.SetBool("ragdoll", true);
            stablizer.angularXDrive = DdriveJoint;
            stablizer.angularYZDrive = DdriveJoint;
            float RagdollTimer = targetTime;
            RagdollTimer -= Time.deltaTime;
            if (RagdollTimer <= 0.0f)
            {
                stablizer.angularXDrive = AdriveJoint;
                stablizer.angularYZDrive = AdriveJoint;
                GetUp();
            }
        }

        if (enemyHealth <= 0)
        {
            isDead = true;
            dying();
        }

        if (Tar == null) // If no Target
        {
            AnimatorController.SetBool("Idle", true); AnimatorController.SetBool("attack", false);
        }
        else if (Tar != null) // If Target
        {
            AnimatorController.SetBool("Idle", false);
            var distance = Vector3.Distance(En.transform.position, Tar.transform.position);
                do{
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
                    { attack(); }
                } while (distance <= attackDis);
            if (distance >= attackDis) // Within attack distance of Target
            {AnimatorController.SetBool("attack", false);}
        }
    }

    public void GetUp() {
        AnimatorController.SetBool("Getting up", true);
        AnimatorController.SetBool("ragdoll", false);
        if (AnimatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                AnimatorController.SetBool("attack", false);
                ragdollP = false;
            }
    }

    public void attack()
    {
        Tar.GetComponent<scr_playerVars>().gotHitP();
        Debug.Log("gothitplayer");
        i = 0;
    }
    public void GotHit()
    {
        enemyHealth -= 30;
        ragdollP = true;
        AudioClip aClip = GetComponent<RandomAudioClip>().GetRandomAudioClip();
        audioData = GetComponent<AudioSource>();
        audioData.clip = aClip;
        audioData.Play();
    }

   public void dying(){
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
