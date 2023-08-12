using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	[RequireComponent(typeof(UIManager))]
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }

		[SerializeField] private Text coinsText, livesText;
		[SerializeField] private int coinsAmount;

		private void Awake()
		{
			//DontDestroyOnLoad(this);
			//if (Instance != null && Instance != this) Destroy(gameObject);
			Instance = this;
		}

		private void OnEnable()
		{
			Player.OnPlayerDied += UpdateLivesDisplay;
		}

		private void OnDisable()
		{
			Player.OnPlayerDied -= UpdateLivesDisplay;
		}

		private void Start()
		{
			coinsText.text = "Coins: " + coinsAmount;
			UpdateLivesDisplay(3);
		}

		public void UpdateCoinsDisplay()
		{
			coinsAmount++;
			coinsText.text = "Coins: " + coinsAmount;
		}

		private void UpdateLivesDisplay(int playerLives)
		{
			livesText.text = "Lives: " + playerLives;
		}
	}
}
