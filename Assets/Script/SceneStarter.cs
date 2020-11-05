using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStarter : MonoBehaviour
{
	public bool buttonPressed = false;

	private float speedCamera = 2f;

    public void playButtonPressed(){
    	buttonPressed = true;
    	gameObject.transform.GetChild(0).gameObject.SetActive(false);
    	GameObject Door = GameObject.Find("Door");
    	DoorControler doorControl = Door.GetComponent<DoorControler>();
    	doorControl.doorIsOpening = true;
    }

    public void Update(){
    	if (buttonPressed) Camera.main.transform.Translate(Vector3.forward*Time.deltaTime*speedCamera);

    	if (Camera.main.transform.position.z>28.7f){
    		SceneManager.LoadScene(/*SceneManager.GetActiveScene().buildIndex +*/ 1);
    	}
    }
}
