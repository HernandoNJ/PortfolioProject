using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Zoo_App.Scripts
{
	[CreateAssetMenu(fileName = "New Card", menuName = "Card/ZooCard")]
	public class CardModel : ScriptableObject
	{
		public string title;
		public string description;
		public string exhibit;
		public Sprite animalSprite;
	}
}


