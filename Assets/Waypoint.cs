using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;

    private void OnDrawGizmos()
    {
        DrawWaypoints();
        DrawLinesBetweenWaypoints();
    }

    private void DrawWaypoints()
    {
        Gizmos.color = Color.blue;
        foreach (Transform waypoint in transform)
        {
            Gizmos.DrawSphere(waypoint.position, waypointSize);
        }
    }

    private void DrawLinesBetweenWaypoints()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform start = transform.GetChild(i);
            Transform end = (i < transform.childCount - 1) ? transform.GetChild(i + 1) : transform.GetChild(0);
            Gizmos.DrawLine(start.position, end.position);
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        int nextIndex = (currentWaypoint.GetSiblingIndex() + 1) % transform.childCount;
        return transform.GetChild(nextIndex);
    }
}
