using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Third_Person_Shooter.Scripts
{
	public class SmoothDamp : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private float speed = 10;

		private void LateUpdate()
		{
			var targetPosition = target.transform.position;
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
			transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles);
		}
	}
}
