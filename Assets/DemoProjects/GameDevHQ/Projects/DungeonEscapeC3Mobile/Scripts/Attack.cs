using Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class Attack : MonoBehaviour
	{
		[SerializeField] private int damageAmount;

		[SerializeField] private bool attackEnabled;

		private void Start() => attackEnabled = true;

		private void OnTriggerEnter2D(Collider2D other)
		{
			var hit = other.GetComponent<IDamageable>();

			if (hit == null || attackEnabled == false)
				return;
			hit.Damage(damageAmount);
			attackEnabled = false;
			StartCoroutine(AttackResetRoutine());
		}

		private IEnumerator AttackResetRoutine()
		{
			yield return new WaitForSeconds(0.5f);
			attackEnabled = true;
		}
	}
}