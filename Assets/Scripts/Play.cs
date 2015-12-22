using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play : MonoBehaviour {
	public Text[] menuGUI;
	public Text[] gameGUI;
	// Use this for initialization
	void Start () {
		SetMenuState ();
	}

	
	public void SetMenuState(){
		gameObject.SetActive (true);
		for (int i = 0; i < menuGUI.Length; i++) {
			menuGUI[i].gameObject.SetActive(true);
		}
		for (int i = 0; i < gameGUI.Length; i++) {
			gameGUI[i].gameObject.SetActive(false);
		}
	}

	void OnMouseDown(){
		gameObject.SetActive (false);
		StartGame ();
	}



	void StartGame(){
		for (int i = 0; i < menuGUI.Length; i++) {
			menuGUI[i].gameObject.SetActive(false);
		}
		for (int i = 0; i < gameGUI.Length; i++) {
			gameGUI[i].gameObject.SetActive(true);
		}
		BubbleFactory.GetInstance().StartGame ();
	}
}
