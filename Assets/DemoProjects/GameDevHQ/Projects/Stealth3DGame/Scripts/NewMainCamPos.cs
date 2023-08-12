using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class NewMainCamPos : MonoBehaviour
	{
		public Transform camTrigger;
		public GameObject virtualCameras;

		private void Start()
		{
			virtualCameras.SetActive(false);
			Camera.main.transform.position = camTrigger.position;
		}
	}
}
