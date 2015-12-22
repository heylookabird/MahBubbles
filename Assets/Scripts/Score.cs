using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public Text score;
	public Text timer;
	public BubbleFactory manager;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		score.text = "" + manager.GetScore ();
		timer.text = "" + manager.GetTime ();
	}
}
