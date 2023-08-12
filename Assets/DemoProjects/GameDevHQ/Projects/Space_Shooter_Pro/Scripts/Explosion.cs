using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Space_Shooter_Pro.Scripts
{
	public class Explosion : MonoBehaviour
	{
		void Start()
		{
			Destroy(gameObject, 3f);
		}
	}
}
