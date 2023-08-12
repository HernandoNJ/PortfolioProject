using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class OverviewPanel : MonoBehaviour, IPanel
	{
		public Case caseData;
		public Text caseNumber;
		public Text clientName;
		public Text dateInfo;
		public Text locationNotes;
		public Text photoNotes;
		public RawImage photoTaken;

		private void OnEnable()
		{
			caseData = UIManager.Instance.newCaseObj;

			caseNumber.text = "CASE NUMBER: " + caseData.caseId;
			clientName.text = caseData.clientName;
			dateInfo.text = DateTime.Now.ToString(); // show actual date
			caseData.dateInfo = dateInfo.text; // assign date to caseData
			locationNotes.text = "LOCATION NOTES: \n" + caseData.locationNotes;

			// 1,1 just for reference, they will be replaced later
			var reconstructedImage = new Texture2D(1, 1);
			reconstructedImage.LoadImage(caseData.photoTakenBytes); // get pic info from caseData

			photoTaken.texture = reconstructedImage; // show pic from reconstructedImage 
			photoNotes.text = "PHOTO NOTES: \n" + caseData.photoNotes;
		}

		public void ProcessInfo() => UIManager.Instance.OnSubmitButtonDown();
	}
}
