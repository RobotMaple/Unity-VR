using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyanimation : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint m_ConfigurableJoint;

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
        JointDrive driveJoint = new JointDrive();
        driveJoint.positionSpring = 1000.0f;
        driveJoint.maximumForce = 1000.0f;
        driveJoint.positionDamper = 50.0f;
        JointDrive RdriveJoint = new JointDrive();
        RdriveJoint.positionSpring = 10.0f;
        RdriveJoint.maximumForce = 10.0f;
        RdriveJoint.positionDamper = 1.0f;

        ConfigurableJoint joint = gameObject.GetComponent<ConfigurableJoint>();
        //Debug.Log("joint" + joint.angularXDrive.positionSpring);

        if (!ragdoll && Main.GetComponent<Scr_EnemyAI>().timerT >= 3)
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

            Debug.Log("test " + ragdoll);


            Main.GetComponent<Scr_EnemyAI>().GotHit();



        }
        //Collider myCollider = collision.contacts[0].thisCollider;
    }

    private Quaternion copyRotation()
    {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
    }
}