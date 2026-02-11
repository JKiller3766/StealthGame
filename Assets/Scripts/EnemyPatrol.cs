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
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
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
}
