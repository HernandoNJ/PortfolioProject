using UnityEngine;
using Assets.DemoProjects.GameDevHQ.Scripts.Player1;

namespace Assets.DemoProjects.GameDevHQ.Scripts.Enemy
{
	public enum EnemyState { Idle, Chase, Attack }

	namespace Enemy
	{
		public class EnemyAI : MonoBehaviour
		{
			[SerializeField] private EnemyState currentState;
			[SerializeField] private CharacterController controller;
			[SerializeField] private Transform target;
			[SerializeField] private Health playerHealth;
			[SerializeField] private float speed = 5f;
			[SerializeField] private float gravity = 9.8f;
			[SerializeField] private float attackCooldown = 1.5f;
			[SerializeField] private float nextAttack = 1.5f;
			[SerializeField] private bool grounded;
			[SerializeField] private Vector3 direction;
			[SerializeField] private Vector3 moveVelocity;

			private void Start()
			{
				controller = GetComponent<CharacterController>();
				target = GameObject.FindWithTag("Player").GetComponent<Transform>();
				playerHealth = target.GetComponent<Health>();

				if (controller == null || target == null || playerHealth == null)
					Debug.LogWarning("References missing in enemy...");

				currentState = EnemyState.Chase;
			}

			private void Update()
			{
				CheckCurrentState();
				RotateEnemy();
			}

			private void MoveEnemy()
			{
				grounded = controller.isGrounded;

				if (grounded && target != null)
				{
					direction = target.position - transform.position;
					direction.y = 0;
					moveVelocity = direction.normalized * speed;
				}

				if (!grounded)
					moveVelocity.y -= gravity * Time.deltaTime;
				controller.Move(moveVelocity * Time.deltaTime);
			}

			private void AttackPlayer()
			{
				if (Time.time > nextAttack)
				{
					if (playerHealth != null)
						playerHealth.Damage(10);
					nextAttack = Time.time + attackCooldown;
				}
			}

			private void CheckCurrentState()
			{
				switch (currentState)
				{
					case EnemyState.Chase:
						MoveEnemy();
						break;
					case EnemyState.Attack:
						AttackPlayer();
						break;
				}
			}

			private void RotateEnemy()
				=> transform.localRotation = Quaternion.LookRotation(direction);

			public void EnemyAttack() => currentState = EnemyState.Attack;

			public void EnemyChase() => currentState = EnemyState.Chase;

		}
	}
}

// direction = target.position - transform.position;
// moveVelocity = Vector3.Normalize(direction) * speed *Time.deltaTime; --- ok
// moveVelocity = direction.normalized * speed * Time.deltaTime; ---ok
