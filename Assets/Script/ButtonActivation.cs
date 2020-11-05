using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
	public bool buttonActivated = false;
	private Renderer buttonRenderer;

    // Start is called before the first frame update
    void Start()
    {
    	buttonRenderer = gameObject.GetComponent<Renderer>();
    	buttonRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
    	if (buttonActivated) return;
        buttonRenderer.material.color = Color.green;
        buttonActivated=true;
    }
}
