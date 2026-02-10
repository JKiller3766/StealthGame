using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public static float TimePast;

    void Update()
    {
        TimePast += Time.deltaTime;
    }

}
