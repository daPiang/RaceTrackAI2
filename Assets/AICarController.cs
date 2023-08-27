using UnityEngine;

public class AICarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    public float waypointRadius = 1.0f;

    [Header("Waypoint Settings")]
    [SerializeField] private Waypoint waypoint;
    public Transform currentWaypoint;

    private Rigidbody rb;
    private Quaternion rotationGoal;
    private Vector3 directionToWaypoint;
    private Vector3 waypointFlatPosition;
    private Vector3 carFlatPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNextWaypoint();
    }

    private void Update()
    {
        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        // Flattening Y-coordinates to ensure movement on X-Z plane only
        waypointFlatPosition = new Vector3(currentWaypoint.position.x, 0, currentWaypoint.position.z);
        carFlatPosition = new Vector3(transform.position.x, 0, transform.position.z);

        // Move the car towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypointFlatPosition, moveSpeed * Time.deltaTime);

        // If car reaches the waypoint, switch to the next waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < waypointRadius)
        {
            SetNextWaypoint();
        }

        RotateTowardsWaypoint();
    }

    private void SetNextWaypoint()
    {
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (waypointFlatPosition - carFlatPosition).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);

        // Smoothly rotate the car to face the direction of the waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotationSpeed * Time.deltaTime);
    }
}
