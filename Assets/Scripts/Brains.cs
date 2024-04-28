using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brains : MonoBehaviour
{
    Vehicle vehicle;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    void Update()
    {
        vehicle.Steer(Mathf.PerlinNoise1D(Time.time) * 2 - 1);
        vehicle.Accelerate();
    }
}
