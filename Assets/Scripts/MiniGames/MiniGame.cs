using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{

    public event EventHandler win;
    public event EventHandler lose;


    public void Win()
    {
        win.Invoke(this, null);
        Debug.Log("game won!");
    }
    public void Lose()
    {
        lose.Invoke(this, null);
    }

}
