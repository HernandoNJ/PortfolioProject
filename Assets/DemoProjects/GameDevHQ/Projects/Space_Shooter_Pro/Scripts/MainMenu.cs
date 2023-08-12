using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DemoProject.GameDevHQ.Projects.Space_Shooter_Pro.Scripts
{
	public class MainMenu : MonoBehaviour
	{
		//load game scene
		public void LoadGameScene()
		{
			SceneManager.LoadScene(1);
		}
	}
}
