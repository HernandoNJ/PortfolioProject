using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Scripts.Player2
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
