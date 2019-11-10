using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public Land target;

    public float speed;

    public float curveHeight;

    public float curveOffSet;

    float count = 0.0f;

    public float timeToTarget;

    public int difficultyLevel;


    bool isDestoyed = false;

    public int miniGameIndex;


    Vector3 start;
    Vector3 end;
    Vector3 controlPoint;

    public Animator animator;
    
    public float selectionRadius;

    public event EventHandler<Missile> selected;

    void Start()
    {
        start = this.transform.position;
        end = this.target.transform.position;

        controlPoint = Vector3.Lerp(start, end, 0.5f);
        controlPoint.Set(controlPoint.x, controlPoint.y - (curveHeight - curveOffSet * UnityEngine.Random.value), controlPoint.z);

        var bezier = GetComponent<Bezier>();
        bezier.controlPoints = new[] { start, controlPoint, controlPoint, end };
    }


    private void ExploseOnTarget()
    {
        target.kill();
        DetonateMissile();
    }

    public void DetonateMissile()
    {
        isDestoyed = true;
        Destroy(this.gameObject, 1);
    }

    public void Select(){
        this.animator.SetBool("Selected", true);
    }

    public void UnSelect(){
        this.animator.SetBool("Selected", false);
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            if ((this.transform.position - mousePos).magnitude < selectionRadius)
            {
                selected.Invoke(this, this);
            }
        }

        if (count < 1)
        {
            count += (1 / timeToTarget) * Time.deltaTime;
            Vector3 m1 = Vector3.Lerp(start, controlPoint, count);
            Vector3 m2 = Vector3.Lerp(controlPoint, end, count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        else if (!isDestoyed)
        {
            ExploseOnTarget();
        }
    }
}
