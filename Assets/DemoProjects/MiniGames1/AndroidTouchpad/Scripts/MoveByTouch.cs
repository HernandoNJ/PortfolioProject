using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
	public float speed;
	public VariableJoystick variableJoystick;
	public Rigidbody rb;

	//void Update()
	//{
	//    if (Input.touchCount > 0) // if there is any touch
	//    {
	//        Touch touch = Input.GetTouch(0);

	//        // touch coordinates are in pixels. Convert to World Point
	//        // it sets touchPosition.z in the same Camera's position
	//        // set it to zero
	//        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
	//        touchPosition.z = 0f;
	//        print("Position: " + touchPosition);

	//        // move the object to touchPosition
	//        transform.position = touchPosition;
	//    }
	//}


	public void FixedUpdate()
	{
		Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
		rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
	}
}
