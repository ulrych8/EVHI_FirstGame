using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

	public NavMeshAgent Agent;

	public GameObject Player;
	public LayerMask WhatIsGround, WhatIsPlayer;

	private Rigidbody Rb;

	//Patroling
	public Vector3 walkPoint;
	public bool walkPointSet ;
	private List<Transform> patrolPoints = new List<Transform>();	//insert list of patrol points in inspector

	//Attacking
	public float timeBetweenAttacks;
	public bool alreadyAttacked;

	//States
	public float sightRange, AttackRange;
	private bool playerInSigtRange, playerInAttackRange, playerShootable;
    private float turnSpeed = 6f;

    //Firing Useful
    public Rigidbody Bullet;
    public float BulletSpeed = 700f;
	public Transform FireTransform;
	private Transform AimingDevice;

	void Awake(){
		Player = GameObject.Find("PlayerTank");
		AimingDevice = transform.Find("TankObject/Sphere");
		Agent = GetComponent<NavMeshAgent>();
		Rb = GetComponent<Rigidbody>();
		Transform ParentPatrol = GameObject.Find("PatrolPoints").transform;
		foreach (Transform child in ParentPatrol){
			patrolPoints.Add(child);
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
    	Vector3 adjustPosition = transform.position - transform.forward;

        playerInSigtRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
        playerShootable = !Physics.Linecast(adjustPosition,Player.transform.position + transform.up,WhatIsGround);

        if (!playerInSigtRange && !playerInAttackRange)	Patroling();
        if ( playerInSigtRange && !playerInAttackRange){
			ChasePlayer();
			if (playerShootable && !alreadyAttacked) Fire();
		}
        if ( playerInSigtRange && playerInAttackRange){
        	if (playerShootable) AttackPlayer();
        	else ChasePlayer();
        }	

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) Agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude <1f) walkPointSet = false;

    }

    private void SearchWalkPoint(){
    	//Debug.Log("Look for random walk point");
    	int randomPoints = Random.Range(0, patrolPoints.Count);

    	walkPoint = patrolPoints[randomPoints].position;

    	walkPointSet = true;
    }

    private void ChasePlayer()
    {
    	LookPlayer();
        Agent.SetDestination(Player.transform.position);
    }

    private void AttackPlayer()
    {
    	LookPlayer();
     	//make sure enemy do not move
     	Agent.SetDestination(transform.position) ;

     	//transform.LookAt(Player.transform.position);
     	//--------------------------------------

		float turn =  turnSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed*Time.deltaTime);

     	//--------------------------------------
     	if (!alreadyAttacked) Fire();

    }

    private void ResetAlreadyAttacked(){
    	alreadyAttacked = false;
    }


    private void Fire(){
    	Quaternion adjustRotation = Quaternion.LookRotation(Player.transform.position-FireTransform.position)*Quaternion.Euler(90,0,0);
    	Rigidbody bulletInstance = Instantiate( Bullet, FireTransform.position, adjustRotation) as Rigidbody;
        bulletInstance.velocity = (BulletSpeed * Time.deltaTime) * FireTransform.forward;

     	alreadyAttacked = true;
     	Invoke(nameof(ResetAlreadyAttacked), timeBetweenAttacks);
    }

    private void LookPlayer(){
    	float turn =  turnSpeed * Time.deltaTime;
    	//adjust aim to not shoot the ground
    	Vector3 aimPos = new Vector3(AimingDevice.position.x,Player.transform.position.y,AimingDevice.position.z);
        Quaternion rotation = Quaternion.LookRotation(Player.transform.position - aimPos);
        AimingDevice.rotation = Quaternion.Lerp(AimingDevice.rotation,rotation, turnSpeed*Time.deltaTime);
    }
}
