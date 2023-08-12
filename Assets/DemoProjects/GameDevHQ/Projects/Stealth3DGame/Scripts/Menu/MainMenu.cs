using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts.Menu
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private string gameScene = "LoadingScreen";

		public void StartGame() => SceneManager.LoadScene(gameScene);

		public void QuitGame() => Application.Quit();
	}
}
