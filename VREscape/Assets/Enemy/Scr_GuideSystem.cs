
using UnityEngine;
using UnityEngine.AI;
public class Scr_GuideSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject target = null;
    public NavMeshAgent agent;
    public float maxDis = 0.5f;
    public float jumpspd = 2.0f;
    public GameObject enemyPar;
    public Rigidbody enemybody;
    public Scr_EnemyAI enemy;
    public float dis;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

    }
   public void FixedUpdate()
    {
        
        
        //i -= Time.deltaTime;
        Vector3 targetPos = target.transform.position;

        agent.SetDestination(targetPos);

        dis = Vector3.Distance(gameObject.transform.position, enemyPar.transform.position);
        //Debug.Log("dis =" + dis);
        if (dis > maxDis)
        {
            agent.speed = 0f;
          //  agent.SetDestination(enemybody.position);
            jump();
        }
        else { agent.speed = 2;  }
        

    }
    public void jump()
    {
        bool ragdollstate = enemy.ragdollP;
        if (ragdollstate == false)
        {
            enemybody.AddForce((transform.position - enemybody.transform.position) * (jumpspd));// Debug.Log("Jump");
            enemybody.AddForce(transform.up * jumpspd);
        }
    }
}
