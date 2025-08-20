using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RepetitionMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float amplitude = 1.0f; 

    private Vector3 startPos;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        float zOffset = Mathf.Sin(Time.time * moveSpeed) * amplitude;
        Vector3 newPosition = new Vector3(startPos.x, startPos.y, startPos.z + zOffset);
        rb.MovePosition(newPosition);
    }
}

