using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class ClientInfoPanel : MonoBehaviour, IPanel
	{
		public Text caseNumberText;
		public InputField firstName, lastName;
		private Case caseData;

		private void OnEnable()
		{
			caseData = UIManager.Instance.newCaseObj;
			caseNumberText.text = "CASE NUMBER: " + caseData.caseId;
		}

		public void ProcessInfo()
		{
			var firstNameEmpty = string.IsNullOrEmpty(firstName.text);
			var lastNameEmpty = string.IsNullOrEmpty(lastName.text);

			if (firstNameEmpty || lastNameEmpty)
				Debug.Log("First or last name empty");
			else
			{
				caseData.clientName = firstName.text + " " + lastName.text;
				Debug.Log("ClientInfoPanel/ProcessInfo - NextButton");
				Debug.Log("Client name: " + caseData.clientName);
			}
		}
	}
}
