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
    public ContentController contentController; 
	public Text FPS; 
	public string version; 


	float switchTime; 
	float timer; 


	// Use this for initialization
	void Start () {
		tE = new TextEditor ();
        Screen.SetResolution(1024, 768, true);
    }
	
	//
	void FixedUpdate () {//Called every 1ms 
		if (isGameOn) {


				if (Time.time>switchTime) {
					//timeToReactJustHappened = true; 
					switchTime = Mathf.Infinity;
					TurnGameTimeToReactOn();

				}
				
			}
       
		if (Input.GetKeyDown (KeyCode.A)) {
			reactionTimes.Add (0.1f+Random.Range(0,0.1f));
			contentController.UpdateScrollList(reactionTimes);
		}

		
	}
	void Update() {
		float fps = 1 / Time.deltaTime;
		FPS.text = "Version: "+version+ " FPS = " + fps.ToString ("0"); 

	}


    public void CopyTextToClipBoard()
    {
        string excelText = "";

        foreach (float f in reactionTimes){
            excelText = excelText+f.ToString("0.000") + "\n";
        }
        excelText = excelText.Replace(".", ",");//French computer
        Debug.Log(excelText);
        tE.text = excelText;
        tE.SelectAll (); 
		tE.Copy ();
        messageBox.text = "Les données furent copiées dans le presse papier avec succès.";
    }
    public void DeleteLastTry() {
        int size = reactionTimes.Count;
        Debug.Log(size);
        if (size>0){

            reactionTimes.RemoveAt(size-1);
            contentController.UpdateScrollList(reactionTimes);
        }
        messageBox.text = "Le dernier essai fut effacé.";
    }
    public void DeleteAllTry(){
        reactionTimes.Clear();
        contentController.UpdateScrollList(reactionTimes);
        messageBox.text = "Tous les essais furent effacés.";
    }


    public void QuitApplication(){
        Application.Quit();
    }


    public void GameButtonPressed() {
		if (isGameOn) { //GameIsOn 
			if (timeToReact) {
				float reactionTime = Time.time - timer;
				timer = 0; 
				messageBox.text = "Votre temps de réaction est de : " + reactionTime.ToString("0.000") + " s";
				reactionTimes.Add (reactionTime);
                contentController.UpdateScrollList(reactionTimes);

				TurnGameOff (); 
			} else {
				//Are you trying to cheat? 
				TurnGameOff();
                messageBox.text = "Essaies-tu de tricher? Recommence!.";
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
		 
		switchTime = Time.time + Random.Range (2, 6); 


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



