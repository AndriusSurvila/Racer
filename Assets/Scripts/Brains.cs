using UnityEngine;

public class Brains : MonoBehaviour
{
    Vehicle vehicle;
    Path path;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
        path = FindObjectOfType<Path>();

        transform.position = path.GetClosestPoint(transform.position);
        path.GetClosestIndex(transform.position);
    }

    void Update()
    {
        vehicle.Steer(Mathf.PerlinNoise1D(Time.time) * 2 - 1);
        vehicle.Accelerate();
    }
}