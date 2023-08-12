using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }
		public Case newCaseObj;
		public Case caseToUpload;

		private string filePath;

		private void Awake() => Instance = this;

		private void OnEnable()
		{
			// TODO Thomas: event created to trigger analytics
		}

		private void Start()
		{
			if (Instance == null)
				Debug.LogError("UIManager is null");
		}

		public void RestartApp()
		{
			try
			{ SceneManager.LoadScene(0); }
			catch (Exception e) { Console.WriteLine(e); }
		}

		public void OnCreateNewCaseButtonDown()
		{
			newCaseObj = new Case();
			newCaseObj.caseId = "" + Random.Range(0, 1000); // DONE assign newCaseObj.caseId
		}

		public void OnSubmitButtonDown()
		{
			// TODO ask Thomas how to create this event
			//if(AnalyticsEvent.CustomEvent() == AnalyticsResult.Ok)
			//    Debug.Log("Analytic result ok");

			CreateCase();
			SaveFile();
			UploadFile();
			RestartApp();
		}

		private void CreateCase()
		{
			// DONE ServiceAppCase created
			caseToUpload = new Case
			{
				clientName = newCaseObj.clientName,
				dateInfo = newCaseObj.dateInfo,
				locationNotes = newCaseObj.locationNotes,
				photoTakenBytes = newCaseObj.photoTakenBytes,
				photoNotes = newCaseObj.photoNotes
			};
		}

		private void SaveFile()
		{
			Debug.Log("Saving");

			// DONE Saving file
			// file is created, but needs to be filled. Requires the .dat extension
			// FIX this is the file name, how is it different from the uploaded name?
			// AWSManager.UploadToS3.request Key = "case #" + caseId,

			var fileName = "/ServiceAppCase#" + newCaseObj.caseId + ".dat";
			Debug.Log("var Filename: " + fileName);
			filePath = Application.persistentDataPath + fileName;
			Debug.Log("File path: " + filePath);

			FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);

			try
			{
				var bf = new BinaryFormatter(); // open a data stream

				// start serialize streaming: where we want to serialize and what
				bf.Serialize(file, caseToUpload);
			}
			catch (SerializationException e)
			{
				Debug.LogError("Error serializing data " + e.Message);
			}
			finally
			{
				file.Close();
			} // close file, end stream

			Debug.Log("App data path: " + filePath);
		}

		private void UploadFile()
		{
			try
			{ AWSManager.Instance.UploadToS3(filePath, newCaseObj.caseId); }
			catch (Exception e) { Console.WriteLine(e); }
		}
	}
}
