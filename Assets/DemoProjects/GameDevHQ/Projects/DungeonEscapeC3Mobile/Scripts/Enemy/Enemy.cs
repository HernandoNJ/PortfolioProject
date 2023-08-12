using Interfaces;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.Enemy
{
	public abstract class Enemy : MonoBehaviour, IDamageable
	{
		[SerializeField] protected Player player;
		[SerializeField] protected Animator animator;
		[SerializeField] protected SpriteRenderer spriteRend;
		[SerializeField] protected Transform pointA, pointB;
		[SerializeField] private GameObject diamondPrefab;
		[SerializeField] protected Vector3 currentTarget;
		[SerializeField] protected int startHealth, gemsAmount;
		[SerializeField] protected bool isHit;
		[SerializeField] protected bool moveEnabled;
		[SerializeField] protected bool isDead;
		[SerializeField] private float speed;

		public int Health { get; set; }

		protected abstract void SetGemsAmount();

		protected void Start()
		{
			Init();
		}

		protected virtual void Update()
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
				animator.GetBool("inCombat") == false || moveEnabled == false)
				return;

			EnemyMove();
		}

		protected virtual void Init()
		{
			Health = startHealth;
			moveEnabled = true;

			animator = GetComponentInChildren<Animator>();
			player = GameObject.FindWithTag("Player").GetComponent<Player>();
			spriteRend = GetComponentInChildren<SpriteRenderer>();

			if (animator == null || spriteRend == null || player == null)
				Debug.LogWarning("Component missing in Enemy script");

			SetGemsAmount();
		}

		protected virtual void EnemyMove()
		{
			if (currentTarget == pointA.position)
				spriteRend.flipX = true;
			if (currentTarget == pointB.position)
				spriteRend.flipX = false;

			if (transform.position.x <= pointA.position.x)
			{
				currentTarget = pointB.position;
				animator.SetTrigger("Idle");
			}
			else if (transform.position.x >= pointB.position.x)
			{
				currentTarget = pointA.position;
				animator.SetTrigger("Idle");
			}

			if (isHit == false)
				transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

			var distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

			if (distance > 2f)
			{
				isHit = false;
				animator.SetBool("inCombat", false);
			}

			//direction = player pos - enemy pos (target - origin)
			var direction = player.transform.localPosition - transform.localPosition;

			//direction.x: if positive, player is at right, else it is at left
			if (direction.x > 0 && animator.GetBool("inCombat"))
				spriteRend.flipX = false;
			else if (direction.x < 0 && animator.GetBool("inCombat") == true)
				spriteRend.flipX = true;
		}

		public virtual void Damage(int damageAmount)
		{
			if (isDead)
				return;

			Health -= damageAmount;

			if (Health < 1)
			{
				isDead = true;
				moveEnabled = false;
				animator.SetTrigger("Death");
				StartCoroutine(DestroyedEnemyRoutine());
			}
		}

		private IEnumerator DestroyedEnemyRoutine()
		{
			yield return new WaitForSeconds(1);
			var diamondObj = Instantiate(diamondPrefab, transform.position, quaternion.identity);
			diamondObj.GetComponent<Diamond>().gems = gemsAmount;
			Destroy(gameObject);
		}
	}
}
