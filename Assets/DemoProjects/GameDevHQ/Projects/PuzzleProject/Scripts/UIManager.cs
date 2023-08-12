using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.PuzzleProject.Scripts
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Text _coinText, _livesText;

		public void UpdateCoinDisplay(int coins)
		{
			_coinText.text = "Coins: " + coins.ToString();
		}

		public void UpdateLivesDisplay(int lives)
		{
			_livesText.text = "Lives: " + lives.ToString();
		}

	}
}
