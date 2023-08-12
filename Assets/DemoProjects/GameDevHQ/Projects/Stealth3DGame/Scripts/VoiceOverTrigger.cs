using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class VoiceOverTrigger : MonoBehaviour
	{
		public AudioClip voiceOverDialogue;
		public int counter;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player") || counter >= 1)
				return;
			counter++;
			Manager.AudioManager.Instance.PlayVoiceOver(voiceOverDialogue);
		}
	}
}
