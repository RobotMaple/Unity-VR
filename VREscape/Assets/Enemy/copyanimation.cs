using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyanimation : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint m_ConfigurableJoint;

    [SerializeField ]
    public GameObject Main;
    public JointDrive driveJoint = new JointDrive();
    public float max = 1500.0f;
    public float dlam = 10.0f;

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

    public void Update()
    {

        ragdoll = Main.GetComponent<Scr_EnemyAI>().ragdollP;
       // JointDrive driveJoint  
        driveJoint.positionSpring = max;
        driveJoint.maximumForce = max;
        driveJoint.positionDamper = dlam;
        JointDrive RdriveJoint = new JointDrive();
        RdriveJoint.positionSpring = 10.0f;
        RdriveJoint.maximumForce = 10.0f;
        RdriveJoint.positionDamper = 1.0f;

        ConfigurableJoint joint = gameObject.GetComponent<ConfigurableJoint>();
        //Debug.Log("joint" + joint.angularXDrive.positionSpring);

        if (!ragdoll)
        {
            this.m_ConfigurableJoint.targetRotation = copyRotation();
            joint.angularXDrive = driveJoint;
            joint.angularYZDrive = driveJoint;



        }
        else {
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