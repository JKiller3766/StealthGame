using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Chasing : MonoBehaviour
{
    public float Speed = 2;
    private Transform player;
    private Vector2 v2;
    private bool chasing = false;
    private bool wasChasing = false;

    public static event Action OnReturn;
    public static event Action OnStopReturn;

    private void Awake()
    {
        if (player != null) player = null;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        v2 = transform.position;
    }

    

    private void OnEnable()
    {
        VisionDetector.OnChase += StartChase;
        VisionDetector.OnStopChase += StopChasing;
    }

    private void OnDisable()
    {
        VisionDetector.OnChase -= StartChase;
        VisionDetector.OnStopChase -= StopChasing;
    }

    private void StartChase()
    {
        if (!chasing && !wasChasing) 
        {
            chasing = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Ending");
        }
    }
       
    void StopChasing()
    {
        if (chasing)
        {
            chasing = false;
            wasChasing = true;
        }
    }

    void FixedUpdate()
    {
        if (chasing)
        {
            Vector2 dir = player.position - this.transform.position;
            this.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
            transform.Rotate(0, 0, GetAnglePlayer());
        } else if (wasChasing) {

            Vector2 dir = v2 - new Vector2(transform.position.x, transform.position.y);
            this.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
            OnReturn?.Invoke();
            transform.Rotate(0, 0, GetAngleReturn());
            if (Vector2.Distance(transform.position, v2) < 0.02f)
            {
                OnStopReturn?.Invoke();
                wasChasing = false;
            }
        }
    }

    private float GetAnglePlayer()
    {
        Vector2 dir = player.position - transform.position;
        float angle = Vector2.Angle(dir, transform.right);
        return angle;
    }

    private float GetAngleReturn()
    {
        Vector2 dir = new Vector2(v2.x-transform.position.x, v2.y - transform.position.y);
        float angle = Vector2.Angle(dir, transform.right);
        return angle;
    }
}