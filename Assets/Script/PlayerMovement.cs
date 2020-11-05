using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private float movementForwardInputValue = 0f;
    private float movementRightInputValue = 0f;

    private float movementSpeed = 6f;
    private float turnSpeed = 6f;

    public Transform tankRotation;

    //private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        movementForwardInputValue = Input.GetAxis("Vertical");
        movementRightInputValue = Input.GetAxis("Horizontal");
    }


    void FixedUpdate()
    {
        if (movementForwardInputValue!=0f || movementRightInputValue!=0f){
            Move();            
        }
        //Turn();
    }

    private void Move(){
        //if movement in both direction
        if(movementForwardInputValue!=0 && movementRightInputValue!=0){
            movementForwardInputValue *= 0.7071f;
            movementRightInputValue *= 0.7071f;
        }

        Vector3 movementForward = transform.forward * movementForwardInputValue * movementSpeed * Time.deltaTime;
        Vector3 movementRight = transform.right * movementRightInputValue * movementSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movementForward + movementRight);

        float turn =  turnSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(/*rb.velocity.x*/movementRightInputValue*turn,0f,/*rb.velocity.z*/movementForwardInputValue*turn));
        //rb.MoveRotation(rb.rotation * rotation);
        //Debug.Log(" vector velocity x"+rb.velocity.x+", z = "+rb.velocity.z+" ... the magnitude is "+rb.velocity.magnitude);
        tankRotation.rotation = Quaternion.Lerp(tankRotation.rotation, rotation, turnSpeed*Time.deltaTime);
    }

    /*private void Turn(){
        float turn = turnSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f,turn,0f);
        rb.MoveRotation(rb.rotation * rotation);
    }*/

    /*private void OnEnable(){
        rb.IsKinematic = true;        
    }

    private void OnDisable(){
        rb.IsKinematic = false;        
    }*/

}
