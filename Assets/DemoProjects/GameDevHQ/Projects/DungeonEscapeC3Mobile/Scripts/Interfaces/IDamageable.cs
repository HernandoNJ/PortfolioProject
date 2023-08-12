namespace Interfaces
{
	public interface IDamageable
	{
		public int Health { get; set; }

		void Damage(int damageAmount);
	}
}
