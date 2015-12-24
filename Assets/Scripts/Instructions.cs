using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	bool instructionsDisplayed;
	public Sprite exit, display;
	public MenuHandler menus;
	SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		instructionsDisplayed = false;
		rend = GetComponent<SpriteRenderer> ();
	}

	void OnMouseDown(){
		//if instruction text is not displayed
		if (!instructionsDisplayed) {
			//change image to exit
			rend.sprite = exit;
			menus.SetInstructionsState();
			instructionsDisplayed = true;
		} else {
			//change image to go to instructions
			rend.sprite = display;
			menus.SetMenuState();
			instructionsDisplayed = false;
		}
	}


}
