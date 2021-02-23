using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCRotate : MonoBehaviour
{
	[SerializeField]
	private Transform m_currentTarget;

	[SerializeField]
	private Vector3 distanceFromTarget = new Vector3(0f, 0f, -5f);
	float horizontalAngleTarget = 0;
	float horizontalAngleCurrent = 0;

	[SerializeField]
	float verticalAngle = 40;
	float zoomValue = 1;

	float zoomValueSmooth = 1;

	[SerializeField]
	float zoomValueSmoothSpeed = 0.1f;

	[SerializeField]
	float horizontalSpeed = 2.5f;
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
		gameObject.transform.position = pos + m_currentTarget.position;

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
			horizontalAngleTarget -= 90.0f;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			horizontalAngleTarget += 90.0f;
		}

		//wrap around avoid overflow
		if (horizontalAngleTarget < 0.0f)
		{
			horizontalAngleTarget += 360.0f;
			horizontalAngleCurrent += 360.0f;
		}
		else if (horizontalAngleTarget >= 360.0f)
		{
			horizontalAngleTarget -= 360.0f;
			horizontalAngleCurrent -= 360.0f;
		}

		// smoothly approach target angle
		horizontalAngleCurrent += (horizontalAngleTarget - horizontalAngleCurrent) * horizontalSpeed * Time.deltaTime;

		Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngleCurrent, 0);
		Vector3 pos = rotation * distanceFromTarget * zoomValueSmooth;

		return pos;
	}

	public void SetCurrentTarget(Transform target)
	{
		m_currentTarget = target;
		//gameObject.transform.parent = m_currentTarget;
		//gameObject.transform.localPosition = distanceFromTarget;

		//horizontalAngle = horizontalAngleSmooth = 0;
		//zoomValue = zoomValueSmooth = 1;
	}
}
