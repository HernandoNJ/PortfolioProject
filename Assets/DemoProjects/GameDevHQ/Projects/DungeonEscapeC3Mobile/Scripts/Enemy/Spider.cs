using Unity.Mathematics;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.Enemy
{
	public class Spider : Enemy
	{
		protected override void SetGemsAmount() => gemsAmount = 2;

		[SerializeField] private GameObject acidPrefab;

		// avoid checking anim parameters
		protected override void Update() { }

		protected override void EnemyMove() { }

		public void SpiderAttack()
		{
			Instantiate(acidPrefab, transform.position, quaternion.identity);
		}
	}

}
