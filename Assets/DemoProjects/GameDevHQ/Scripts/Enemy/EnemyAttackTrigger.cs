using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Scripts.Enemy
{
	public class EnemyAttackTrigger : MonoBehaviour
	{
		[SerializeField] private Enemy.EnemyAI enemyAI;

		private void Start()
		{
			enemyAI = GetComponentInParent<Enemy.EnemyAI>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && other != null)
				enemyAI.EnemyAttack();
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player") && other != null)
				enemyAI.EnemyChase();
		}
	}
}