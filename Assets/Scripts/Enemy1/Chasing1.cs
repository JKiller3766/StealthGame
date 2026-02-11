using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chasing1 : MonoBehaviour
{
    public float Speed = 3f;

    private Transform player;
    private Vector2 startPosition;

    private bool isChasing = false;
    private bool isReturning = false;

    public static event Action OnReturn;
    public static event Action OnStopReturn;

    private void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        startPosition = transform.position;
    }

    private void OnEnable()
    {
        VisionDetector1.OnChase += StartChase;
        VisionDetector1.OnStopChase += StopChasing;
    }

    private void OnDisable()
    {
        VisionDetector1.OnChase -= StartChase;
        VisionDetector1.OnStopChase -= StopChasing;

        OnReturn = null;
        OnStopReturn = null;
    }

    private void StartChase()
    {
        isChasing = true;
        isReturning = false;
        startPosition = transform.position;
    }

    void StopChasing()
    {
        if (isChasing)
        {
            isChasing = false;
            isReturning = true;
        }
    }

    private void FixedUpdate()
    {
        if (isChasing && player != null)
        {
            MoveAndRotate(player.position);
        }
        else if (isReturning)
        {
            MoveAndRotate(startPosition);
            OnReturn?.Invoke();

            if (Vector2.Distance(transform.position, startPosition) < 0.1f)
            {
                isReturning = false;
                OnStopReturn?.Invoke();
                transform.position = startPosition;
            }
        }
    }

    private void MoveAndRotate(Vector2 targetPos)
    {
        Vector2 direction = targetPos - (Vector2)transform.position;

        transform.position += (Vector3)direction.normalized * Speed * Time.fixedDeltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Ending");
        }
    }
}