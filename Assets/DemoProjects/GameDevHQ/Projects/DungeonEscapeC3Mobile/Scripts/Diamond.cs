using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class Diamond : MonoBehaviour
	{
		public int gems;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				var player = other.GetComponent<DungeonPlayer.Player>();
				if (player != null)
					player.AddGems(gems);
				Destroy(gameObject);
			}
		}
	}
}
