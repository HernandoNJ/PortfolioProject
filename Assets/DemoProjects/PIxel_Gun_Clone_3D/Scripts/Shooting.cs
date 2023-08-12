using UnityEngine;

namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class Shooting : MonoBehaviour
	{
		[SerializeField] Camera fpsCamera;
		float fireRateTimer;
		bool canShoot;
		public float fireRate = 0.1f; //How much delay for each fire

		void Start()
		{
			if (fpsCamera == null)
				Debug.Log("fpsCamera is not assigned in inspector");
		}

		void Update()
		{
			if (fireRateTimer < fireRate)
				fireRateTimer += Time.deltaTime;

			if (Input.GetButton("Fire1") && fireRateTimer > fireRate)
			{
				fireRateTimer = 0; // Reset fireRateTimer

				Vector3 vptoRay = new Vector3(0.5f, 0.5f);
				Ray ray = fpsCamera.ViewportPointToRay(vptoRay);
				RaycastHit _hit;

				if (Physics.Raycast(ray, out _hit, 100))
					Debug.Log(_hit.collider.gameObject.name);
			}
		}
	}
}

