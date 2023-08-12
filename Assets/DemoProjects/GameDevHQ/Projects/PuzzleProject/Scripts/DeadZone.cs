using System.Collections;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.PuzzleProject.Scripts
{
	public class DeadZone : MonoBehaviour
	{
		[SerializeField]
		private GameObject _respawnPoint;

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				Player player = other.GetComponent<Player>();

				if (player != null)
				{
					player.Damage();
				}

				CharacterController cc = other.GetComponent<CharacterController>();

				if (cc != null)
				{
					cc.enabled = false;
				}

				other.transform.position = _respawnPoint.transform.position;

				StartCoroutine(CCEnableRoutine(cc));
			}
		}

		IEnumerator CCEnableRoutine(CharacterController controller)
		{
			yield return new WaitForSeconds(0.5f);
			controller.enabled = true;
		}

	}
}
