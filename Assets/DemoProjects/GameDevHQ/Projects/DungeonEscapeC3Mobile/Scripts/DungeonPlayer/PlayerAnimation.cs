using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.DungeonPlayer
{
	public class PlayerAnimation : MonoBehaviour
	{
		[SerializeField] private Animator animator;
		[SerializeField] private Animator swordAnimator;

		private void Start()
		{
			animator = GetComponentInChildren<Animator>();
			if (animator == null || swordAnimator == null)
				Debug.LogWarning("Place the animator");
		}

		public void SetRunAnim(float speedArg)
		{
			animator.SetFloat("speed", Mathf.Abs(speedArg));
		}

		public void SetJumpAnim(bool jumpArg)
		{
			animator.SetBool("jumping", jumpArg);
		}

		public void SetAttackAnim()
		{
			animator.SetTrigger("attack");
			swordAnimator.SetTrigger("swordAttack");
		}

		public void SetDeathAnim()
		{
			animator.SetTrigger("Death");
		}

	}
}

