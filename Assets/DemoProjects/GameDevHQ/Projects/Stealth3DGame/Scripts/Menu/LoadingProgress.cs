using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts.Menu
{
	public class LoadingProgress : MonoBehaviour
	{
		// ref prog bar
		[SerializeField] private Image progressBar;
		[SerializeField] private string gameScene = "Main1";

		private void Start() => StartCoroutine(LoadProgressAsync());

		private IEnumerator LoadProgressAsync()
		{
			// create async operation: LoadSceneAsync();
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene);

			while (!asyncLoad.isDone) // while operation isn't finished
			{
				// progress bar fill amount = operation progress
				progressBar.fillAmount = asyncLoad.progress;

				Debug.Log("while loop");

				yield return new WaitForEndOfFrame();
			}

			// TODO: ask Thomas, why after finishing the while loop it exits the coroutine?
		}
	}
}
