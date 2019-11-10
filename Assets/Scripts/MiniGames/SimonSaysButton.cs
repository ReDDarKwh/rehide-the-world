using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButton : MonoBehaviour
{

    public SimonSays simonSays;
    public Animator animator;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        PlayerPress();
    }


    void PlayerPress()
    {

        Debug.Log(name);

        simonSays.ValidateButton(this);
        PressDown();
    }

    internal void PressDown()
    {
        audioSource.Play();
        animator.SetTrigger("LightUp");

        StartCoroutine("PressUp");
    }

    internal IEnumerator PressUp()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.Stop();
    }
}
