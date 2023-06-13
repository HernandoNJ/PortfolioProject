using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
	[SerializeField] protected float minSpawnX;
	[SerializeField] protected float minSpawnY;
	[SerializeField] protected float maxSpawnX;
	[SerializeField] protected float maxSpawnY;
	[SerializeField] protected float randSpawnPosX;
	[SerializeField] protected float randSpawnPosY;
	[SerializeField] protected Vector3 spawnPos;
	[SerializeField] protected GameObject spawnGameobject;

	protected virtual void Init()
	{
		randSpawnPosX = Random.Range(minSpawnX, maxSpawnX);
		randSpawnPosY = Random.Range(minSpawnY, maxSpawnY);
		spawnPos = new Vector3(randSpawnPosX, randSpawnPosY, 0);
		Instantiate(spawnGameobject, spawnPos, Quaternion.identity);
	}
}
