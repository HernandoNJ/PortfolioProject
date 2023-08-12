using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.Service_App.Scripts
{
	public class LocationPanel : MonoBehaviour, IPanel
	{
		public RawImage mapImage;
		public InputField locationMapNotes;
		public Text caseNumber;

		public string mapsAPIKey, url, center;
		public float xCoord, yCoord;
		public int zoom, imgSize;

		private const string constMapsUrl = "https://maps.googleapis.com/maps/api/staticmap?";
		private const string constMapsAPIKey = "AIzaSyDL3BqCk1kDUeddbxvsxf7X8_b7qzo1QoM";

		private void OnEnable()
		{
			caseNumber.text = "CASE NUMBER: " + UIManager.Instance.newCaseObj.caseId;
		}

		private void Start()
		{
			// For testing --> center = "Berkley,CA";
			url = $"{url}center={center}&zoom={zoom}&size={imgSize}x{imgSize}&key={mapsAPIKey}";
			StartCoroutine(GetMapRoutine());
		}

		public void ProcessInfo()
		{
			if (string.IsNullOrEmpty(locationMapNotes.text) == false)
				UIManager.Instance.newCaseObj.locationNotes = locationMapNotes.text;
		}

		private IEnumerator GetMapRoutine()
		{
			using WWW map = new WWW(url);

			yield return map;
			if (map.error != null)
				Debug.LogError("map error: " + map.error);

			// TODO check if this map is assigned to caseToUpload 
			mapImage.texture = map.texture; // map shown, obtained from url  
		}
	}
}

// ************

// ----------------------------
// For Android build
// private void Start() => StartCoroutine(GetLocationRoutine());
//
// public void StartGettingMap() => StartCoroutine(GetMapRoutine());
//
// public void ProcessInfo() { }
//
// private IEnumerator GetLocationRoutine()
// {
//     if (Input.location.isEnabledByUser)
//     {
//         Input.location.Start(); 
//         var maxWait = 20;
//     
//         while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//         {
//             yield return new WaitForSeconds(1f);
//             maxWait--;
//         }
//     
//         if (maxWait < 1)
//         {
//             Debug.LogError("Timed out");
//             yield break;
//         }
//     
//         if (Input.location.status == LocationServiceStatus.Failed)
//         {
//             Debug.LogError("Get location failed");
//         }
//         else
//         {
//             xCoord = Input.location.lastData.latitude;
//             yCoord = Input.location.lastData.longitude;
//             center = $"{xCoord},{yCoord}";
//                 
//             url = $"{url}center={center}&zoom={zoom}&size={imgSize}x{imgSize}&key={mapsAPIKey}";
//                 
//             if(xCoord != 0) Debug.Log("xCoord:" + xCoord);
//             if(yCoord != 0) Debug.Log("yCoord:" + yCoord);
//             if(xCoord == 0 || yCoord == 0) Debug.LogError("error getting coords");
//
//             StartGettingMap();
//         }
//             
//         Input.location.Stop();
//     }
//     else Debug.LogError("Location services not enabled... dev message");
// }
//     
// private IEnumerator GetMapRoutine()
// {
//     using WWW map = new WWW(url);
//
//     yield return map;
//     if (map.error != null) Debug.LogError("map error: " + map.error);
//     mapImage.texture = map.texture;
// }

// ----------------------------------

//public string testUrl =
//         "https://maps.googleapis.com/maps/api/staticmap?center=Berkeley,CA&zoom=14&size=400x400&key=AIzaSyDL3BqCk1kDUeddbxvsxf7X8_b7qzo1QoM";

// public string testUrl2 =
//         "https://maps.googleapis.com/maps/api/staticmap?center=Miami,FL&zoom=14&size=400x400&key=AIzaSyDL3BqCk1kDUeddbxvsxf7X8_b7qzo1QoM";

// public string testCenter = "Sacramento,CA";
// public string testCenter2;

//public float eiffelX, eiffelY;

// private void Start()
// {

// For testing
// xCoord = 48.8584f;
// yCoord = 2.2945f;
// center = xCoord + "," + yCoord;
// url = $"{url}center={center}&zoom={zoom}&size={imgSize}x{imgSize}&key={mapsAPIKey}";
// StartCoroutine(GetMapRoutine());


//     url = constMapsUrl;
//     url = string.Format("{0}center={1},{2}&zoom={3}&size={4}x{5}&key={6}", url, xCoord, yCoord, zoom, imgSize,
//     url = $"{url}center={testCenter}&zoom={zoom}&size={imgSize}x{imgSize}&key={mapsAPIKey}";
//     var num1 = eiffelX.ToString();
//     var num2 = eiffelY.ToString();
//     testCenter2 = num1 + "," + num2;
//     url = $"{url}center={testCenter}&zoom={zoom}&size={imgSize}x{imgSize}&key={mapsAPIKey}";
//     Debug.Log(url);
//     Debug.Log(testUrl2);
//
//     eiffelX = 48.8584f;
//     eiffelY = 2.2945f;
//
// }

// *************
