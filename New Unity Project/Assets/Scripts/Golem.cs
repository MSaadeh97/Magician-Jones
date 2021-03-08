using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem: MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public LayerMask GroundLayer, PlayerLayer, WalkAreaLayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public Transform up;

    public float attackDelay = 1f;
    bool attackCD = false;
    public float attackStunDuration = 1;

    bool stunned = false;
    public float stunDuration = 1.5f;

    public GameObject gravityMagic;

    public float sightRange, attackRange;
    public bool playerInSight, playerAttackable;

    public AudioSource golemAS, golemSlamAS, loseSound, barrelBreak;

    bool playingSlam = false;

    public TrapDoor trapDoor;

    private void Awake()
    {
        player = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GetComponentInChildren<Animator>().SetBool("isStunned", false);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);

    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerAttackable = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);

        if (!stunned)
        {
            if (Player.isInArea == false)
            {
                Patrol();
            }
            if (playerInSight && !playerAttackable && Player.isInArea == true)
            {
                ChasePlayer();
            }
            if (playerAttackable && playerInSight && Player.isInArea == true)
            {
                AttackPlayer();
            }
        }

        if (trapDoor == null)
        {
            trapDoor = FindObjectOfType<TrapDoor>();
        }
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            GetComponentInChildren<Animator>().SetBool("isStunned", false);
            GetComponentInChildren<Animator>().SetBool("isAttacking", false);
            agent.SetDestination(walkPoint);
        }

        Vector3 distToWalkPoint = transform.position - walkPoint;

        if (distToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float ranzomZ = Random.Range(-walkPointRange, walkPointRange);
        float ranzomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + ranzomX, transform.position.y, transform.position.z + ranzomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 6f, WalkAreaLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        GetComponentInChildren<Animator>().SetBool("isStunned", false);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);


        if(!attackCD)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        agent.SetDestination(transform.position);
        GetComponentInChildren<Animator>().SetBool("isStunned", false);
        GetComponentInChildren<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackDelay);

        if (playingSlam == false)
        {
            golemSlamAS.Play();
            playingSlam = true;
        }

        if (playerAttackable && !attackCD)
        {
            StopAS();
            trapDoor.StopDoor();
            loseSound.Play();
            KillPlayer();
        }
        attackCD = true;
        stunned = true;
        GetComponentInChildren<Animator>().SetBool("isStunned", true);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackStunDuration);
        playingSlam = false;
        attackCD = false;
        stunned = false;

        StopCoroutine("Attack");
    }

    IEnumerator Stunned()
    {
        agent.SetDestination(transform.position);
        GetComponentInChildren<Animator>().SetBool("isStunned", true);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
        barrelBreak.Stop();
        StopCoroutine("Stunned");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boulder") && gravityMagic.GetComponent<GravityMagic>().lastBoulder.active)
        {
            stunned = true;
            barrelBreak.Play();
            StartCoroutine("Stunned");
            Destroy(other.gameObject);
        }
    }

    void KillPlayer()
    {
        Player.isDead = true;
    }

    public void StartAS()
    {
        golemAS.Play();
    }

    public void StopAS()
    {
        golemAS.Stop();
    }
}
