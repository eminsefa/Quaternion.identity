using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    int maxReflections = 4;
    int currentReflections = 0;
    int rayDistance = 100;

    LineRenderer lr;
    public List<Vector3> points;

    public GameObject ball;
    Vector2 direction;
    Vector2 firstTouchPosition;
    Vector2 lastTouchPosition;
    Vector2 ballPos;

    void Start()
    {
        points = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
        
    }
    void Update()
    {
        ballPos = ball.transform.position;
    }
    public Vector2 GetFirstTouchPos(Vector2 position)
    {
        firstTouchPosition = position;
        return position;
    }    

    public Vector2 GetLaunchTouchPos(Vector2 position)
    {
        lastTouchPosition = position;
        return position;
    }
    public void DrawLine()
    {
        print(points[0]);
        direction = GetFirstTouchPos(firstTouchPosition) - GetLaunchTouchPos(lastTouchPosition);
        if(direction==Vector2.zero)
        {
            return;
        }

        var hitInfo = Physics2D.Raycast(ballPos, direction, rayDistance,LayerMask.GetMask("Borders", "Asteroids"));
       
            currentReflections = 0;
        if (points.Count !=0)
        {
            points.Clear();
        }

        if (hitInfo)
            {
                ReflectFurther(ballPos, hitInfo);
            }
            else
            {
                points.Add(ballPos + direction.normalized * Mathf.Infinity);
            }
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
        
        
    }
    void ReflectFurther(Vector2 origin, RaycastHit2D hitInfo)
    {
        
        if (currentReflections > maxReflections) { return; }

        points.Add(hitInfo.point);
        currentReflections++;

        Vector2 inDirection = (hitInfo.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitInfo.normal);
        

        var newHitInfo = Physics2D.Raycast(hitInfo.point + (newDirection * 0.00001f), newDirection, rayDistance, LayerMask.GetMask("Borders","Asteroids"));
        if (newHitInfo)
        {
            ReflectFurther(hitInfo.point, newHitInfo);
        }
        else
        {
            points.Add(hitInfo.point + newDirection * rayDistance);
            
        }

    }
}
