using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	private int score;
	public Text scoreText;

	public int Score { get => score; set => score = value; }

	void Start()
	{
		scoreText.text = "Score: 0";
	}

	void Update()
	{

	}

	public void SetScore()
	{

	}
}
