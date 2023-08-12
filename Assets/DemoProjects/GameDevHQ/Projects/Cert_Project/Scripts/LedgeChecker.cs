using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts
{
	public class LedgeChecker : MonoBehaviour
	{
		[SerializeField] private Vector3 handsPos;
		[SerializeField] private Vector3 standupPos;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("LedgeGrabChecker"))
			{
				var player = other.GetComponentInParent<Player>();

				if (player != null)
					player.GrabLedge(handsPos, this);
			}
		}

		public Vector3 GetStandPos()
		{
			return standupPos;
		}
	}
}
