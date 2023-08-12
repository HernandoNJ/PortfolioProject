using System.Collections;
using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class CameraConeTrigger : MonoBehaviour
	{
		public Animator animator;
		public GameObject gameOverCutscene;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				return;
			var render = GetComponent<Renderer>();
			var color = new Color(1f, 0f, 0f, 0.17f);
			render.material.SetColor("_TintColor", color);
			animator.enabled = false;
			StartCoroutine(AlertRoutine());
		}

		private IEnumerator AlertRoutine()
		{
			yield return new WaitForSeconds(0.5f);
			gameOverCutscene.SetActive(true);
		}
	}
}
