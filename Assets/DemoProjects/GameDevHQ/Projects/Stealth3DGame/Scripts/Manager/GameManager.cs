using UnityEngine;
using UnityEngine.Playables;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts.Manager
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;
		public static GameManager Instance
		{ get { if (_instance == null) Debug.LogError("GameManager is null"); return _instance; } }

		public PlayableDirector introCutscene;
		public GameObject introCutsceneGO;
		public bool hasCard { get; set; }

		private void Awake() { _instance = this; }

		private void Start()
		{
			introCutsceneGO.SetActive(true);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				introCutscene.time = 60f;
			}
		}
	}
}
