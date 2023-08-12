using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//! Remember VERIFY NULL OBJECTS ALWAYS ... Put this color and Debug.LogError 
namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class PixelGunGameManager : MonoBehaviourPunCallbacks
	{
		[SerializeField] GameObject playerPrefab;
		void Start()
		{
			int randomPointX = Random.Range(-20, -40);
			int randomPointZ = Random.Range(50, 30);
			Vector3 randomPoint = new Vector3(randomPointX, 0, randomPointZ);

			if (playerPrefab == null)
				Debug.LogError("playerPrefab is null / PixelGunGameManager/ GameManagerGO");
			if (PhotonNetwork.IsConnected && playerPrefab != null)
				PhotonNetwork.Instantiate(playerPrefab.name, randomPoint, Quaternion.identity);


		}
		public override void OnJoinedRoom()
		{
			Debug.Log("----- GM OnJoinedRoom ... ");
			Debug.Log(PhotonNetwork.NickName + "entered the room " + PhotonNetwork.CurrentRoom.Name);
			Debug.Log("*** Exiting GM OnJoinedRoom");
		}

		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			Debug.Log("******** GM ONPLAYERENTEREDROOM");
			Debug.Log(newPlayer.NickName + "Joined to" + PhotonNetwork.CurrentRoom.Name);
			Debug.Log("player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
			Debug.Log("*** Exiting GM OnPlayerEnteredRoom");

		}

	}
}
