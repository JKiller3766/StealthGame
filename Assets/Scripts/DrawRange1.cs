using UnityEngine;

public class DrawRange1 : MonoBehaviour
{
    [Header("Configuraci�n de L�neas")]
    public LineRenderer visionLine;

    [Header("Ajustes de Visi�n")]
    public float detectionRange = 5f;
    public float visionAngle = 45f;
    [Range(10, 60)] public int segments = 30;

    private void OnEnable()
    {
        VisionDetector1.OnChase += () => SetupLine(visionLine, Color.red, 0.05f);
        VisionDetector1.OnStopChase += () => SetupLine(visionLine, Color.yellow, 0.05f);
    }
    void Start()
    {
        SetupLine(visionLine, Color.yellow, 0.05f);
    }

    void Update()
    {
        DrawVisionCone();
    }

    void SetupLine(LineRenderer line, Color color, float width)
    {
        line.startColor = color;
        line.endColor = color;
        line.startWidth = width;
        line.endWidth = width;
        line.useWorldSpace = true;
        line.alignment = LineAlignment.View;
    }

    void DrawVisionCone()
    {
        if (visionLine == null) return;

        visionLine.positionCount = segments + 2;
        visionLine.SetPosition(0, transform.position);

        float startAngle = -visionAngle / 2;

        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = startAngle + (visionAngle / segments) * i;

            Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * transform.right;
            Vector3 point = transform.position + direction * detectionRange;

            visionLine.SetPosition(i + 1, point);
        }
        visionLine.SetPosition(segments + 1, transform.position);
    }
}