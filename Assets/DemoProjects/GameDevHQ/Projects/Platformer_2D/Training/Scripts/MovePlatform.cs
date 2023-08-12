using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class MovePlatform : MonoBehaviour
	{
		public Transform pointA;
		public Transform pointB;
		[SerializeField] private float speed;
		[SerializeField] private bool moveToRight;
		[SerializeField] private bool moveToLeft;

		private void Start()
		{
			transform.position = pointA.position;
			moveToRight = true;
		}

		private void FixedUpdate()
		{
			var step = speed * Time.deltaTime;

			if (moveToRight)
			{
				transform.position = Vector3.MoveTowards
								(transform.position, pointB.transform.position, step);

				if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
				{
					moveToRight = false;
					moveToLeft = true;
				}
			}

			if (moveToLeft)
			{
				transform.position = Vector3.MoveTowards
								(transform.position, pointA.transform.position, step);

				if (Vector3.Distance(transform.position, pointA.position) < 0.01f)
				{
					moveToRight = true;
					moveToLeft = false;
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				other.transform.parent = transform;
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
				other.transform.parent = null;
		}
	}
}
