using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
	public float speed;
	public VariableJoystick variableJoystick;
	public FixedJoystick fixedJoystick;
	public DynamicJoystick dynamicJoystick;
	public Rigidbody rb;

	public void FixedUpdate()
	{
		//Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
		//rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

		Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
		rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

		//Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
		//rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
	}
}