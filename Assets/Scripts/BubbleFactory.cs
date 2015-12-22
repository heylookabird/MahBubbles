using UnityEngine;
using System.Collections;

public class BubbleFactory : MonoBehaviour {
	public int gametime;
	int timeLeft, statetime = 0;
	int level = 1;
	int bubblesGeneratedLevel = 0;
	int bubblesPoppedLevel = 0;
	int bubblesGeneratedTotal = 0;
	int bubblesPoppedTotal = 0;
	int score = 0;
	int streak = 0;
	int currentBubble = 0;
	Bubble[] bubbles;
	bool active;
	static BubbleFactory instance;

	public int NORMAL_REWARD = 100;

	public Play playButton;

	public Rigidbody2D bubble;
	// Use this for initialization
	void Start () {
		instance = this;
		Bubble[] bubbles = new Bubble[100];
		GenerateNewBubble ();
		active = false;
	}
	public static BubbleFactory GetInstance(){
		return instance;
	}

	void Update(){
		PhoneTouch ();
	}	
	// Update is called once per frame
	void FixedUpdate () {

		if (active) {
			statetime++;
			if (statetime % 60 == 0) {
				timeLeft--;
			}

			if(timeLeft == 0){
				GameOver();
			}
		}

		if (bubblesGeneratedLevel < level) {
			float random = Random.Range (-1, 4);

			if (random < 0) {
				GenerateNewBubble ();
			}
		} else {
			CheckLevelClear();
		}
	}

	void Reset(){
		bubblesGeneratedLevel = 0;
		bubblesGeneratedTotal = 0;
		bubblesPoppedLevel = 0;
		bubblesPoppedTotal = 0;
		timeLeft = gametime;
		statetime = 0;
		level = 10;
		score = 0;
		streak = 0;

		for (int i = 0; i < currentBubble; i++) {
			Destroy(bubbles[i]);
			bubbles[i] = null;
		}

		currentBubble = 0;

	}

	public void StartGame(){
		Reset ();
		active = true;
		GenerateNewBubble ();
	}

	void GameOver(){
		active = false;
		playButton.SetMenuState ();
	}

	void CheckLevelClear(){
		if (bubblesPoppedLevel == bubblesGeneratedLevel) {
			level++;
			bubblesPoppedLevel = 0;
			bubblesGeneratedLevel = 0;
		}
	}

	public void ExpireBubble(Bubble b){
		bubblesPoppedLevel++;

		if (active) {
			if (b.transform.position.x > -10 || b.transform.position.x < 10) {
				timeLeft -= 2;
			}
		}

		RemoveBubble (b);
		Destroy (b.gameObject);
	}

	public bool IsActive(){
		return active;
	}

	bool RemoveBubble(Bubble b){
		bool found = false;
		for(int i = 0; i < currentBubble; i++){
			if(b == bubbles[i]){
				found = true;
				currentBubble--;
			}
			if(found){
				bubbles[i] = bubbles[i+1];
			}
		}

		return found;
	}

	public int GetScore(){
		return score;
	}

	public int GetTime(){
		return timeLeft;
	}

	public void PopBubble(Bubble b){
		if (active) {
			if (b.GetColor () == "green") {
				RewardPerfect (2f);
			} else if (b.GetColor () == "yellow") {
				RewardNormal ();
			} else {
				RewardNothing ();
			}
		}
		bubblesPoppedLevel++;
		bubblesPoppedTotal++;
		RemoveBubble (b);
		Destroy (b.gameObject);
	}

	void RewardPerfect(float mult){
		timeLeft += 5;
		int reward = (int)(NORMAL_REWARD * 1.5f);
		score += reward;

		if (streak > 3) {
			score += reward * (streak/10);
		}
	}

	void RewardNormal(){
		timeLeft += 2;
		score += NORMAL_REWARD;
		streak = 0;
	}

	void RewardNothing(){
		streak = 0;
		score += NORMAL_REWARD / 2;
	}
	void GenerateNewBubble(){
		Vector3 pos = new Vector3 (Random.Range (-8, 8), Random.Range(-6, -10), 0);
		Instantiate (bubble, pos, Quaternion.identity).name = "Bubble";
		bubblesGeneratedLevel++;
		bubblesGeneratedTotal++;
	}

	void PhoneTouch(){
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			
			if (touch.phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if(hit.collider.gameObject.name == "Bubble"){
						for(int i = 0; i < currentBubble; i++){
							if(hit.collider.gameObject == bubbles[i]){
								PopBubble(bubbles[i]);
							}
						}
					}else{
						HandleMenuOption(hit.collider.gameObject);
					}
				}
			}
		}
	}

	void HandleMenuOption(GameObject obj){

	}
}
