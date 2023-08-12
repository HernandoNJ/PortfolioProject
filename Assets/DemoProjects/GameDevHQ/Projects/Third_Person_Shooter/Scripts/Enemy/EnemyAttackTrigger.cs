using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Third_Person_Shooter.Scripts.Enemy
{
	public class EnemyAttackTrigger : MonoBehaviour
	{
		[SerializeField] private EnemyAI enemyAI;

		private void Start()
		{
			enemyAI = GetComponentInParent<EnemyAI>();
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