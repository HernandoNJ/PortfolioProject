using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class LaunchManager : MonoBehaviourPunCallbacks
	{
		public GameObject EnterGamePanel;
		public GameObject ConnectionStatusPanel;
		public GameObject LobbyPanel;
		public Text text1;


		#region Unity Methods
		private void Awake()
		{
			Debug.Log("~~~ awake called... auto sync scene= true ");

			PhotonNetwork.AutomaticallySyncScene = true;

			Debug.Log("~~~ exiting awake");
		}


		void Start()
		{
			Debug.Log("*** Initializing start...");

			EnterGamePanel.SetActive(true);
			ConnectionStatusPanel.SetActive(false);
			LobbyPanel.SetActive(false);

			Debug.Log("Exiting start...");
		}

		void Update()
		{
		}

		#endregion


		#region Public Methods
		public void ConnectToPhotonServer()
		{
			if (!PhotonNetwork.IsConnected)
			{
				Debug.Log("*** PhotonNetwork.ConnectUsingSettings(); ***");
				PhotonNetwork.ConnectUsingSettings();
				ConnectionStatusPanel.SetActive(true);
				EnterGamePanel.SetActive(false);
			}
		}

		// Find a random room to join. if there is not available room, a callback method - OnJoinRandomFailed - will be called
		public void JoinRandomRoom()
		{
			Debug.Log("*** JoinRandomRoom  ----");
			Debug.Log("***+++ PhotonNetwork.JoinRandomRoom();");

			PhotonNetwork.JoinRandomRoom();

			//Debug.Log("***= PhotonNetwork.JoinRandomRoom(); " + PhotonNetwork.JoinRandomRoom());
			//Debug.Log("-- Exiting JoinRandomRoom");
		}

		#endregion


		#region Photon Callbacks
		public override void OnConnected()
		{
			Debug.Log("--- OnConnected() --- Connected to Internet --- (low level connection)");
		}

		public override void OnConnectedToMaster()
		{
			Debug.Log("*** Entering OnConnectedToMaster ---");

			Debug.Log(PhotonNetwork.NickName +
				"' --> *** Connected to Photon server (OnConnectedToMaster()) ***");

			LobbyPanel.SetActive(true);
			ConnectionStatusPanel.SetActive(false);

			Debug.Log("--- Exiting OnConnectedToMaster ---");
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("--- ENTERING LM OnJoinedRoom ---");
			Debug.Log(PhotonNetwork.NickName + " ... successfully entered in room " + PhotonNetwork.CurrentRoom.Name);
			Debug.Log("*** LM OnJoinedRoom PlayerList.Length (number of players): " + PhotonNetwork.PlayerList.Length);

			PhotonNetwork.LoadLevel("GameScene");

			Debug.Log("***** Sc GameScene Loaded ");
			Debug.Log("--- EXITING LM OnJoinedRoom called ---");

		}

		//Called when a remote player enters the room we are in. Player newPlayer is an automatic parameter
		public override void OnPlayerEnteredRoom(Player newPlayer)
		{
			Debug.Log("*** LM OnPlayerEnteredRoom");
			Debug.Log(newPlayer.NickName + " has ENTERED to " +
				PhotonNetwork.CurrentRoom.Name + ". Num of players in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);
			Debug.Log("*** LM OnPlayerEnteredRoom Players list(count): " + PhotonNetwork.PlayerList.Length);
			Debug.Log("... Exiting LM OnPlayerEnteredRoom ");
		}

		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log("********************* hhhhhhhhhhhhh");
			Debug.Log("--- Entering OnJoinRandomFailed ---");
			base.OnJoinRandomFailed(returnCode, message);
			Debug.Log(message);
			Debug.Log("--- next step: call CreateAndJoinRoom(); --- ");

			CreateAndJoinRoom();

			Debug.Log("--- Exiting OnJoinRandomFailed ---");
		}

		#endregion


		#region Private methods
		void CreateAndJoinRoom()
		{
			Debug.Log("+++ Entering CreateAndJoinRoom() +++");
			Debug.Log("--- Creating parameters for new room ---");
			Debug.Log("--- Creating randomRoomName ---");

			string randomRoomName = "Room: " + Random.Range(0, 1000);
			Debug.Log("Random room name: " + randomRoomName);

			Debug.Log("--- Creating room options ---");
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.IsOpen = true;
			roomOptions.IsVisible = true;
			roomOptions.MaxPlayers = 20;

			Debug.Log("/// roomOptions created ///");

			Debug.Log("--- next step is Create a Room \n PhotonNetwork.CreateRoom(randomRoomName,roomOptions) ----");
			PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

			Debug.Log("*** Room created... ****");

			Debug.Log("--- parameters: RoomOptions randon name: " + randomRoomName + "room options.IsOpen: " + roomOptions.IsOpen + "roomOptions.IsVisible: " + roomOptions.IsVisible + "Max players: " + roomOptions.MaxPlayers);

			Debug.Log("+++ Exiting CreateAndJoinRoom() +++");
		}

		#endregion
	}
}