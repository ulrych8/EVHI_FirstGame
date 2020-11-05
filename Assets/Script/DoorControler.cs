using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
	public bool doorIsOpening = false;
	private float speedOpening = 2.5f;

	private GameObject Buttons ;
	private List<Transform> buttonList = new List<Transform>();

	void Start(){
		Buttons = GameObject.Find("Buttons");
		foreach (Transform child in Buttons.transform){
			buttonList.Add(child);
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doorIsOpening){
        	gameObject.transform.Translate(Vector3.forward*Time.deltaTime*speedOpening);
        }

        if (gameObject.transform.position.y < 0f){
        	doorIsOpening = false;
        }

        foreach (Transform button in buttonList){
        	ButtonActivation butt = button.GetComponent<ButtonActivation>();
        	doorIsOpening = true;
        	if (! butt.buttonActivated){
        		doorIsOpening = false;
        	}
        }
    }
}
