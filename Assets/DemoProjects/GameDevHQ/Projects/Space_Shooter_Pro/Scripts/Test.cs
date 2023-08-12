using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Space_Shooter_Pro.Scripts
{
	public class Test : MonoBehaviour
	{
		[SerializeField] private int weaponId;

		void Start()
		{
		}

		void Update()
		{
			switch (weaponId)
			{
				case 1:
					Debug.Log("Gun");
					break;
				case 2:
					Debug.Log("Knife");
					break;
				case 3:
					Debug.Log("Machine Gun");
					break;
			}
		}
	}
}