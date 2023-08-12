using Photon.Pun;
using TMPro;
using UnityEngine;


//* This script will adjust player according to PhotonView component
namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class PlayerSetup : MonoBehaviourPunCallbacks
	{
		[SerializeField] GameObject FPSCamera; //! check if null
		[SerializeField] TextMeshProUGUI playerNameText; //! check if null

		void Start()
		{
			if (FPSCamera == null)
				Debug.LogError("FPSCamera is null");

			//If true, I am the Master. Only I can control this player (MovementController), and I will see only this player´s camera feed
			if (photonView.IsMine)
			{
				transform.GetComponent<MovementController>().enabled = true;
				FPSCamera.SetActive(true);
			}
			else
			{
				transform.GetComponent<MovementController>().enabled = false;
				FPSCamera.SetActive(false);
			}

			//it doesn´t depend on photonView.IsMine attribute
			SetPlayerUI();
		}

		void SetPlayerUI()
		{
			if (playerNameText == null)
				Debug.Log("playerNameText is null");
			else
				playerNameText.text = photonView.Owner.NickName; // Set player´s name
		}

	}
}
