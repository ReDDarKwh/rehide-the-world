using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public MiniGame miniGame;
    public Animator animator;
    public int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        miniGame.win += OnWin;
        miniGame.lose += OnLose;
    }

    private void OnLose(object sender, EventArgs e)
    {
    }

    private void OnWin(object sender, EventArgs e)
    {
        animator.SetTrigger("Close");
        var sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
        sceneManager.WonMiniGame();
    }


    void OnSlideOutOver(){
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
