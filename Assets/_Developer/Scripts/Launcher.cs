using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighten : MonoBehaviour
{
    public TrajectoryRenderer TrajectoryRef;
    public Transform LaunchPoint;
    Vector2 DragStartPos, DragCurrentPos, DragReleasePos;
    bool isDragging = false;
    [SerializeField]float LaunchForce;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        else if(Input.GetMouseButtonDown(0) && isDragging)
        {
            Vector2 dragCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 launchDirection = (DragStartPos - dragCurrentPos).normalized;
            float launchStrength = (DragStartPos - dragCurrentPos).magnitude * LaunchForce;

            TrajectoryRef.ShowTrajectory(LaunchPoint.position, launchDirection * launchStrength);
        }
    }
}
