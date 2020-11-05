using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{
	public float hideTime = 15f;
	public float rotateSpeed = 0.5f;
    // Update is called once per frame
    void Update()
    {
        transform.parent.transform.Rotate(0,Time.deltaTime*rotateSpeed/2,Time.deltaTime*rotateSpeed);
    }

    private void OnTriggerEnter(Collider other){
    	if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
    		Rigidbody targetRigidBody = other.GetComponent<Rigidbody>();
	    	TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>(); 
	    	targetHealth.Heal();
	    	transform.GetComponent<Renderer>().enabled = false;
	    	transform.GetComponent<Collider>().enabled = false;


	    	Invoke("Respawn", hideTime);
	    }
    }

    private void Respawn(){
	    	transform.GetComponent<Renderer>().enabled = true;
	    	transform.GetComponent<Collider>().enabled = true;

    }
}
