using System;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetector1 : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange = 5f;
    public float VisionAngle = 90f;

    public float MemoryTime = 0.05f;
    private float timer;

    private bool isChasing = false;

    public static event Action OnChase;
    public static event Action OnStopChase;

    private void FixedUpdate()
    {
        bool canSeePlayer = CheckForPlayer();

        if (canSeePlayer)
        {
            timer = MemoryTime;

            if (!isChasing)
            {
                isChasing = true;
                OnChase?.Invoke();
            }
        }
        else
        {
            if (isChasing)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    isChasing = false;
                    OnStopChase?.Invoke();
                }
            }
        }
    }

    private bool CheckForPlayer()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, DetectionRange, WhatIsPlayer);
        if (playerCol == null) return false;

        Transform target = playerCol.transform;

        Vector2 dirToTarget = (target.position - transform.position).normalized;
        if (Vector2.Angle(transform.right, dirToTarget) > VisionAngle / 2) return false;

        Vector2 origin = transform.position + (Vector3)(dirToTarget * 0.5f);
        float distance = Vector2.Distance(transform.position, target.position);

        RaycastHit2D hit = Physics2D.Raycast(origin, dirToTarget, distance, WhatIsVisible);

        if (hit.collider != null && hit.collider.transform != target)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isChasing ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}