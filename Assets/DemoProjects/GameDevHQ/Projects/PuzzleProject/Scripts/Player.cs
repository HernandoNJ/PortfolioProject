using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.DemoProjects.GameDevHQ.Projects.PuzzleProject.Scripts
{
	public class Player : MonoBehaviour
	{
		private CharacterController _controller;
		[SerializeField] private float _speed = 5.0f;
		[SerializeField] private float _gravity = 1.0f;
		[SerializeField] private float _jumpHeight = 15.0f;
		[SerializeField] private float _pushPower = 2f;
		[SerializeField] private int _coins;
		[SerializeField] private int _lives = 3;
		[SerializeField] private Vector3 _wallSurfaceNormal;

		public float _yVelocity;
		private bool _canDoubleJump = false;
		private bool _wallJumpEnabled = false;
		private UIManager _uiManager;
		private Vector3 _direction, _velocity, _pushDirection;

		void Start()
		{
			_controller = GetComponent<CharacterController>();
			_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
			if (_uiManager == null)
				Debug.LogError("The UI Manager is NULL.");
			_uiManager.UpdateLivesDisplay(_lives);
		}

		void Update()
		{
			float horizontalInput = Input.GetAxis("Horizontal");

			if (_controller.isGrounded)
			{
				_direction = new Vector3(horizontalInput, 0, 0);
				_velocity = _direction * _speed;

				_wallJumpEnabled = true;

				if (Input.GetKeyDown(KeyCode.Space))
				{
					_yVelocity = _jumpHeight;
					_canDoubleJump = true;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space) && _wallJumpEnabled == false)
				{
					if (_canDoubleJump)
					{
						_yVelocity += _jumpHeight;
						_canDoubleJump = false;
					}
				}

				else if (Input.GetKeyDown(KeyCode.Space) && _wallJumpEnabled)
				{
					_yVelocity = _jumpHeight;
					_velocity = _wallSurfaceNormal * _speed;
				}

				_yVelocity -= _gravity;
			}

			_velocity.y = _yVelocity;

			_controller.Move(_velocity * Time.deltaTime);
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (hit.gameObject.CompareTag("MovingBox"))
			{
				Rigidbody otherRb = hit.collider.attachedRigidbody;

				if (otherRb != null)
				{
					//Vector3 pushDirection = hit.moveDirection;
					_pushDirection = new Vector3(hit.moveDirection.x, 0, 0);
					otherRb.velocity = _pushDirection * _pushPower;
				}
			}
			if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
			{
				_wallSurfaceNormal = hit.normal;
				_wallJumpEnabled = true;
				Debug.DrawRay(hit.point, hit.normal, Color.blue);
			}
		}

		public void AddCoins()
		{
			_coins++;

			_uiManager.UpdateCoinDisplay(_coins);
		}

		public void Damage()
		{
			_lives--;

			_uiManager.UpdateLivesDisplay(_lives);

			if (_lives < 1)
			{ SceneManager.LoadScene(0); }
		}

		public int CoinsCount() => _coins;
	}
}
