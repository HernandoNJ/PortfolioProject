using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class TakePhotoPanel : MonoBehaviour, IPanel
	{
		public RawImage photoTaken;
		public InputField photoNotes;
		public Text caseNumber;
		private Case caseData;
		private string imagePath;

		private void OnEnable()
		{
			caseData = UIManager.Instance.newCaseObj;
			caseNumber.text = "CASE NUMBER: " + caseData.caseId;
		}

		public void TakeNewPicture() => TakePicture(512);

		public void ProcessInfo()
		{
			// TODO newImage is the photo taken with the camera
			var newImage = NativeCamera.LoadImageAtPath(imagePath, 512, false);
			var imageDataBytes = newImage.EncodeToPNG(); // photo as byte[]

			// TODO assigning photoTakenBytes and photo notes to caseData - UIManager.Instance.newCaseObj
			// TODO photoTakenBytes is a byte[] - check it out
			caseData.photoTakenBytes = imageDataBytes;
			caseData.photoNotes = photoNotes.text;
		}

		private void TakePicture(int maxSize)
		{
			NativeCamera.TakePicture(path =>
			{
			// Debug.Log("Image path1: " + path);

			if (path != null)
				{
				// Create a Texture2D from the captured image
				var newPhotoTexture = NativeCamera.LoadImageAtPath(path, maxSize, false);

					if (newPhotoTexture == null)
					{
						Debug.Log("Couldn't load texture from " + path);
						return;
					}

				// Show pic taken with the camera
				photoTaken.texture = newPhotoTexture;
					imagePath = path;
					Debug.Log("Image path 2: " + imagePath);
				}
			}, maxSize);
		}
	}
}
