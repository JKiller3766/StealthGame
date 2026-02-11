using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class VisionDetector : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;
    private bool chasing;

    [SerializeField]
    private float time = 3f;

    public static event Action OnChase;
    public static event Action OnStopChase;

    private void OnEnable()
    {
        Chasing.OnStopReturn += StopReturning;
    }

    private void OnDisable()
    {
        Chasing.OnStopReturn -= StopReturning;
    }

    void StopReturning()
    {
        chasing = false;
    }

    private void FixedUpdate()
    {
        var playersDetected = DetectPlayers().Length > 0;

        if (playersDetected && !chasing && time < 2f) {
            if (!chasing)
            {
                Debug.Log("Detectado");
                chasing = true;
                OnChase?.Invoke();
            } else
            {
                time += Time.deltaTime;
            }

        } else if (!playersDetected && chasing && time > 2f)
        {
            Debug.Log("No Detectado");
            time = 0f;
            chasing = false;
            OnStopChase?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
        Vector3 leftBoundary = Quaternion.Euler(0, 0, VisionAngle / 2) * transform.right * DetectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -VisionAngle / 2) * transform.right * DetectionRange;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }

    private Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                PlayerIsVisible(ref players);
            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        bool result = false;
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);

        if (playerColliders.Length != 0)
        {
            result = true;

            foreach (var item in playerColliders)
            {
                players.Add(item.transform);
            }
        }

        return result;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var angle = GetAngle(players[i]);
           
            if (angle > VisionAngle/2)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Vector2.Angle(targetDir, transform.right);
        
        return angle;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var isVisible = IsVisible(players[i]);

            if (!isVisible)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, DetectionRange, WhatIsVisible);

        return (hit.collider.transform == target);
    }
}
