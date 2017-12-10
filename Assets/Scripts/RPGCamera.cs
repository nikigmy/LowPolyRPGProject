using UnityEngine;
using System.Collections;
using System;

public class RPGCamera : MonoBehaviour
{
    public Transform Target;

    public float MaximumDistance;
    public float MinimumDistance;

    public float ScrollModifier;
    public float TurnModifier;
    public float MaxXRotation = 85;
    public bool wasCentered = false;

    Transform m_CameraTransform;

    Vector3 m_LookAtPoint;
    Vector3 m_LocalForwardVector;
    float m_Distance;

    void Start()
    {
        m_CameraTransform = transform.GetChild(0);
        m_LocalForwardVector = m_CameraTransform.forward;

        m_Distance = -m_CameraTransform.localPosition.z / m_CameraTransform.forward.z;
        m_Distance = Mathf.Clamp(m_Distance, MinimumDistance, MaximumDistance);
        m_LookAtPoint = m_CameraTransform.localPosition + m_LocalForwardVector * m_Distance;
    }

    void LateUpdate()
    {
        UpdateDistance();
        UpdateZoom();
        UpdatePosition();
        UpdateRotation();
    }

    void UpdateDistance()
    {
        m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * ScrollModifier, MinimumDistance, MaximumDistance);
    }

    void UpdateZoom()
    {
        m_CameraTransform.localPosition = m_LookAtPoint - m_LocalForwardVector * m_Distance;
    }

    void UpdatePosition()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.transform.position + new Vector3(0, 0, 0);
    }

    void UpdateRotation()
    {
        if (Input.GetMouseButton(0) == true || Input.GetMouseButton(1) == true || Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            var xRotation = -Input.GetAxis("Mouse Y") * TurnModifier * Time.deltaTime * 100;
            var currentRotation = NormalizeRotation(transform.localRotation.eulerAngles.x);
            var cameraRotation = NormalizeRotation(m_CameraTransform.localRotation.eulerAngles.x);
            var futureRotation = currentRotation + xRotation + cameraRotation;
            //Debug.Log(cameraRotation + " " + currentRotation + "  " + futureRotation);
            if (Math.Abs(futureRotation) >= MaxXRotation)
            {
                var rotation = 0.0f;
                if (futureRotation < 0)
                    rotation = (-MaxXRotation - cameraRotation);
                else
                    rotation = MaxXRotation - cameraRotation;

                if (!wasCentered)
                {
                    transform.rotation = Quaternion.Euler(rotation, transform.localRotation.eulerAngles.y, 0);
                }
                Debug.Log(1);
                wasCentered = true;
            }
            else
            {
                if (Math.Abs(xRotation) > 0.1)
                {
                    Debug.Log(2);
                    Debug.Log(futureRotation);
                    transform.Rotate(xRotation, 0, 0);
                    wasCentered = false;
                }
            }
        }

        if (Input.GetMouseButton(0) == true || Input.GetMouseButton(1) == true || Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            var yRotation = Input.GetAxis("Mouse X") * TurnModifier * Time.deltaTime * 100;
            transform.Rotate(0, yRotation, 0);
        }


        if ((Input.GetMouseButton(1) || Input.GetButton("Fire2")) && Target != null)
        {
            Target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    static float NormalizeRotation(float rotation)
    {
        while (rotation > 180)
            rotation -= 360;
        while (rotation < -180)
            rotation += 360;
        return rotation;
    }
}
