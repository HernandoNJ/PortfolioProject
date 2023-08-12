using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TappyBirdGameManager : MonoBehaviour
{
	enum PageState { None, Start, GameOver, Countdown };

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStartedEvent;
	public static event GameDelegate OnGameoverConfirmedEvent;

	public static TappyBirdGameManager instance;

	public GameObject startPage;
	public GameObject gameoverPage;
	public GameObject countdownPage;
	public Text scoreText;
	public Text highScoreText;

	private int score = 0;
	private bool isGameover = true;
	private static int gameoverCount = 0;

	public int Score { get => score; }
	public int GameoverCount { get => gameoverCount; }
	public bool IsGameover { get => isGameover; }

	private void OnEnable()
	{
		CountdownText.OnCountdownFinishedEvent += OnCountdownFinishedMethod;
		TapController.OnPlayerScored += OnPlayerScoredMethod;
		TapController.OnPlayerDied += OnPlayerDiedMethod;
	}

	private void OnDisable()
	{
		CountdownText.OnCountdownFinishedEvent -= OnCountdownFinishedMethod;
		TapController.OnPlayerScored -= OnPlayerScoredMethod;
		TapController.OnPlayerDied -= OnPlayerDiedMethod;
	}

	void Awake() => instance = this; // TODO remove after fixing singleton bug
									 // FIXME: singleton
									 // if (Instance != null) Destroy(gameObject);
									 // else { Instance = this; DontDestroyOnLoad(gameObject); }

	private void Start() { SetPageState(PageState.Start); ShowHighScore(); }

	// activated when play button is pressed
	// show up countdown page and execute counter (OnEnable)
	public void StartGame() => SetPageState(PageState.Countdown);

	public void ShowHighScore() => highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");

	public void ResetHighScore() { PlayerPrefs.DeleteKey("HighScore"); ShowHighScore(); }

	private void OnCountdownFinishedMethod()
	{
		// what to do after countdown finishes?
		// deactivate pages
		// reset score
		// isGameOver to false => start the game (enable update code)
		SetPageState(PageState.None);
		OnGameStartedEvent();
		score = 0;
		isGameover = false;
	}

	private void OnPlayerScoredMethod() { score++; scoreText.text = score.ToString(); }

	private void OnPlayerDiedMethod()
	{
		isGameover = true;
		int savedScore = PlayerPrefs.GetInt("HighScore");
		if (score > savedScore)
			PlayerPrefs.SetInt("HighScore", score);
		ShowHighScore();
		SetPageState(PageState.GameOver);
	}

	// activated when replay button is pressed
	public void GameOverConfirmedMethod()
	{
		gameoverCount++;
		if (GameoverCount < 3)
		{
			OnGameoverConfirmedEvent();  // event sent to TapController
			scoreText.text = "0"; // reset score
			SetPageState(PageState.Start); // show up start page
		}
		else
		{ SceneManager.LoadScene("RestartScene"); gameoverCount = 0; } // needed for Lives script

	}

	private void SetPageState(PageState pg)
	{
		switch (pg)
		{
			case PageState.None:
				startPage.SetActive(false);
				gameoverPage.SetActive(false);
				countdownPage.SetActive(false);
				break;

			case PageState.Start:
				startPage.SetActive(true);
				gameoverPage.SetActive(false);
				countdownPage.SetActive(false);
				break;

			case PageState.GameOver:
				startPage.SetActive(false);
				gameoverPage.SetActive(true);
				countdownPage.SetActive(false);
				break;

			case PageState.Countdown:
				startPage.SetActive(false);
				gameoverPage.SetActive(false);
				countdownPage.SetActive(true);
				break;
		}
	}
}
