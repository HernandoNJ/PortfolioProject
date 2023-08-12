using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class LookAtPlayer : MonoBehaviour
	{
		public Transform target;
		public Transform startPosition;

		private void Start()
		{
			transform.position = startPosition.position;
			transform.rotation = startPosition.rotation;
		}

		private void Update()
		{
			transform.LookAt(target);
		}
	}
}
