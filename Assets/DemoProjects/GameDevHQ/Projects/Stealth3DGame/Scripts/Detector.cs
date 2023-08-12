using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class Detector : MonoBehaviour
	{
		public GameObject gameOverScene;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				return;
			gameOverScene.SetActive(true);
		}
	}
}
