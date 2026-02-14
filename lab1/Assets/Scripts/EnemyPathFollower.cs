using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public EnemyPath path;
    public float waypointReachDistance = 5f;
    public float maxSteerAngle = 25f;

    private int currentWaypoint = 0;
    private EnemyController car;

    private void Start()
    {
        car = GetComponent<EnemyController>();
    }

    private void FixedUpdate()
    {
        if (path == null || path.waypoints.Length == 0)
            return;

        Transform target = path.waypoints[currentWaypoint];

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;
        Vector3 direction = toTarget.normalized;

        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        float steer = Mathf.Clamp(angle, -maxSteerAngle, maxSteerAngle);

        car.SetSteer(steer);

        float absAngle = Mathf.Abs(angle);
        float speedFactor = Mathf.InverseLerp(90f, 0f, absAngle);
        car.SetSpeedMultiplier(speedFactor);

        if (distance < waypointReachDistance ||
    transform.position.z > target.position.z)
        {
            currentWaypoint++;

            if (currentWaypoint >= path.waypoints.Length)
                currentWaypoint = 0; 
        }
    }

    private void ApplySteer(float steer)
    {
        var fl = car.GetComponent<EnemyController>().GetType()
            .GetField("_colliderFL", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(car) as WheelCollider;

        var fr = car.GetComponent<EnemyController>().GetType()
            .GetField("_colliderFR", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(car) as WheelCollider;

        fl.steerAngle = steer;
        fr.steerAngle = steer;
    }
}
