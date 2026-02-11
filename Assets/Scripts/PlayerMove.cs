using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField] 
    public static float DistanceMade = 0f;

    [SerializeField]
    public GameObject sprite;


    Rigidbody2D rb;
    private Vector2 moveInput;

    private Vector2 lastPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * speed;
        rb.linearVelocity = (moveInput * speed);

        DistanceMade += Vector2.Distance(rb.position, lastPosition);

        lastPosition = rb.position;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

            sprite.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
