using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	public AudioClip health;

	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
	
	//spawns the power ups
	public void SpawnIfAvailable(Vector3 position){
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}
	
	//disables the object when the game is over
	private void GameOver () {
		gameObject.SetActive(false);
	}
	
	//gives the player a life when power up is hit
	void OnTriggerEnter () {
		Runner.AddHealth();
		gameObject.SetActive(false);
		AudioSource.PlayClipAtPoint(health, new Vector3(5, 1, 2));
	}
}
