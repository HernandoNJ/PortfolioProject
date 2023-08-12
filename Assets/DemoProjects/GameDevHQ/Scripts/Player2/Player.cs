using UnityEngine;
using Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts;

namespace Assets.DemoProjects.GameDevHQ.Scripts.Player2
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float jumpHeight = 20f;
		[SerializeField] private float gravity;
		[SerializeField] private Vector3 direction;
		[SerializeField] private Vector3 velocity;
		[SerializeField] private bool jumpEnabled = false;
		[SerializeField] private bool climbEnabled = false;

		private LedgeChecker activeLedge;
		private CharacterController controller;
		private Animator animator;

		private void Start()
		{
			controller = GetComponent<CharacterController>();
			animator = GetComponentInChildren<Animator>();
		}

		private void Update()
		{
			PlayerMove();
			if (Input.GetKeyDown(KeyCode.E) && climbEnabled)
				ActivateClimb();
		}

		private void PlayerMove()
		{
			if (controller.isGrounded)
			{
				if (jumpEnabled)
				{
					jumpEnabled = false;
					animator.SetBool("jumping", jumpEnabled);
				}

				var moveH = Input.GetAxisRaw("Horizontal");
				direction = new Vector3(0, 0, moveH);
				animator.SetFloat("speed", Mathf.Abs(moveH));

				if (moveH != 0)
				{
					Vector3 facingDirection = transform.localEulerAngles;
					facingDirection.y = direction.z > 0 ? 0 : 180;
					transform.localEulerAngles = facingDirection;
				}

				if (Input.GetKeyDown(KeyCode.Space))
				{
					direction.y += jumpHeight;
					jumpEnabled = true;
					animator.SetBool("jumping", jumpEnabled);
				}
			}

			direction.y -= gravity;
			velocity = direction * speed;
			controller.Move(velocity * Time.deltaTime);
		}

		public void GrabLedge(Vector3 handsPos, LedgeChecker currentLedge)
		{
			controller.enabled = false;
			animator.SetBool("grabLedge", true);
			animator.SetBool("jumping", false);
			animator.SetFloat("speed", 0.0f);
			climbEnabled = true;
			transform.position = handsPos;

			activeLedge = currentLedge;
		}

		private void ActivateClimb()
		{
			animator.SetTrigger("climbUp");
			climbEnabled = false;
		}

		public void ClimbUpComplete()
		{
			transform.position = activeLedge.GetStandPos();
			animator.SetBool("grabLedge", false);
			controller.enabled = true;
		}
	}
}
