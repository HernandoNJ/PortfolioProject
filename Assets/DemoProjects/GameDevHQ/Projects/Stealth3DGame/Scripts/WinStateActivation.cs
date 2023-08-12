using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class WinStateActivation : MonoBehaviour
	{
		public GameObject winCutscene;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				return;
			if (Manager.GameManager.Instance.hasCard)
				winCutscene.SetActive(true);
			else
				Debug.Log("Grab the key");
		}
	}
}
