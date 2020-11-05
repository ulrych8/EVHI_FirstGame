using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TankHealth : MonoBehaviour
{
	public float StartingTankHealth = 100f;
	public Slider Slider;
	public Image FillImage;
	public Color FullHealthColor = Color.green;
	public Color ZeroHealthColor = Color.red;
	//public GameObject ExplosionPrefab;
    //public DeathManager deathScript;
	//private ParticleSystem ExplosionParticles;
	private float CurrentHealth;
	private bool Dead;
	
	/*
    void Awake()
    {
    	ExplosionParticles = Instatiate(ExplosionPrefab).GetComponent<ParticleSystem>();

    	ExplosionParticles.gameObject.SetActive(false);
    }
	*/

    private void OnEnable(){
    	CurrentHealth = StartingTankHealth;
    	Dead = false;

    	SetHealthUI();
    }

    public void TakeDamage(float amount){
    	CurrentHealth -= amount;
    	SetHealthUI();

    	if (CurrentHealth<=0f && !Dead){
    		OnDeath();
    	}

    }

    public void Heal(){
        CurrentHealth = StartingTankHealth;
        SetHealthUI();
    }

    private void SetHealthUI(){
    	Slider.value = CurrentHealth;
    	FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth/StartingTankHealth);
    }

    private void OnDeath(){
    	Dead = true;
    	//ExplosionParticles.transform.position = transform.position;
    	//ExplosionParticles.gameObject.SetActive(true);

    	//ExplosionParticles.Play();
        gameObject.GetComponent<DeathManager>().GameOver();
        gameObject.GetComponent<PlayerMovement>().enabled = false;

    	//gameObject.SetActive(false);
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
