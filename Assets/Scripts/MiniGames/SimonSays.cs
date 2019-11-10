using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MiniGame
{
    public List<SimonSaysButton> buttons;
    public int sequenceLength;
    public List<SimonSaysButton> sequence;
    public int currentSequenceIndex;

    private bool canPlay = false;

    public AudioSource audioSource;
    public AudioClip winAudioClip;
    public AudioClip loseAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidateButton(SimonSaysButton button)
    {

        if (!canPlay)
        {
            return;
        }


        Debug.Log("validate button");
        currentSequenceIndex++;
        if (button == sequence[currentSequenceIndex - 1])
        {
            if (sequence.Count == currentSequenceIndex)
            {
                audioSource.PlayOneShot(winAudioClip);
                Win();
            }
            
        }
        else
        {
            
            audioSource.PlayOneShot(loseAudioClip);
            Restart();
        }

    }

    void Restart()
    {
        currentSequenceIndex = 0;
        sequence = new List<SimonSaysButton>();
        canPlay = false;

        for (var i = 0; i < sequenceLength; i++)
        {
            sequence.Add(buttons[UnityEngine.Random.Range(0, 4)]);
        }

        StartCoroutine("PlaySequence");
    }

    IEnumerator PlaySequence()
    {
        foreach (var b in sequence)
        {
            yield return new WaitForSeconds(1);
            b.PressDown();
        }
        canPlay = true;
    }
}
