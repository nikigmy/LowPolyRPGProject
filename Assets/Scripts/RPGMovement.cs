using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;
[RequireComponent(typeof(CharacterController))]
public class RPGMovement : MonoBehaviour
{
    public GameObject SpawnParticles;
    public bool alive;
    public float ForwardSpeed;
    public float BackwardSpeed;
    public float StrafeSpeed;
    public float RotateSpeed;
    public int strafing;
    CharacterController m_CharacterController;
    Vector3 m_LastPosition;
    Animator m_Animator;

    public Transform Camera;

    float m_AnimatorSpeed;
    Vector3 m_CurrentMovement;
    float m_CurrentTurnSpeed;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!alive)
        {
            ApplyGravityToCharacterController();
            return;
        }
        ResetSpeedValues();

        UpdateRotateMovement();

        UpdateForwardMovement();
        UpdateBackwardMovement();
        UpdateStrafeMovement();

        MoveCharacterController();
        ApplyGravityToCharacterController();

        UpdateAnimation();

        if ((Input.GetMouseButton(1) || Input.GetButton("Fire2")) && Camera != null && alive)
        {
            transform.rotation = Quaternion.Euler(NormalizeRotation(transform.rotation.eulerAngles.x), NormalizeRotation(Camera.rotation.eulerAngles.y), NormalizeRotation(transform.rotation.eulerAngles.x));
        }
        if(Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void Interact()
    {
        var objects = FindObjectsOfType<Interactable>();
        if(objects != null && objects.Length > 0)
        {
            objects[0].Interact();
        }
    }

    void UpdateAnimation()
    {
        Vector3 movementVector = transform.position - m_LastPosition;

        float speed = Vector3.Dot(movementVector.normalized, transform.forward);
        float direction = Vector3.Dot(movementVector.normalized, transform.right);

        if (Mathf.Abs(speed) < 0.2f)
        {
            speed = 0f;
        }

        if (speed > 0.6f)
        {
            speed = 1f;
            direction = 0f;
        }

        if (speed >= 0f)
        {
            if (Mathf.Abs(direction) > 0.7f)
            {
                speed = 1f;
            }
        }

        m_AnimatorSpeed = Mathf.MoveTowards(m_AnimatorSpeed, speed, Time.deltaTime * 5f);
        if(strafing != 0)
        {
            m_Animator.SetFloat("X", strafing);
        }
        else
        {
            m_Animator.SetFloat("X", Input.GetAxis("Horizontal"));
        }

        m_Animator.SetFloat("Y", Input.GetAxis("Vertical"));
        m_Animator.SetFloat("Strafing", strafing != 0 ? 1 : 0);
        m_Animator.SetBool("Moving", strafing != 0 || Math.Abs(Input.GetAxis("Vertical")) > 0.1 || Math.Abs(Input.GetAxis("Horizontal")) > 0.1); 

        m_LastPosition = transform.position;
    }

    void ResetSpeedValues()
    {
        m_CurrentMovement = Vector3.zero;
        m_CurrentTurnSpeed = 0;
    }

    void ApplyGravityToCharacterController()
    {
        m_CharacterController.Move(transform.up * Time.deltaTime * -9.81f);
    }

    void MoveCharacterController()
    {
        m_CharacterController.Move(m_CurrentMovement * Time.deltaTime);
    }

    void UpdateForwardMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.1f)
        {
            m_CurrentMovement = transform.forward * ForwardSpeed;
        }
    }

    void UpdateBackwardMovement()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.1f)
        {
            m_CurrentMovement = -transform.forward * BackwardSpeed;
        }
    }

    void UpdateStrafeMovement()
    {
        strafing = 0;
        if (Input.GetKey(KeyCode.Q) == true)
        {
            m_CurrentMovement = -transform.right * StrafeSpeed;
            strafing = -1;
        }

        if (Input.GetKey(KeyCode.E) == true)
        {
            m_CurrentMovement = transform.right * StrafeSpeed;
            strafing = 1;
        }
    }

    void UpdateRotateMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            m_CurrentTurnSpeed = -RotateSpeed;
            transform.Rotate(0.0f, -RotateSpeed * Time.deltaTime, 0.0f);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            m_CurrentTurnSpeed = RotateSpeed;
            transform.Rotate(0.0f, RotateSpeed * Time.deltaTime, 0.0f);
        }
    }

    static float NormalizeRotation(float rotation)
    {
        while (rotation > 180)
            rotation -= 360;
        while (rotation < -180)
            rotation += 360;
        return rotation;
    }

    public void SpawnEnded()
    {
        alive = true;
        SpawnParticles.SetActive(false);
    }

    private void Die()
    {
        alive = false;
    }
}