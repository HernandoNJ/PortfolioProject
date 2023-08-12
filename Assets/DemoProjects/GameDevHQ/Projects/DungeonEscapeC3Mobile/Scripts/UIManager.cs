using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager instance;
		public static UIManager Instance
		{
			get
			{
				if (instance == null)
					Debug.LogError("UI Manager is null");
				return instance;
			}
		}

		public Text playerGemsCountText;
		public Text gemCountText;
		public Image selectionImage;
		public Image[] healthBar;

		private void Awake()
		{
			instance = this;
			if (instance == null)
				Debug.LogError("UI Manager is null");
		}

		public void UpdateShopGemsCount(int gemsCount)
		{
			playerGemsCountText.text = gemsCount + " G";
		}

		public void UpdateShopSelection(int yPos)
		{
			var xPos = selectionImage.rectTransform.anchoredPosition.x;
			selectionImage.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
		}

		public void UpdateUIGemsCount(int count)
		{
			gemCountText.text = "" + count;
		}

		public void UpdateLives(int livesRemaining)
		{
			for (int i = 0; i <= livesRemaining; i++)
			{
				if (i == livesRemaining)
					healthBar[i].enabled = false;

			}
		}

	}
}
