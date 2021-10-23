using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyanimation : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint m_ConfigurableJoint;
    public AudioClip HitSound;
    public AudioSource RootSource;
    [SerializeField]
    public GameObject Main;


    Quaternion targetInitialRotation;
    [SerializeField]
    public bool ragdoll;
    float vel;
    // Start is called before the first frame update
    void Start()
    {

        this.m_ConfigurableJoint = this.GetComponent<ConfigurableJoint>();
        this.targetInitialRotation = this.targetLimb.transform.localRotation;

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {

        ragdoll = Main.GetComponent<Scr_EnemyAI>().ragdollP;
        //Config for when enemy is NOT in ragdoll state joints strength is active
        JointDrive driveJoint = new JointDrive();
        driveJoint.positionSpring = 4000.0f;
        driveJoint.maximumForce = 4000.0f;
        driveJoint.positionDamper = 50.0f;
        //Config for when enemy is in ragdoll state joints strength is lowered
        JointDrive RdriveJoint = new JointDrive();
        RdriveJoint.positionSpring = 40.0f;
        RdriveJoint.maximumForce = 40.0f;
        RdriveJoint.positionDamper = 50.0f;

        ConfigurableJoint joint = gameObject.GetComponent<ConfigurableJoint>();
        //Debug.Log("joint" + joint.angularXDrive.positionSpring);

        if (!Main.GetComponent<Scr_EnemyAI>().AnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Ragdoll")) // When Enemy Is active and timer 
        {
            this.m_ConfigurableJoint.targetRotation = copyRotation();
            joint.angularXDrive = driveJoint;
            joint.angularYZDrive = driveJoint;
        }
        else
        {
            joint.angularXDrive = RdriveJoint;
            joint.angularYZDrive = RdriveJoint;
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        //Debug.Log("mag" + vel.magnitude); 
       
        if (other.gameObject.layer == LayerMask.NameToLayer("item") && other.rigidbody.velocity.magnitude > 1.5f)
        {
            float audioLevel = other.relativeVelocity.magnitude / 10.0f;
            RootSource.GetComponent<AudioSource>().PlayOneShot(HitSound, audioLevel);
            Main.GetComponent<Scr_EnemyAI>().GotHit();



        }
        if (other.rigidbody.velocity.magnitude > 0.5f)
        {
            float audioLevel = other.relativeVelocity.magnitude / 15.0f;
            RootSource.GetComponent<AudioSource>().PlayOneShot(HitSound, audioLevel);
        }        //Collider myCollider = collision.contacts[0].thisCollider;
    }

    private Quaternion copyRotation()
    {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
    }
}