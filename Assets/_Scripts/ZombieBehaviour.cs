using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    WALK,
    JUMP,
    PUNCH
}

public class ZombieBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
   // public LayerMask collisionLayer;
    //public Vector3 LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);
    public bool HasLOS;
    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;

    [Header("Attack")]
    public float attackDistance;
    public PlayerBehaviour playerBehaviour;
    public float damageDelay = 1.0f;
    public bool IsAttacking = false;
    public float kickForce = 10f;
    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, transform.localScale, transform.forward, transform.rotation, 10.0f, collisionLayer);
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        }


        if (HasLOS && distanceToPlayer < attackDistance && !IsAttacking)
        {
            
            animator.SetInteger("AnimState", (int)CryptoState.PUNCH);
            transform.LookAt(transform.position - player.transform.forward);

         
            DoPunchDamage();
            IsAttacking = true;


            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)CryptoState.JUMP);
            }

        }
        else if(HasLOS && distanceToPlayer > attackDistance)
        {
            animator.SetInteger("AnimState", (int)CryptoState.WALK);
            IsAttacking = false;
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);

        }

        
    }

    /*private void OnDrawGizmos()
    {

        var offset = new Vector3(0.0f, 2.0f, 5.0f);
        var size = new Vector3(4.0f, 2.0f, 10.0f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;

        }
    }

    private void DoPunchDamage()
    {
        playerBehaviour.TakeDamage(20);
        StartCoroutine(kickBack());

    }

    private IEnumerator kickBack()
    {
        yield return new WaitForSeconds(1.0f);
        var direction = Vector3.Normalize(player.transform.position - transform.position);
        playerBehaviour.controller.SimpleMove(direction * kickForce);
        StopCoroutine(kickBack());
    }


}