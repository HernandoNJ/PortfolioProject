using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class Player : MonoBehaviour
	{
		private CharacterController controller;
		[SerializeField] private int lives;
		[SerializeField] private float speed;
		[SerializeField] private float gravity;
		[SerializeField] private float jumpHeight;
		[SerializeField] private bool doubleJumpEnabled;

		public static event Action<int> OnPlayerDied;

		private float yVelocity;
		private bool inGround;

		private void Start()
		{
			lives = 3;
			controller = GetComponent<CharacterController>();
		}

		private void Update()
		{
			float inputH = Input.GetAxis("Horizontal");
			Vector3 direction = new Vector3(inputH, 0, 0);
			Vector3 velocity = direction * speed;

			bool spaceKeyPressed = Input.GetKeyDown(KeyCode.Space);

			inGround = controller.isGrounded;

			if (inGround)
			{
				if (spaceKeyPressed)
				{
					yVelocity = jumpHeight;
					doubleJumpEnabled = true;
				}
			}
			else
			{
				if (spaceKeyPressed && doubleJumpEnabled)
				{
					yVelocity += jumpHeight;
					doubleJumpEnabled = false;
				}

				yVelocity -= gravity;
			}

			velocity.y = yVelocity;
			controller.Move(velocity * Time.deltaTime);
		}

		public void PlayerDamage()
		{
			lives--;
			OnPlayerDied?.Invoke(lives);
			if (lives == 0)
				SceneManager.LoadScene(0);
		}

	}
}
