using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
	public float MaxDamage = 100f;
	public float MaxLifeTime = 4f;
	public float ExplosionRadius = 5f;
	public float ExplosionForce = 1000f;

	//private float FiringBullet;
	public LayerMask TankMask;
	public LayerMask PlayerMask;
	public ParticleSystem ExplosionParticle;


	//public GameObject Bullet;
	
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, MaxLifeTime);
    }

    //private void OnCollisionEnter(Collision CollisionInfo){
    private void OnTriggerEnter(Collider other){
    	Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, TankMask | PlayerMask);
    	for (int i =0; i<colliders.Length; i++){
    		//Debug.Log("Rigidbody Found !");
    		Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();
    		if (!targetRigidBody) continue;
    		targetRigidBody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
    		//Debug.Log("Explosion occured....");
    		TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>(); 

    		if (!targetHealth) continue;
    		float damage = CalculateDamage(targetRigidBody.position);

    		targetHealth.TakeDamage(damage);
    	}

    	ExplosionParticle.transform.parent = null;

    	ExplosionParticle.Play();
    	Destroy(ExplosionParticle.gameObject, ExplosionParticle.main.duration);
    	Destroy(gameObject);
    }

    private float CalculateDamage( Vector3 targetPosition){
    	Vector3 explosionToTarget = targetPosition - transform.position;
    	float explosionDistance = explosionToTarget.magnitude;
    	float relativeDistance = ( ExplosionRadius - explosionDistance) / ExplosionRadius;
    	float damage = relativeDistance * MaxDamage;
    	damage = Mathf.Max(0f, damage);
    	return damage;
    }

}
