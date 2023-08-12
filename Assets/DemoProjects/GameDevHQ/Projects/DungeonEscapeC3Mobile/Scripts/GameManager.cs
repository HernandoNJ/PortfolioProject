using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;
		public static GameManager Instance
		{
			get
			{
				if (instance == null)
					Debug.LogError("Game Manager is null");
				return instance;
			}
		}

		public DungeonPlayer.Player Player { get; private set; }
		public bool HasKeyToCastle { get; set; }

		private void Awake()
		{
			instance = this;
			Player = GameObject.FindWithTag("Player").GetComponent<DungeonPlayer.Player>();
		}
	}
}