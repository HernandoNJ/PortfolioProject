using UnityEngine;

public class BulletPoint : MonoBehaviour
{
	public GameObject bulletPrefab;
	void Start()
	{
		InvokeRepeating("InstantiateBulletPrefab", 0.2f, 5);
	}


	public void InstantiateBulletPrefab()
	{
		Instantiate(bulletPrefab, transform.position, transform.rotation);
	}
}
