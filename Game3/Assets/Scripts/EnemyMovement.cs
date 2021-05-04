using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange, attackRange, health;
    public bool playerInSightRange, playerInAttackRange;
    //public GameObject projectile;

    AudioSource audio;

    void Start ()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Awake ()
    {
        player = GameObject.Find ("Player").transform;
        agent = GetComponent<NavMeshAgent> ();
    }

   private void Update ()
   {
       playerInSightRange = Physics.CheckSphere (transform.position, sightRange, whatIsPlayer);
       playerInAttackRange = Physics.CheckSphere (transform.position, attackRange, whatIsPlayer);

       if (!playerInSightRange && !playerInAttackRange)
       {
           Patroling ();
       }

       if (playerInSightRange && !playerInAttackRange)
       {
           ChasePlayer ();
       }

       if (playerInAttackRange && playerInSightRange)
       {
           //AttackPlayer ();

           kill ();
       }

       if (playerInSightRange && !audio.isPlaying) 
            audio.Play ();

        if (!playerInSightRange) 
            audio.Stop ();
   }

   private void Patroling ()
   {
       if (!walkPointSet)
       {
           searchWalkPoint ();
       }

       if (walkPointSet)
       {
           agent.SetDestination (walkPoint);
       }

       Vector3 distanceToWalkPoint = transform.position - walkPoint;

       if (distanceToWalkPoint.magnitude < 1f)
       {
           walkPointSet = false;
       }
   }

   private void searchWalkPoint ()
   {
       float randZ = Random.Range (-walkPointRange, walkPointRange);
       float randX = Random.Range (-walkPointRange, walkPointRange);

       walkPoint = new Vector3 (transform.position.x + randX, transform.position.y, transform.position.z + randZ);

       if (Physics.Raycast (walkPoint, -transform.up, 2f, whatIsGround))
       {
           walkPointSet = true;
       }
   }

   private void ChasePlayer ()
   {
       agent.SetDestination (player.position);
   }

   private void AttackPlayer ()
   {
       agent.SetDestination (transform.position);
       transform.LookAt (player);

       //Rigidbody rb = Instantiate (projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
       //rb.AddForce (transform.forward * 32f, ForceMode.Impulse);

       Application.Quit ();
   }

    private void kill ()
    {
        mazeNo.mazeNumber = SceneManager.GetActiveScene().buildIndex;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(8);
    }
}
