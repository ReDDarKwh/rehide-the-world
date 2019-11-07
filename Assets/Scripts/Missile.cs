using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public Land target;

    public float speed;

    public float curveHeight;

    float count = 0.0f;

    Vector3 start;
    Vector3 end;
    Vector3 controlPoint;

    void Start()
    {
        start = this.transform.position;
        end = this.target.transform.position;

        controlPoint = Vector3.Lerp(start, end, 0.5f);
        controlPoint.Set(controlPoint.x, controlPoint.y - curveHeight, controlPoint.z);

        var bezier = GetComponent<Bezier>();


    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1.0f) {
                count += 1.0f * Time.deltaTime;
                Vector3 m1 = Vector3.Lerp( start, controlPoint, count );
                Vector3 m2 = Vector3.Lerp( controlPoint, end, count );
                transform.position = Vector3.Lerp(m1, m2, count);
        }
    }
}
