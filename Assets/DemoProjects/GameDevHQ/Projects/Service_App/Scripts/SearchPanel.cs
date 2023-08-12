using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class SearchPanel : MonoBehaviour, IPanel
	{
		public InputField caseNumberInput;

		public void ProcessInfo()
		{
			// Download list of all objects from AWS S3
			AWSManager.Instance.GetS3List(caseNumberInput.text);


			// Compare list with caseNumberInput

			// If a match found, download the object
		}
	}
}
