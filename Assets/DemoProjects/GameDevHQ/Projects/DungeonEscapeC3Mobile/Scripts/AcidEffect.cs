using Interfaces;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class AcidEffect : MonoBehaviour
	{
		[SerializeField] private float speed = 3;
		[SerializeField] private int damageAmount;

		private void Start()
		{
			Destroy(gameObject, 5);
		}

		private void Update()
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				var hit = other.GetComponent<IDamageable>();
				hit?.Damage(damageAmount);
				Debug.Log("acid damaged to: " + other.name);
				Destroy(gameObject);
			}
		}
	}
}
