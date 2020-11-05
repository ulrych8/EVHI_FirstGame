using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNextLevelScript : MonoBehaviour
{
    public bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other){
    	hasBeenTriggered = true;
    }
}
