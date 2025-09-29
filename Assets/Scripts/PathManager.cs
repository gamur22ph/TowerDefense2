using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class PathManager : MonoBehaviour
{
    public static PathManager instance { get; private set; }
    public List<Vector2> Paths { get; private set; }
    private EdgeCollider2D pathCollider;

    private void Awake()
    {
        instance = this;
        pathCollider = GetComponent<EdgeCollider2D>();
        Paths = new List<Vector2>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Paths.Add(transform.GetChild(i).position);
        }
        pathCollider.SetPoints(Paths);

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Vector2 GetFirstPoint()
    {
        return Paths[0];
    }

    public Vector2 GetLastPoint()
    {
        return Paths[Paths.Count - 1];
    }

    public Vector2 GetFirstPath()
    {
        if (Paths.Count < 2) return Paths[0];
        return Paths[1];
    }

    public int GetNextPathIdx(int currentPath)
    {
        if (currentPath >= (Paths.Count - 1)) return currentPath;
        currentPath += 1;
        return currentPath;
    }

    private void OnDrawGizmos()
    {
        if (transform.childCount < 2) return;
        Vector2 prevPoint = transform.GetChild(0).position;
        Vector2 currentPoint = Vector2.zero;
        for (int i = 1; i < transform.childCount; i++)
        {
            if (i != 1) prevPoint = currentPoint;
            currentPoint = transform.GetChild(i).position;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(prevPoint, currentPoint);
            
        }
        Vector2 lastDirection = (currentPoint - prevPoint).normalized;

        float arrowLength = 2f;
        float arrowAngleDeg = 30f;
        float arrowAngleRad = arrowAngleDeg * Mathf.Deg2Rad;

        // Rotate direction vector by ±arrowAngle
        float cosA = Mathf.Cos(arrowAngleRad);
        float sinA = Mathf.Sin(arrowAngleRad);

        // Clockwise rotation (right wing)
        Vector2 right = new Vector2(
            cosA * lastDirection.x + sinA * lastDirection.y,
           -sinA * lastDirection.x + cosA * lastDirection.y
        );

        // Counter-clockwise rotation (left wing)
        Vector2 left = new Vector2(
            cosA * lastDirection.x - sinA * lastDirection.y,
            sinA * lastDirection.x + cosA * lastDirection.y
        );

        // Get arrowhead endpoints
        Vector2 rightTip = currentPoint - right * arrowLength;
        Vector2 leftTip = currentPoint - left * arrowLength;
        Gizmos.DrawLine(currentPoint, rightTip);
        Gizmos.DrawLine(currentPoint, leftTip);
    }
}
