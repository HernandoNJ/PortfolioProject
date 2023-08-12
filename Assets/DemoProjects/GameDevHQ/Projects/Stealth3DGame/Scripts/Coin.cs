using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class Coin : MonoBehaviour
	{
		public AudioClip coinSound;

		private void Start()
		{
			AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
		}
	}
}
