using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vehicle vehicle;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            vehicle.Accelerate();
        }

        if (Input.GetKey(KeyCode.S))
        {
            vehicle.Brake();
        }

        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0) vehicle.Steer(horizontal);
    }
}
