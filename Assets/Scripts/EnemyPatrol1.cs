using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class EnemyPatrol1 : MonoBehaviour
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

    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnPoint"))
        {
            if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 0, 180);
    }

    private void OnEnable()
    {
        VisionDetector1.OnChase += StartChase;
        VisionDetector1.OnStopChase += StopChasing;
        Chasing1.OnReturn += Returning;
        Chasing1.OnStopReturn += StopReturning;
    }

    private void OnDisable()
    {
        VisionDetector1.OnChase -= StartChase;
        VisionDetector1.OnStopChase -= StopChasing;
        Chasing1.OnReturn -= Returning;
        Chasing1.OnStopReturn -= StopReturning;
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
