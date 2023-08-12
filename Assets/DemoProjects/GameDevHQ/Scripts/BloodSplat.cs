using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Scripts
{
	public class BloodSplat : MonoBehaviour
	{
		private void Start()
		{
			Destroy(gameObject, 1.5f);
		}
	}
}
