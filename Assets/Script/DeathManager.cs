using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{

	public GameObject deathPanel;
	private bool pauseGame = false;

    // Start is called before the first frame update
    void Start()
    {
        deathPanel.SetActive(false);
    }

    public void GameOver(){
    	deathPanel.SetActive(true);
    	ToggleTime();
    }

    private void ToggleTime(){
    	pauseGame = !pauseGame;

    	if (pauseGame){
    		Time.timeScale=0.1f;
    	}else{
    		Time.timeScale=1;
    	}
    }

    public void Retry(){
    	ToggleTime();
    	SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
