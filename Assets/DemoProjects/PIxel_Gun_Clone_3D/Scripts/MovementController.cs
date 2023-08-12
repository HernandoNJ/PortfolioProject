using UnityEngine;

//* This script is responsible of player movement
//! Remember VERIFY NULL OBJECTS ALWAYS ... Put this color and Debug.LogError 
namespace Assets.DemoProjects.Pixel_Gun_Clone_3D.Scripts
{
	public class MovementController : MonoBehaviour
	{
		[SerializeField] float speed = 5f;
		[SerializeField] GameObject fpsCameraGO;
		Vector3 velocity = Vector3.zero;
		Vector3 mouseRot = Vector3.zero;
		Rigidbody rb;
		float lookSensitivity = 5f;
		float cameraUpDownRotation = 0f;
		float currentCameraUpDownRotation = 0f;

		void Start() => rb = GetComponent<Rigidbody>();

		void Update()
		{
			MovePlayer();
			MouseRotation();

			//Calculate camera rotation look up and down
			float _camUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

			//Apply rotation
			RotateCamera(_camUpDownRotation);
		}

		//Runs physic iterations
		void FixedUpdate()
		{
			if (velocity != Vector3.zero)
			{
				//* MovePosition() makes all physics and collisions checks automatically. It needs a target position to move  
				//* velocity * time = distance
				Vector3 targetPosition = rb.position + velocity * Time.fixedDeltaTime;
				rb.MovePosition(targetPosition);
			}

			Quaternion mouseRotQ = rb.rotation * Quaternion.Euler(mouseRot);
			rb.MoveRotation(mouseRotQ);

			//! Verify if the GO is not empty
			if (fpsCameraGO == null)
				Debug.LogError("fpsCameraGO is empty");
			else
			{
				//Current X axis camera rotation
				currentCameraUpDownRotation -= cameraUpDownRotation;
				//With prev value, camera rotates 360. Clamp to limit rotation range (min-max values) 
				currentCameraUpDownRotation = Mathf.Clamp(currentCameraUpDownRotation, -40, 50);

				fpsCameraGO.transform.localEulerAngles = new Vector3(currentCameraUpDownRotation, 0, 0);
			}
		}

		void MovePlayer()
		{
			// Calculate movement velocity as 3D vector
			float _inpH = Input.GetAxis("Horizontal");
			float _inpV = Input.GetAxis("Vertical");
			Vector3 _moveH = transform.right * _inpH;
			Vector3 _moveV = transform.forward * _inpV;

			// Final movement velocity
			Vector3 _moveVH = (_moveH + _moveV).normalized;
			Vector3 _moveVel = _moveVH * speed;

			velocity = _moveVel;
		}

		void MouseRotation()
		{
			//Calculate rot as 3D vector for turning around
			float _yRot = Input.GetAxis("Mouse X");
			Vector3 _rotVector = new Vector3(0, _yRot, 0) * lookSensitivity;
			mouseRot = _rotVector;
		}

		void RotateCamera(float camUpDownRotation)
		{
			cameraUpDownRotation = camUpDownRotation;
		}
	}
}