using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcLineRenderer : MonoBehaviour
{
    public LineRenderer lr;

    //these are current default "ideal jump" values
    public float maxVelocity = 9f;
    public float angle = 35f;
    public int resolution = 20;

    public float currentVelocity = 0f;

    private float g; //force of gravity on the y axis
    private float radianAngle;
    private bool shouldRenderNew = false;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
    }

    private void OnValidate()
    {
        if (lr != null && Application.isPlaying)
        {
            RenderArc();
        }
    }
    private void Start()
    {
        currentVelocity = 0f;
    }

    private void Update()
    {
        if(shouldRenderNew)
            RenderArc();
    }

    public void ChangeVelocityValue(float multiplier)
    {
        currentVelocity = multiplier * maxVelocity;
        shouldRenderNew = true;
    }

    //initialization
    void RenderArc()
    {
        print("rendering new arc " + lr.enabled);
        lr.enabled = true;
        // obsolete: lr.SetVertexCount(resolution + 1);
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
        shouldRenderNew = false;
    }

    //Create an array of Vector 3 positions for the arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (currentVelocity * currentVelocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float z = t * maxDistance;
        float y = z * Mathf.Tan(radianAngle) - ((g * z * z) / (2 * currentVelocity * currentVelocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(0f, y, z);
    }
}