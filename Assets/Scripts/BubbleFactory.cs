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
	int currentBubbleIndex = 0;
	Rigidbody2D[] bubbles;
	bool active;
	static BubbleFactory instance;

	public MenuHandler menus;
	public int NORMAL_REWARD = 100;
	public int StartLevel;
	public Rigidbody2D bubble;
	// Use this for initialization
	void Start () {
		instance = this;
		bubbles = new Rigidbody2D[100];
		GenerateNewBubble ();
		active = false;
	}
	public static BubbleFactory GetInstance(){
		return instance;
	}

	void Update(){
		//PhoneTouch ();
	}	
	// Update is called once per frame
	void FixedUpdate () {

		if (active) {
			statetime++;
			if (statetime % 60 == 0) {
				timeLeft--;
			}

			if(timeLeft <= 0){
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
		level = StartLevel;
		score = 0;
		streak = 0;

		for (int i = 0; i < currentBubbleIndex; i++) {
			if(bubbles[i] != null){
			Destroy(bubbles[i].gameObject);
			bubbles[i] = null;
			}
		}

		currentBubbleIndex = 0;

	}

	public void StartGame(){
		Reset ();
		active = true;
		GenerateNewBubble ();
	}

	void GameOver(){
		active = false;
		menus.SetMenuState ();
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
		RemoveBubble (b);
		Destroy (b.gameObject);
	}

	public bool IsActive(){
		return active;
	}

	bool RemoveBubble(Bubble b){
		bool found = false;
		for(int i = 0; i < currentBubbleIndex; i++){
			if(b.GetComponent<Rigidbody2D>() == bubbles[i]){
				found = true;
				currentBubbleIndex--;
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

	public int PopBubble(Bubble b){
		int reward = 0;
		if (active) {
			if (b.GetColor () == "green") {
				reward = RewardPerfect (2f);
			} else if (b.GetColor () == "yellow") {
				reward = RewardNormal ();
			} else {
				reward = RewardNothing ();
			}
		}
		bubblesPoppedLevel++;
		bubblesPoppedTotal++;
		RemoveBubble (b);
		Destroy (b.gameObject);

		return reward;
	}

	int RewardPerfect(float mult){
		timeLeft += 1;
		int reward = (int)(NORMAL_REWARD * 1.5f);
		streak++;

		if (streak > 3) {
			reward += (int)(reward * (streak/5f));
			timeLeft += 2;
		}

		score += reward;

		return reward;
	}

	int RewardNormal(){
		int reward = NORMAL_REWARD;
		score += reward;
		streak = 0;

		return reward;
	}

	int RewardNothing(){
		int reward = NORMAL_REWARD / 2;
		streak = 0;
		score += reward;

		return reward;
	}
	void GenerateNewBubble(){
		Vector3 pos = new Vector3 (Random.Range (-8, 8), Random.Range(-6, -10), 0);
		Rigidbody2D b = (Rigidbody2D)Instantiate (bubble, pos, Quaternion.identity);
		b.gameObject.name = "Bubble";
		bubbles [currentBubbleIndex] = b;
		bubblesGeneratedLevel++;
		bubblesGeneratedTotal++;
		currentBubbleIndex++;
	}
	/* Touch input on Android seems to work using just MouseDown, keeping this here in case it doesn't work on iphone
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

	}*/
}
