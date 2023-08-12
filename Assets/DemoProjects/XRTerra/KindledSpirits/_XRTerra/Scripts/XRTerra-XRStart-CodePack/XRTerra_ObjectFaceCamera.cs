// using.XRT
using UnityEngine;

public class XRTerra_ObjectFaceCamera : MonoBehaviour
{
	Camera cameraToFace;

	// Start is called before the first frame update
	void Start()
	{
		cameraToFace = Camera.main;
	}

	void LateUpdate()
	{
		transform.LookAt(transform.position + cameraToFace.transform.rotation * Vector3.forward, cameraToFace.transform.rotation * Vector3.up);
	}

}
