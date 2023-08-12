using Interfaces;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.DungeonPlayer
{
	public class Player : MonoBehaviour, IDamageable
	{
		private Rigidbody2D rb;
		private PlayerAnimation playerAnim;
		private SpriteRenderer spriteRend;

		[SerializeField] private GameObject swordArcGO;
		[SerializeField] private SpriteRenderer swordArcRend;
		[SerializeField] private float jumpForce = 5f;
		[SerializeField] private float speed = 5f;
		[SerializeField] private float startHealth;
		[SerializeField] private bool resetJump;
		[SerializeField] private bool isGrounded;
		[SerializeField] private LayerMask groundLayerMask;

		public int Health { get; set; }

		public int diamonds;

		private void Start()
		{
			startHealth = 4;
			Health = (int)startHealth;

			rb = GetComponent<Rigidbody2D>();
			playerAnim = GetComponent<PlayerAnimation>();
			spriteRend = transform.Find("Sprite").GetComponent<SpriteRenderer>();
			swordArcRend = swordArcGO.GetComponent<SpriteRenderer>();

			if (rb == null || playerAnim == null || spriteRend == null || swordArcGO == null || swordArcRend == null)
				Debug.LogWarning("Set required components in player script");
		}

		private void Update()
		{
			MovePlayer();
			CheckGrounded();
		}

		private void CheckGrounded()
		{
			Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.green);

			var hitInfo = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: 0.8f,
					layerMask: groundLayerMask);

			if (hitInfo.collider != null)
			{
				isGrounded = true;
				if (resetJump == false)
					playerAnim.SetJumpAnim(false);
			}
			else
				isGrounded = false;
		}

		private void MovePlayer()
		{
			if (Health < 1)
				return;

			//var moveH = Input.GetAxisRaw("Horizontal") * speed;
			var moveH = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
			var spacePressed = Input.GetKeyDown(KeyCode.Space);
			var leftBtnPressed = Input.GetMouseButtonDown(0);
			var ABtnPressed = CrossPlatformInputManager.GetButtonDown("A_btn"); // Attack
			var BBtnPressed = CrossPlatformInputManager.GetButtonDown("B_btn"); // Jump

			var flipped = moveH < 0;
			FlipPlayer(flipped);

			if ((spacePressed || BBtnPressed) && isGrounded)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				StartCoroutine(ResetJumpRoutine());
				playerAnim.SetJumpAnim(true);
			}

			if (ABtnPressed && isGrounded)
				playerAnim.SetAttackAnim();

			rb.velocity = new Vector2(moveH, rb.velocity.y);
			playerAnim.SetRunAnim(moveH);
		}

		private void FlipPlayer(bool isFlipped)
		{
			spriteRend.flipX = isFlipped;
			swordArcRend.flipX = isFlipped;
			swordArcRend.flipY = isFlipped;

			var newPos = swordArcGO.transform.localPosition;
			if (isFlipped)
				newPos.x = -1f;
			else
				newPos.x = 1;
			swordArcGO.transform.localPosition = newPos;
		}

		private IEnumerator ResetJumpRoutine()
		{
			resetJump = true;

			yield return new WaitForSeconds(0.1f);
			resetJump = false;
		}

		public void Damage(int damageAmount)
		{
			if (Health < 1)
				return;
			Health -= damageAmount;
			UIManager.Instance.UpdateLives(Health);
			if (Health < 1)
				playerAnim.SetDeathAnim();
		}

		public void AddGems(int amount)
		{
			diamonds += amount;
			UIManager.Instance.UpdateUIGemsCount(diamonds);
		}
	}
}
