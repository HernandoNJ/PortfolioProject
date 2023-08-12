using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class Coin : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				UIManager.Instance.UpdateCoinsDisplay();
				Destroy(gameObject);
			}
		}
	}
}
