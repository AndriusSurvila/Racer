using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        print((int)(rb.velocity.magnitude * 3.6f) + "km/h");    
    }

    public void Steer(float value)
    {
        transform.Rotate(0, value * rotateSpeed * Time.deltaTime, 0);
    }

    public void Accelerate()
    {
        rb.velocity += transform.forward * speed * Time.deltaTime;
    }

    public void Brake()
    {

    }
}