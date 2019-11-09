using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
    public Vector3[] controlPoints;
    public LineRenderer lineRenderer;

    private int curveCount = 0;
    private int layerOrder = 0;
    private int SEGMENT_COUNT = 50;


    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
        curveCount = (int)controlPoints.Length / 3;

        DrawCurve();
    }

    void Update()
    {
        //lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
    }

    void DrawCurve()
    {

        var magnitude = 0f;
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex], controlPoints[nodeIndex + 1], controlPoints[nodeIndex + 3]);
                lineRenderer.positionCount = (((j * SEGMENT_COUNT) + i));


                int index = (j * SEGMENT_COUNT) + (i - 1);

                lineRenderer.SetPosition(index, pixel);

                magnitude += index > 0? (lineRenderer.GetPosition(index - 1) - pixel).magnitude : 0;
            }

        }

        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(magnitude, 1f));
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 start, Vector3 controlPoint, Vector3 end)
    {
        Vector3 m1 = Vector3.Lerp(start, controlPoint, t);
        Vector3 m2 = Vector3.Lerp(controlPoint, end, t);
        return Vector3.Lerp(m1, m2, t);
    }
}