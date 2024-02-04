using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed    = 20f;
    public float runningSpeed = 200f;

    Animator m_Animator;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;


    void Start ()
    {
        m_Animator      = GetComponent<Animator>();
        m_Rigidbody     = GetComponent<Rigidbody>();
        m_AudioSource   = GetComponent<AudioSource>();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play ();
            }

        }
        else
        {
             m_AudioSource.Stop ();
        }

        Vector3 desiredForward;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            desiredForward = Vector3.RotateTowards(transform.forward, m_Movement*runningSpeed, turnSpeed * Time.deltaTime, 0f);
        }
        else
        {
            desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        }
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * runningSpeed);
        }
        else
        {
            m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        }
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}