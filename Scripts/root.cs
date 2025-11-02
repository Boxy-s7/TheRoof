using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    
    public GameObject pausePanel;
    void Awake()
    {

        LevelStats.Init();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Onkeys();
    }

        public void Onkeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {

      bool isActive = !pausePanel.activeSelf;
        pausePanel.SetActive(isActive);
        Time.timeScale = isActive ? 0f : 1f;

    }

    public void ExitGame()
    {

        Application.Quit();
        Debug.Log("Exit game");

    }
}
