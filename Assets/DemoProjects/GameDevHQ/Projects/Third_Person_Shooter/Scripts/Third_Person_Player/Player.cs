using UnityEngine;

namespace Assets.DemoProject.GameDevHQ.Projects.Third_Person_Shooter.Scripts.Third_Person_Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private Transform camTransform;
		[SerializeField] private CharacterController controller;
		[Header("Float values")]
		[SerializeField] private float speed = 6;
		[SerializeField] private float jumpPower = 2;
		[SerializeField] private float gravity = 20;
		[SerializeField] private float cameraSensitivity = 2;
		[Header("Vectors")]
		[SerializeField] private Vector3 direction;
		[SerializeField] private Vector3 moveVelocity;
		[SerializeField] private bool grounded;

		private void Start()
		{
			controller = GetComponent<CharacterController>();

			if (Camera.main == null || controller is null)
			{
				Debug.LogWarning("controller or camera NULL");
				return;
			}

			camTransform = Camera.main.transform;
		}

		private void Update()
		{
			PlayerMove();
			CameraRotation();
		}

		private void PlayerMove()
		{
			grounded = controller.isGrounded;

			if (grounded)
			{
				var moveH = Input.GetAxis("Horizontal");
				var moveV = Input.GetAxis("Vertical");

				direction = new Vector3(moveH, 0, moveV);
				moveVelocity = direction * speed;

				if (Input.GetKeyDown(KeyCode.Space))
					moveVelocity.y = jumpPower;

				moveVelocity = transform.TransformDirection(moveVelocity);
			}

			moveVelocity.y -= gravity * Time.deltaTime;
			controller.Move(moveVelocity * Time.deltaTime);
		}

		private void CameraRotation()
		{
			var mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
			var mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

			transform.Rotate(0, mouseX, 0); // rotate player left-right 

			var camRot = camTransform.localEulerAngles; // cam rot xyz
			camRot.x -= mouseY;
			camRot.x = Mathf.Clamp(camRot.x, 0, 30);
			camTransform.localRotation = Quaternion.AngleAxis(camRot.x, new Vector3(1, 0, 0));
		}
	}
}


// private void CameraRotation()
// {
//     var mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
//     var mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;
//
//      var currentRotation = transform.localEulerAngles;
//      currentRotation.y += mouseX;
//     transform.localEulerAngles = currentRotation;
//     transform.rotation = Quaternion.AngleAxis(currentRotation.y,Vector3.up);
//
//     transform.Rotate(0, mouseX, 0); // rotate left-right
//
//     var currentCamRotation = camTransform.localEulerAngles;
//     currentCamRotation.x -= mouseY;
//      camTransform.localEulerAngles = currentCamRotation; --- ok
//     camTransform.rotation = Quaternion.AngleAxis(currentCamRotation.x,Vector3.right); --- not working right
//     camTransform.localRotation = Quaternion.AngleAxis
//     (currentCamRotation.x,Vector3.right); --- NetworkMessageInfo ok 
//
//     camTransform.Rotate(-mouseY, 0, 0);
// }
