using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameM;
	public float gameSpeed;
	public bool inGame;
	public bool readyToStart;

	public GameObject playerPrefab;
	public Transform startPosition;

	public float loadingTime;
	public string startLoadingText;
	public string pressLoadingText;
	public Animator HUDAnimator;
	[SerializeField] private Text loadingText;

	[SerializeField] private Image avatar;
	[SerializeField] private Text scoreText;
	[SerializeField] private string distanceText;
	[SerializeField] private string scale;

	private int score;
	[SerializeField] private float timeToScore;
	[SerializeField] private Sprite[] spirits;

	void Awake(){
		gameM = this;
		Invoke("AddScore", timeToScore);
	}

	void Start(){

		SetLoadingText(startLoadingText);
		Invoke("FinishLoading", loadingTime);
	}

	void Update(){
		if (Input.GetKeyDown("space") && inGame == false && readyToStart){
			StartGame();
		}
	}

	void AddScore(){
		if (inGame){
			score++;
			ShowScore();
		}
		Invoke("AddScore", timeToScore);
	}

	public void ChangeSpiritAvatar(int index){

		avatar.sprite = spirits[index];
	}

	void ShowScore(){

		scoreText.text = distanceText + score.ToString() + scale;
	}

	void StartGame(){

		inGame = true;
		loadingText.gameObject.SetActive(false);
		HUDAnimator.SetTrigger("InGame");
		Instantiate(
			playerPrefab,
		   	startPosition.position, 
			Quaternion.Euler(Vector3.zero)
		);
	}

	void SetLoadingText(string text){

		loadingText.text = text;
	}

	void FinishLoading(){

		readyToStart = true;
		SetLoadingText(pressLoadingText);
	}
}