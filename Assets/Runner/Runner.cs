using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	
	public float acceleration;

	private bool touchingPlatform;
	
	public Vector3 boostVelocity, jumpVelocity;
	
	public float gameOverY;
	
	private Vector3 startPosition;
	
	public Vector3 healthPosition;
	
	private static int boosts;
	
	public static int health;
	
	public AudioClip gameOverSound;
	
	public static float targetTime;

	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			//activates jump
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			//activates boost
			else if(boosts > 0){
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
				
			}
		}
		
		//starts timer
		if(transform.localPosition.y < gameOverY){
			targetTime -= Time.deltaTime;
		}
		//sets timer at 2 seconds
		else{
			targetTime = 2.0f;
		}
		
		if(targetTime > 0) {
			//activates health
			if(Input.GetButtonDown("Health") && touchingPlatform == false){							
				if(health > 0) {
					transform.localPosition = PlatformManager.healthRespawn + (1f * Vector3.up) + (2f * Vector3.right);
					transform.rotation = Quaternion.identity;
					health -= 1;
					GUIManager.SetHealth(health);
				}
			}
		}
		//triggers the game over event
		else if(targetTime <= 0){
				GameEventManager.TriggerGameOver();
			}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);
		
		//triggers the game over event when the player falls off a platform
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
 	}
	
	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
	
	private void GameStart () {
		boosts = 0;
		health = 0;
		GUIManager.SetBoosts(boosts);
		GUIManager.SetHealth(health);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
		transform.rotation = Quaternion.identity;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		audio.PlayOneShot(gameOverSound);
	}
	
	//adds a boost and displays it in the GUI
	public static void AddBoost () {
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
	
	//adds a life and displays it in the GUI
	public static void AddHealth () {
		health += 1;
		GUIManager.SetHealth(health);
	}
}
