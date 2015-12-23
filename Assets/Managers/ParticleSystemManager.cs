using UnityEngine;
using System.Collections;

public class ParticleSystemManager : MonoBehaviour {

	public ParticleSystem[] particleSystems;
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOver();
	}

	//starts particle systems
	private void GameStart () {
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].Clear();
			particleSystems[i].enableEmission = true;
		}
	}
	
	//stops particle systems
	private void GameOver () {
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].enableEmission = false;
		}
	}
}
