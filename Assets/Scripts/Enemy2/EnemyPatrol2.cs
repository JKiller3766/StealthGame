using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class EnemyPatrol2 : MonoBehaviour
{
    private bool chasing = false;
    private bool returning = false;

    void FixedUpdate()
    {
        if (!chasing && !returning) Rotate();
    }

    private void Rotate()
    {
        float angulo = Time.time * 100f;

        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }

    private void OnEnable()
    {
        VisionDetector2.OnChase += StartChase;
        VisionDetector2.OnStopChase += StopChasing;
        Chasing2.OnReturn += Returning;
        Chasing2.OnStopReturn += StopReturning;
    }

    private void OnDisable()
    {
        VisionDetector2.OnChase -= StartChase;
        VisionDetector2.OnStopChase -= StopChasing;
        Chasing2.OnReturn -= Returning;
        Chasing2.OnStopReturn -= StopReturning;
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
        returning = false;
    }
}
