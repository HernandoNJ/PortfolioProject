using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.PuzzleProject.Scripts
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

		public Player Player { get; private set; }
		public bool HasKeyToCastle { get; set; }

		private void Awake()
		{
			instance = this;
			Player = GameObject.FindWithTag("Player").GetComponent<Player>();
		}
	}
}