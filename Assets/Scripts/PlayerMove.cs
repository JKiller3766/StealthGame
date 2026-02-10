using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.0f;

    Rigidbody2D rb;
    private Vector2 moveInput;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = moveInput * Speed;
        rb.linearVelocity = (moveInput * Speed);
    }

    // NOTE: InputSystem: "move" action becomes "OnMove" method
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
