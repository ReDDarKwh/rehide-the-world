using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public bool isDestroyed = false;

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void kill()
    {
        Debug.Log("BOOM!");
        renderer.color = Color.blue;
        isDestroyed = true;
    }
}
