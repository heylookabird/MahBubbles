using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

	public Text[] menuGUI;
	public Text[] gameGUI;
	public Text instruction;

	public Play playButton;
	public Instructions instructionButton;
	// Use this for initialization
	void Start () {
		SetMenuState ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMenuState(){
		//buttons displayed
		playButton.gameObject.SetActive (true);
		instructionButton.gameObject.SetActive (true);

		//menu text displayed
		for (int i = 0; i < menuGUI.Length; i++) {
			menuGUI[i].gameObject.SetActive(true);
		}
		//game gui hidden
		for (int i = 0; i < gameGUI.Length; i++) {
			gameGUI[i].gameObject.SetActive(false);
		}
		//instructions hidden
		instruction.gameObject.SetActive (false);
	}

	public void SetInstructionsState(){
		//play button hidden
		playButton.gameObject.SetActive (false);
		//menu text hidden
		for (int i = 0; i < menuGUI.Length; i++) {
			menuGUI[i].gameObject.SetActive(false);
		}
		//game gui hidden
		for (int i = 0; i < gameGUI.Length; i++) {
			gameGUI[i].gameObject.SetActive(false);
		}
		//instruction text displayed
		instruction.gameObject.SetActive (true);
	}

	public void StartGame(){
		//buttons hidden
		playButton.gameObject.SetActive (false);
		instructionButton.gameObject.SetActive (false);
		//menu text hidden
		for (int i = 0; i < menuGUI.Length; i++) {
			menuGUI[i].gameObject.SetActive(false);
		}
		//game gui displayed
		for (int i = 0; i < gameGUI.Length; i++) {
			gameGUI[i].gameObject.SetActive(true);
		}
		//game started
		BubbleFactory.GetInstance().StartGame ();
	}
	
}
