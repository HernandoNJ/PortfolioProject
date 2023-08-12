using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Training.Scripts
{
	public class DeadZone : MonoBehaviour
	{
		[SerializeField] private Transform respawnPoint;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				Player player = other.GetComponent<Player>();

				if (player != null)
				{
					player.PlayerDamage();
					other.gameObject.SetActive(false);
					other.transform.position = respawnPoint.position;
					other.gameObject.SetActive(true);
				}
			}
		}
	}
}




