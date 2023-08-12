using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class Shop : MonoBehaviour
	{
		[SerializeField] private GameObject shopPanel;
		[SerializeField] private GameObject playerSprite;
		[SerializeField] private GameObject swordArc;
		[SerializeField] private DungeonPlayer.Player player;
		[SerializeField] private AdsManager adsManager;

		public int currentItem;
		public int currentItemCost;

		private void Start()
		{
			shopPanel.SetActive(false);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player"))
				return;
			player = other.GetComponent<DungeonPlayer.Player>();

			if (player == null)
				return;
			UIManager.Instance.UpdateShopGemsCount(player.diamonds);
			ActivateShop(true);
			adsManager.LoadAd();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
				ActivateShop(false);
		}

		public void SelectedItem(int item)
		{
			// item 0:  - 1: Boots of flight 2 - Key to castle
			currentItem = item;
			var uiInst = UIManager.Instance;

			switch (item)
			{
				case 0: // Flame sword 
					uiInst.UpdateShopSelection(50);
					currentItemCost = 200;
					break;
				case 1: // Boots of flight 
					uiInst.UpdateShopSelection(-40);
					currentItemCost = 400;
					break;
				case 2: // Key to castle
					uiInst.UpdateShopSelection(-135);
					currentItemCost = 100;
					break;
			}
		}

		public void BuyItem()
		{
			if (player.diamonds >= currentItemCost)
			{
				// award item
				player.diamonds -= currentItemCost;
				Debug.Log("purchased item: " + currentItem);
				Debug.Log("remaining diamonds: " + player.diamonds);
				if (currentItem == 2)
					GameManager.Instance.HasKeyToCastle = true;
				ActivateShop(false);
			}
			else
			{
				Debug.Log("Diamonds not enough");
				ActivateShop(false);
			}
		}

		private void ActivateShop(bool isOpen)
		{
			if (isOpen)
			{
				shopPanel.SetActive(true);
				playerSprite.SetActive(false);
				swordArc.SetActive(false);
			}
			else
			{
				shopPanel.SetActive(false);
				playerSprite.SetActive(true);
				swordArc.SetActive(true);
			}
		}
	}
}