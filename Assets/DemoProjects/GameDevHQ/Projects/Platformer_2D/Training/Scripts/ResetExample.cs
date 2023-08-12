using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class ResetExample : MonoBehaviour
	{
		public GameObject target;

		private void Reset()
		{
			Debug.Log("Reset");
			if (!target)
				target = GameObject.FindWithTag("Player");

		}

		private void Update()
		{
			transform.LookAt(target.transform);
		}
	}
}
