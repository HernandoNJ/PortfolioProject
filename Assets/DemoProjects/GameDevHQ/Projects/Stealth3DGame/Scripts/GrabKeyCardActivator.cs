using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class GrabKeyCardActivator : MonoBehaviour
	{
		public GameObject sleepingGuardCutscene;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				return;
			sleepingGuardCutscene.SetActive(true);
			Manager.GameManager.Instance.hasCard = true;
		}
	}
}
