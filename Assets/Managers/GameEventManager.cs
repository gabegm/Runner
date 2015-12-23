using UnityEngine;
using System.Collections;

public static class GameEventManager {
	
	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver;
	
	//starts the game
	public static void TriggerGameStart(){
		if(GameStart != null){
			GameStart();
		}
	}

	//stops the game
	public static void TriggerGameOver(){
		if (Runner.health > 0 && Runner.targetTime <= 0){
		}
		else if(GameOver != null){
			GameOver();
		}
	}

	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
}
