using System;
using UnityEngine;

public class HenGameEventsSystem : MonoBehaviour
{
	public static HenGameEventsSystem instance;

	private void Awake() { instance = this; }

	public event Action onValidCheckpointTriggerEnter;

	// Check the action isn't null before invoking the event 
	public void ValidCheckpointTriggerEnter()
	{
		if (onValidCheckpointTriggerEnter != null)
			onValidCheckpointTriggerEnter();
	}

}
