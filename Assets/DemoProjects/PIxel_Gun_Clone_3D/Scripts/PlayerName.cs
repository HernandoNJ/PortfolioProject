using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class PlayerName : MonoBehaviour
	{
		public InputField nameInpF;
		public Button setNameBtn;

		public void OnNameInpFChanged(string name)
		{

		}

		public void OnClick_SetName()
		{
			PhotonNetwork.NickName = nameInpF.text;
		}
	}
}
