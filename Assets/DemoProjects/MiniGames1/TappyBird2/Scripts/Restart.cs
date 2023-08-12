using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	public void RestartScene() => SceneManager.LoadScene("TappyBirdScene");
	public void ExitGame() => Application.Quit();

}
