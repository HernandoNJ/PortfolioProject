using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts.Manager
{
	[RequireComponent(typeof(AudioManager))]
	public class AudioManager : MonoBehaviour
	{
		private static AudioManager _instance;
		public static AudioManager Instance
		{ get { if (_instance == null) Debug.Log("AudioManager is null"); return _instance; } }

		public AudioSource VoiceOverAudioSource;

		private void Awake()
		{
			_instance = this;
		}

		public void PlayVoiceOver(AudioClip clipArg)
		{
			VoiceOverAudioSource.clip = clipArg;
			VoiceOverAudioSource.Play();
		}
	}
}
