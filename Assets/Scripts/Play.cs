using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play : MonoBehaviour {
	public MenuHandler menus;
	void OnMouseDown(){
		StartGame ();
	}



	void StartGame(){
		menus.StartGame ();
	}
}
