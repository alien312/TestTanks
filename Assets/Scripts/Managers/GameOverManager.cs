using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Controls Controls;

    void Awake()
    {
        //Controls.Player.UIRestart.performed += ctx => SceneManager.LoadScene(0);
        Controls.Player.UIExit.performed += ctx => Application.Quit();
    }

    private void OnEnable()
    {
        Controls.Player.UIExit.Enable();
    }

    private void OnDisable()
    {
        Controls.Player.UIExit.Disable();
    }
}
