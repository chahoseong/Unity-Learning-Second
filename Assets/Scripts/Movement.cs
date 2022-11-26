using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float boost;
    [SerializeField] private float angularSpeed;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        // 왼쪽으로 회전
        Vector3 rotation = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            // z축으로 x만큼 회전
            rotation.z += angularSpeed;
        }
        // 오른쪽으로 회전
        if (Input.GetKey(KeyCode.D))
        {
            // z축으로 -x만큼 회전
            rotation.z -= angularSpeed;
        }
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.freezeRotation = true;
            Vector3 force = Vector3.up * (boost * Time.deltaTime);
            rigidbody.AddRelativeForce(force);
            rigidbody.freezeRotation = false;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
