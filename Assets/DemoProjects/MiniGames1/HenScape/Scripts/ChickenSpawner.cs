using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
	public GameObject chicken;
	private List<GameObject> spawnPoints = new List<GameObject>();

	void Start()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("ChickenSpawner"))
		{ spawnPoints.Add(g); }

		InvokeRepeating("createChicken", 3, 1);
	}

	private void createChicken()
	{
		int rand = Random.Range(0, spawnPoints.Count);

		if (spawnPoints.Count > 0)
		{
			Instantiate(chicken, spawnPoints[rand].transform.position, Quaternion.identity);
			spawnPoints.RemoveAt(rand);
		}
	}

}
