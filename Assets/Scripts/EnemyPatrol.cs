using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsWall;
    public float Speed;
    private bool movingRight = true;

    void FixedUpdate()
    {
        Move();

        if (EdgeDetected()) Flip();
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
}
