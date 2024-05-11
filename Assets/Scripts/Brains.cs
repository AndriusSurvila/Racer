using UnityEngine;

public class Brains : MonoBehaviour
{
    Vehicle vehicle;
    Path path;
    public Vector3 targetPos;
    public float minTurnAngle = 5;

    void Start()
    {
        vehicle = GetComponent<Vehicle>();
        path = FindObjectOfType<Path>();

        transform.position = path.GetClosestPoint(transform.position);
        targetPos = path.GetNextPoint(transform.position);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, targetPos, Color.red);

        if (Vector3.Distance(transform.position, targetPos) < 5)
        {
            targetPos = path.GetNextPoint(transform.position);
        }

        var angle = Vector3.SignedAngle(transform.forward, targetPos - transform.position, Vector3.up);
        if (Mathf.Abs(angle)  > minTurnAngle)
        {
            vehicle.Steer(angle);
        }
        vehicle.Accelerate();
    }
}