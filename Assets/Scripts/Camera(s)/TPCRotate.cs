using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCRotate : MonoBehaviour
{
	[SerializeField]
	private Transform m_currentTarget;

	[SerializeField]
	private Vector3 distanceFromTarget = new Vector3(0f, 0f, -5f);
	float horizontalAngleDistance = 0;
	float horizontalAngleCurrent = 0;

	[SerializeField]
	float verticalAngle = 40;
	float zoomValue = 1;

	float horizontalAngleSmooth = 0;
	float zoomValueSmooth = 1;


	[SerializeField]
	float zoomValueSmoothSpeed = 0.1f;

	[SerializeField]
	float horizontalSpeed = 0.02f;
	[SerializeField]
	float zoomSpeed = 0.2f;

	[SerializeField]
	float zoomMin = 0.5f;
	[SerializeField]
	float zoomMax = 2.0f;

	private void Start()
	{
		//Needs testing
		//SetCurrentTarget(m_currentTarget);
	}

	private void LateUpdate()
	{
		if (!m_currentTarget)
		{
			return;
		}

		Vector3 pos = ControllerInput();

		gameObject.transform.localPosition = pos;

		Camera camera = GetComponent<Camera>();
		camera.transform.LookAt(m_currentTarget);
	}

	private Vector3 ControllerInput()
	{
		float z = zoomSpeed * Input.GetAxis("Camera Zoom");

		zoomValue = Mathf.Clamp(zoomValue + z, zoomMin, zoomMax);

		zoomValueSmooth = Mathf.Lerp(zoomValueSmooth, zoomValue, zoomValueSmoothSpeed);


		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			horizontalAngleDistance -= 90.0f;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			horizontalAngleDistance += 90.0f;
		}
		//if !true overflow
#if true
		//if true rounding error
		float fraction = horizontalAngleDistance * horizontalSpeed;
		horizontalAngleCurrent += fraction;
		horizontalAngleDistance -= fraction;

		if (horizontalAngleCurrent < 0.0f)
		{
			horizontalAngleCurrent += 360.0f;
		}
		else if (horizontalAngleCurrent > 360.0f)
		{
			horizontalAngleCurrent -= 360.0f;
		}
		Debug.Log(horizontalAngleCurrent);

#endif
		Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngleCurrent, 0);
		Vector3 pos = rotation * distanceFromTarget * zoomValueSmooth;

		return pos;
	}

	/* work in progress
	public void SetCurrentTarget(Transform target)
	{
		m_currentTarget = target;
		gameObject.transform.parent = m_currentTarget;
		gameObject.transform.localPosition = distanceFromTarget;

		horizontalAngle = horizontalAngleSmooth = 0;
		zoomValue = zoomValueSmooth = 1;
	}
	*/
}
