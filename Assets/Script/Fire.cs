using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	public Rigidbody Bullet;
    public float BulletSpeed = 700f;
	public Transform FireTransform;
    public Transform BaseTankTransform;
    public float timeBetweenFire = 0.2F;
    public bool alreadyFired = false;

    void FixedUpdate()
    {
        if ( Input.GetButtonDown("Fire1") && !alreadyFired ){
            Quaternion adjustRotation = Quaternion.LookRotation(FireTransform.position-BaseTankTransform.position)*Quaternion.Euler(90,0,0);
        	Rigidbody bulletInstance = Instantiate( Bullet, FireTransform.position, adjustRotation) as Rigidbody;
        	bulletInstance.velocity = (BulletSpeed * Time.deltaTime) * FireTransform.forward;
            alreadyFired = true;
            Invoke("ResetAlreadyFired", timeBetweenFire);
        }
    }

    private void ResetAlreadyFired(){
        alreadyFired = false;
    }
}
