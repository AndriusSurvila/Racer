using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float acceleration = 1.0f;
    public float brakeAcceleration = 3f;
    public float rotateSpeed = 1.0f;
    public float reverseAcceleration = 1f;
    Rigidbody rb;
    public AudioSource engineSound;
    public AnimationCurve pitchCurve;
    public AnimationCurve rotateSpeedCurve;
    public float speedRatio;
    public float sightDrag = 1f;
    public float drag = 5f;
    public bool isAccelerating;
    public Transform[] wheels;
    Vector3 localVelocity;
    public ParticleSystem brakeParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        print((int)(rb.velocity.magnitude) + "m/s");

        speedRatio = rb.velocity.magnitude / maxSpeed;
        engineSound.pitch = pitchCurve.Evaluate(speedRatio);

        localVelocity = transform.InverseTransformVector(rb.velocity);

        rb.velocity += -transform.right * localVelocity.x * sightDrag * Time.deltaTime;

        if (!isAccelerating)
        {
            rb.velocity += -transform.forward * localVelocity.z * drag * Time.deltaTime;
        }
        
        if (Mathf.Abs(localVelocity.normalized.x) > 0.5f)
        {
            print("drift");
            var emmision = brakeParticles.emission;
            emmision.enabled = true;
        }
        else
        {
            var emmision = brakeParticles.emission;
            emmision.enabled = false;
        }

        isAccelerating = false;
        brakeParticles.Stop();
    }

    public void Steer(float value)
    {
        value = Mathf.Clamp(value, -1, 1);
        transform.Rotate(0, value * rotateSpeed * rotateSpeedCurve.Evaluate(speedRatio) * Mathf.Sign(localVelocity.z) * Time.deltaTime, 0);

        foreach (var wheel in wheels)
        {
            float rotationAngle = value * rotateSpeed * rotateSpeedCurve.Evaluate(speedRatio) * speedRatio;
            wheel.Rotate(new Vector3(rotationAngle, 0, 0));
        }
    }

    public void Accelerate()
    {
        rb.velocity += transform.forward * acceleration * Time.deltaTime;
        isAccelerating = true;
    }

    public void Brake()
    {
        var acc = speedRatio > 0 ? brakeAcceleration : reverseAcceleration;
        rb.velocity += -transform.forward * acc * Time.deltaTime;
    }
}