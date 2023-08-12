using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Scripts.Player2
{
	public class Shoot : MonoBehaviour
	{
		[SerializeField] private Camera mainCam;
		[SerializeField] private GameObject bloodEffect;

		private void Start()
		{
			mainCam = Camera.main;
			Cursor.lockState = CursorLockMode.Confined;
		}

		private void Update()
		{
			Fire();
		}

		private void Fire()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("mouse button down");
				var camCenter = new Vector3(0.5f, 0.5f, 0);
				var ray = mainCam.ViewportPointToRay(camCenter);
				RaycastHit hitInfo;

				if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 6))
				{
					Debug.Log("physics raycast");
					var otherCollider = hitInfo.collider;
					var otherHealth = otherCollider.GetComponent<Health>();

					if (otherHealth != null)
					{
						otherHealth.Damage(20);
						Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

					}
				}
			}
		}
	}
}
