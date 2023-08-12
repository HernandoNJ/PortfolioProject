using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Third_Person_Shooter.Scripts
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private int minHealth;
		[SerializeField] private int maxHealth;
		[SerializeField] private int currentHealth;

		private void Start()
		{
			currentHealth = maxHealth;
		}

		public void Damage(int damageAmount)
		{
			currentHealth -= damageAmount;
			if (currentHealth < minHealth)
				Destroy(gameObject);
		}

	}
}