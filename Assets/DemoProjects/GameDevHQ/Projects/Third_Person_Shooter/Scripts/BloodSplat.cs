using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Third_Person_Shooter.Scripts
{
	public class BloodSplat : MonoBehaviour
	{
		private void Start()
		{
			Destroy(gameObject, 1.5f);
		}
	}
}