using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float acceleration = 12.0f; // Controla la resposta

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Aquest mètode s'executa cada vegada que premis o deixis anar una tecla
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Calculem la velocitat objectiu
        Vector2 targetVelocity = movementInput * Speed;

        // Fem que la velocitat actual s'apropi a l'objectiu de forma fluida
        // Això corregirà el "bloqueig" que sents i farà el canvi més orgànic
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
    }
}