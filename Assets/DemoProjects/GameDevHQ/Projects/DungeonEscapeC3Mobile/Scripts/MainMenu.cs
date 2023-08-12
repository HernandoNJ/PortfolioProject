using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class MainMenu : MonoBehaviour
	{
		public void StartButton()
		{
			SceneManager.LoadScene(1);
		}

		public void QuitButton()
		{
			Debug.Log("Quitting app");
			Application.Quit();
		}
	}
}