using UnityEngine;

public class DrawRange1 : MonoBehaviour
{
    public LineRenderer VisionLine;

    public float DetectionRange = 5f;
    public float VisionAngle = 45f;
    [Range(10, 60)] public int Segments = 30;

    private void OnEnable()
    {
        VisionDetector1.OnChase += CambiarARojo;
        VisionDetector1.OnStopChase += CambiarAAmarillo;
    }
    private void OnDisable()
    {
        VisionDetector1.OnChase -= CambiarARojo;
        VisionDetector1.OnStopChase -= CambiarAAmarillo;
    }

    void Start()
    {
        SetupLine(VisionLine, Color.yellow, 0.05f);
    }

    void Update()
    {
        DrawVisionCone();
    }

    private void CambiarARojo() => SetupLine(VisionLine, Color.red, 0.05f);
    private void CambiarAAmarillo() => SetupLine(VisionLine, Color.yellow, 0.05f);

    void SetupLine(LineRenderer line, Color color, float width)
    {
        if (line == null) return;

        line.startColor = color;
        line.endColor = color;
        line.startWidth = width;
        line.endWidth = width;
        line.useWorldSpace = true;
        line.alignment = LineAlignment.View;
    }

    void DrawVisionCone()
    {
        if (VisionLine == null) return;

        VisionLine.positionCount = Segments + 2;
        VisionLine.SetPosition(0, transform.position);

        float startAngle = -VisionAngle / 2;

        for (int i = 0; i <= Segments; i++)
        {
            float currentAngle = startAngle + (VisionAngle / Segments) * i;

            Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * transform.right;
            Vector3 point = transform.position + direction * DetectionRange;

            VisionLine.SetPosition(i + 1, point);
        }
        VisionLine.SetPosition(Segments + 1, transform.position);
    }
}