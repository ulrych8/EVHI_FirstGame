using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public float transitionTime = 1f;

    public Transform cubeToNextLevel;

    private CubeNextLevelScript cubeScript;

    void Start(){
    	cubeToNextLevel = GameObject.Find("Cube").transform;
    	//cubeToNextLevel = gameObject.transform.GetChild(2).gameObject.transform;
    	cubeScript = cubeToNextLevel.GetComponent<CubeNextLevelScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cubeScript.hasBeenTriggered){
        	LoadNextLevel();
        }
    }

    public void LoadNextLevel(){
    	int sceneIndex = SceneManager.GetActiveScene().buildIndex;
    	StartCoroutine(LoadLevel(sceneIndex+1));
    }

    IEnumerator LoadLevel( int levelIndex){
    	animator.SetTrigger("Start");

    	yield return new WaitForSeconds(transitionTime);

    	SceneManager.LoadScene(levelIndex);
    }
}
