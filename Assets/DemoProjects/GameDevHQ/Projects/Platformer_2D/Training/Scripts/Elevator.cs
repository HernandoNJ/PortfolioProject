using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class Elevator : MonoBehaviour
	{
		[SerializeField] private Transform startPos, targetPos;
		[SerializeField] private bool isGoingDown;
		[SerializeField] private float speed;

		public void CallElevator()
		{
			isGoingDown = !isGoingDown;
		}

		private void FixedUpdate()
		{
			var step = speed * Time.deltaTime;
			if (isGoingDown)
				transform.position = Vector3.MoveTowards(transform.position, targetPos.position, step);
			else if (!isGoingDown)
				transform.position = Vector3.MoveTowards(transform.position, startPos.position, step);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				Debug.Log("trigger with player");
				other.transform.parent = transform;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				Debug.Log("Player exiting trigger");
				other.transform.parent = null;
			}
		}
	}
}
