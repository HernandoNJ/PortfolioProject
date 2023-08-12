using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProject.GameDevHQ.Projects.Zoo_App.Scripts
{
	public class CardView : MonoBehaviour
	{
		public Text title;
		public Text description;
		public Text exhibit;
		public Image animalImage;

		public CardModel[] cards;

		public void DisplayCard(int cardIndex)
		{
			title.text = cards[cardIndex].title;
			description.text = cards[cardIndex].description;
			exhibit.text = cards[cardIndex].exhibit;
			animalImage.sprite = cards[cardIndex].animalSprite;
		}
	}
}




