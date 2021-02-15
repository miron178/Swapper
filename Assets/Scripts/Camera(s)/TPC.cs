using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPC : MonoBehaviour
{
	[SerializeField]
	private Transform m_currentTarget;

	[SerializeField]
	private Vector3 distanceFromTarget = new Vector3(0f, 0f, -5f);
	float horizontalAngle = 0;
	float verticalAngle = 0;
	float zoomValue = 1;

	float horizontalAngleSmooth = 0;
	float verticalAngleSmooth = 0;
	float zoomValueSmooth = 1;

	[SerializeField]
	float horizontalAngleSmoothSpeed = 0.1f;
	[SerializeField]
	float verticalAngleSmoothSpeed = 0.1f;
	[SerializeField]
	float zoomValueSmoothSpeed = 0.1f;

	[SerializeField]
	float horizontalSpeed = 0.2f;
	[SerializeField]
	float verticalSpeed = 0.2f;
	[SerializeField]
	float zoomSpeed = 0.2f;

	[SerializeField]
	float verticalAngleMin = -30;
	[SerializeField]
	float verticalAngleMax = +30;

	[SerializeField]
	float zoomMin = 0.5f;
	[SerializeField]
	float zoomMax = 2.0f;

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
		float v = verticalSpeed * Input.GetAxis("Camera Vertical");
		float h = horizontalSpeed * Input.GetAxis("Camera Horizontal");
		float z = zoomSpeed * Input.GetAxis("Camera Zoom");

		horizontalAngle = (horizontalAngle + h) % 360;
		verticalAngle = Mathf.Clamp(verticalAngle + v, verticalAngleMin, verticalAngleMax);
		zoomValue = Mathf.Clamp(zoomValue + z, zoomMin, zoomMax);

		horizontalAngleSmooth = Mathf.Lerp(horizontalAngleSmooth, horizontalAngle, horizontalAngleSmoothSpeed);
		verticalAngleSmooth = Mathf.Lerp(verticalAngleSmooth, verticalAngle, verticalAngleSmoothSpeed);
		zoomValueSmooth = Mathf.Lerp(zoomValueSmooth, zoomValue, zoomValueSmoothSpeed);

		Quaternion rotation = Quaternion.Euler(verticalAngleSmooth, horizontalAngleSmooth, 0);
		Vector3 pos = rotation * distanceFromTarget * zoomValueSmooth;

		return pos;
	}
}
