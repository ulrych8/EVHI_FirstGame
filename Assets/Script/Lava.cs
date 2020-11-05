using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	public float lavaDamage = 101f;
    public void OnTriggerEnter(Collider other){
    	Rigidbody targetRigidBody = other.GetComponent<Rigidbody>();
    	TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>(); 
    	targetHealth.TakeDamage(lavaDamage);
    }
}
