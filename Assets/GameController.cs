using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameController : MonoBehaviour {

	TextEditor tE;

	private bool isGameOn = false; 
	//private bool timeToReactJustHappened  = false; 
	private bool timeToReact = false; 

	public Button gameButton; 
	public Text gameButtonText; 
	public Text messageBox; 
	public List<float> reactionTimes; 


	float switchTime; 
	float timer; 


	// Use this for initialization
	void Start () {
		tE = new TextEditor (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameOn) {


				if (Time.time>switchTime) {
					//timeToReactJustHappened = true; 
					switchTime = Mathf.Infinity;
					TurnGameTimeToReactOn();

				}
				
			}


		
	}


	public void CopyTextToClipBoard() {
		tE.text = "TEST!\n TESTAGain";
		tE.SelectAll (); 
		tE.Copy (); 
	}

	public void GameButtonPressed() {
		if (isGameOn) { //GameIsOn 
			if (timeToReact) {
				float reactionTime = Time.time - timer;
				timer = 0; 
				messageBox.text = "Votre temps de réaction est de : " + reactionTime.ToString() + " s";
				reactionTimes.Add (reactionTime); 

				TurnGameOff (); 
			} else {
				//Are you trying to cheat? 
				//TurnGameOff(); 
			}
		} 
		else {// GameIsNotOn turn it on
			TurnGameStandbyOn(); 
		}
		
	}
	void TurnGameStandbyOn() {
		isGameOn = true;
		gameButtonText.text = "Le test est en cours. Cliquez seulement lorsque le bouton devient vert!"; 
		SetButtonColor(Color.red);
		 
		switchTime = Time.time + Random.Range (2, 5); 


	}
	void TurnGameTimeToReactOn() {
		timeToReact = true; 
		gameButtonText.text = "Reagissez!";
		SetButtonColor(Color.green);
		timer = Time.time; 
		//GameButtonPressed (); 

	}
	void TurnGameOff() {
		isGameOn = false; 
		timeToReact  = false; 
		gameButtonText.text = "Commencer l'expérience"; 
		SetButtonColor(Color.white);

	}

	void SetButtonColor(Color color) {
		ColorBlock cb = gameButton.colors; 
		cb.normalColor = color; 
		cb.highlightedColor = color; 
		gameButton.colors = cb; 
	}

}



