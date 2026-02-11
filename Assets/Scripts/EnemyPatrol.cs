using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class EnemyPatrol : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsWall;
    public float Speed;
    private bool movingRight = true;
    private bool chasing = false;
    private bool returning = false;

    void FixedUpdate()
    {
        if (!chasing && !returning) Move();

        if (EdgeDetected() && !chasing && !returning) Flip();
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit;

        if (movingRight)
        {
            hit = Physics2D.Raycast(EdgedetectionPoint.position, Vector2.right, 1.5f, WhatIsWall);

        } else {
            hit = Physics2D.Raycast(EdgedetectionPoint.position, Vector2.left, 1.5f, WhatIsWall);
        }

        if (hit.collider != null)
        {
            movingRight = false;
            return true;
        }
        return false;
    }

    private void Flip()
    {
        transform.Rotate(0, 0, 180);
    }

    private void OnEnable()
    {
        VisionDetector.OnChase += StartChase;
        VisionDetector.OnStopChase += StopChasing;
        Chasing.OnReturn += Returning;
        Chasing.OnStopReturn += StopReturning;
    }

    private void OnDisable()
    {
        VisionDetector.OnChase -= StartChase;
        VisionDetector.OnStopChase -= StopChasing;
        Chasing.OnReturn -= Returning;
        Chasing.OnStopReturn -= StopReturning;
    }

    private void StartChase()
    {
        if (!chasing)
        {
            chasing = true;
        }
    }
    void StopChasing()
    {
        if (chasing)
        {
            chasing = false;
        }
    }

    void Returning()
    {
        returning = true;
    }

    void StopReturning()
    {
        transform.localRotation = Quaternion.identity;
        
        returning = false;
    }
}
