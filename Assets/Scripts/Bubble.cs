using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {
	public float xVel = 0;
	public float xAcc = .005f;
	public float maxXVel = .1f;

	bool increasing = true;

	int currInterval;
	public int redInterval, yellowInterval, greenInterval;
	int randFrame;
	int randFrameMin = 50;
	int randFrameMax = 100;
	Sprite red, yellow, green;
	//keeping stateTime int because it can be used to just count frames
	int stateTime;
	COLOR color;

	SpriteRenderer image;

	enum COLOR{
		YELLOW,
		RED,
		GREEN
	}
	// Use this for initialization
	void Start () {
		stateTime = 0;
		currInterval = redInterval;
		color = COLOR.RED;
		image = gameObject.GetComponent<SpriteRenderer> ();
		//randomize start
		xAcc = Random.Range (-xAcc, xAcc);

		Sprite[] all = Resources.LoadAll<Sprite> ("Button");
		red = all[8];
		green = all[9];
		yellow = all[10];

	}
	
	void FixedUpdate(){
		stateTime += 1;
		Sway ();
		UpdateColor ();
		CheckTopBound ();
	}

	void CheckTopBound(){
		if (transform.position.y > 6) {
			BubbleFactory.GetInstance().ExpireBubble(this);
		}
	}

	public void UpdateColor(){
		if (stateTime == currInterval) {
			//changed it to be more gradual/forgiving
			if (color == COLOR.RED || color == COLOR.GREEN) {
				color = COLOR.YELLOW;
				image.sprite = yellow;
				currInterval = yellowInterval;
			} else if (color == COLOR.YELLOW) {
				if(increasing){
					color = COLOR.GREEN;
					image.sprite = green;
					currInterval = greenInterval;
					increasing = false;
				}else{
					color = COLOR.RED;
					image.sprite = red;
					currInterval = redInterval;
					increasing = true;
				}
			}
			/*else if (color == COLOR.GREEN){
				color = COLOR.;
				image.sprite = red;
				currInterval = redInterval;
			}*/

			stateTime = 0;
		}

	}

	void Sway(){
		//if it hits max velocity
		if (Mathf.Abs(xVel + xAcc) > maxXVel){
			//turn the motherfucker around
			xAcc = -xAcc;
		}

		if (Mathf.Abs(transform.position.x + xVel) > 13) {
			xVel = 0;
			xAcc = -xAcc;
		}

		xVel += xAcc;
		transform.position = new Vector3 (transform.position.x + xVel, transform.position.y + .01f, transform.position.z);
	}

	public string GetColor(){
		return color.ToString().ToLower();
	}

	void OnMouseDown(){
		int reward = BubbleFactory.GetInstance ().PopBubble (this);

		Debug.Log ("Rewarded: " + reward);
	}

}
