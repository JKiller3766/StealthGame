using UnityEngine;

public class Chasing : MonoBehaviour
{
    public float Speed = 2;
    private Transform player;
    private Vector2 v2;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        v2= transform.position;
    }

    private void OnEnable()
    {
        VisionDetector.OnChase += StartChase;
        VisionDetector.OnStopChase += Volver;
    }

    private void StartChase()
    {
            Vector2 dir = player.position - this.transform.position;
            this.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡He chocado con el jugador!");
        }
    }
       
    private void Volver()
    {
        while (v2.x != transform.position.x && v2.y != transform.position.y)
        {   
            Vector2 dir = v2 - new Vector2(transform.position.x, transform.position.y);
            this.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
        }
    }
}