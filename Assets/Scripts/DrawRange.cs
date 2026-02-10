using UnityEngine;

public class DetectorVisual : MonoBehaviour
{
    [Header("Configuración de Líneas")]
    public LineRenderer visionLine;
    public LineRenderer circleLine;

    [Header("Ajustes de Visión")]
    public float detectionRange = 5f;
    public float visionAngle = 45f;
    [Range(10, 60)] public int segments = 30;

    void Start()
    {
        SetupLine(visionLine, Color.yellow, 0.05f);
        SetupLine(circleLine, new Color(1, 1, 1, 0.3f), 0.03f);
    }

    void Update()
    {
        DrawVisionCone();
        DrawDetectionCircle();
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

    void DrawDetectionCircle()
    {
        if (circleLine == null) return;

        int circleSegments = 50;
        circleLine.positionCount = circleSegments + 1;
        circleLine.loop = true;

        for (int i = 0; i <= circleSegments; i++)
        {
            float angle = i * (2 * Mathf.PI) / circleSegments;
            float x = Mathf.Cos(angle) * detectionRange;
            float y = Mathf.Sin(angle) * detectionRange;

            Vector3 pos = transform.position + new Vector3(x, y, 0);
            circleLine.SetPosition(i, pos);
        }
    }
}