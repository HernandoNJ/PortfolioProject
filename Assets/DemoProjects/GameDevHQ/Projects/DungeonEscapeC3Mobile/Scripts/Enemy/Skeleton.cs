namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.Enemy
{
	public class Skeleton : Enemy
	{
		protected override void SetGemsAmount() => gemsAmount = 3;

		public override void Damage(int damageAmount)
		{
			base.Damage(damageAmount);
			animator.SetTrigger("Hit");
			isHit = true;
			animator.SetBool("inCombat", true);
		}
	}
}
