using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrajectoryRenderer : MonoBehaviour
{
     private LineRenderer lineRenderer;
     private float timeStep = 0.1f;
     private int posCounts = 30;  

     private void Start()
     {
         lineRenderer = GetComponent<LineRenderer>();
     }

     public void ShowTrajectory(Vector2 _startPos, Vector2 _startVelocity)
     {
         lineRenderer.positionCount = posCounts; 
         Vector3[] points = new Vector3[posCounts];

         for (int i = 0; i < posCounts; i++)
         {
             float t = i * timeStep;
             points[i] = CalculatePos(_startPos, _startVelocity, t); 
         }

         lineRenderer.SetPositions(points); 
     }

     Vector3 CalculatePos(Vector2 _startPos, Vector2 _startVelocity, float time)
     {
         return _startPos + _startVelocity * time + 0.5f * Physics2D.gravity * (time * time);
     }


    
}
