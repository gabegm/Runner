using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUISkin gameOverSkin, instructionsSkin, playButtonSkin, runnerSkin;
	public bool gameOverTextEnabled, instructionsTextEnabled, playButtonEnabled, runnerTextEnabled;
	public GUIStyle gameOverTextGuiStyle, instructionsTextGuiStyle, runnerTextGuiStyle;
	public GUIText healthText, boostsText, distanceText;
	
	private static GUIManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverTextEnabled = false;
		runnerTextEnabled = true;
		instructionsTextEnabled = true;
		playButtonEnabled = true;
	}
	
	void OnGUI () {	
		GUI.skin = gameOverSkin;
		if (gameOverTextEnabled) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), ("GAME OVER"), gameOverTextGuiStyle);
		}
		
		GUI.skin = instructionsSkin;
		if (instructionsTextEnabled) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2+5, 100, 50), ("Press the button to start"), instructionsTextGuiStyle);
		}
		
		GUI.skin = playButtonSkin;
		if (playButtonEnabled) {
			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/2+65, 100, 50), ("Play")))
			{
				GameEventManager.TriggerGameStart();
			}
		}
		
		GUI.skin = runnerSkin;
		if (runnerTextEnabled) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-25, 100, 50), ("RUNNER"), runnerTextGuiStyle);
		}
	}
	
	private void GameStart () {
		gameOverTextEnabled = false;
		instructionsTextEnabled = false;
		playButtonEnabled = false;
		runnerTextEnabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverTextEnabled = true;
		instructionsTextEnabled = true;
		playButtonEnabled = true;
		enabled = true;
	}
	
	//assigning value to string
	public static void SetBoosts(int boosts){
		instance.boostsText.text = boosts.ToString();
	}
	
	//assigning value to text
	public static void SetDistance(float distance){
		instance.distanceText.text = distance.ToString("f0");
	}
	
	//assigning value to text
	public static void SetHealth(int health){
		instance.healthText.text = health.ToString();
	}
}
