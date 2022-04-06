using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI management utility.
/// </summary>
public class UIManager : MonoBehaviour
{
    private bool paused;
    GameObject[] pauseObjects;
    public WaveManager waveManager;
    public GameObject waveDisplay;

    void Start()
    {
        paused = false;
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();
    }

    // Update is called once per frame
    void Update()
    {

        //Uses the escape button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                paused = true;
                ShowPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                paused = false;
                HidePaused();
            }
        }
    }

    /// <summary>
    /// Controls pausing.
    /// </summary>
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            paused = false;
            HidePaused();
        }
    }

    /// <summary>
    /// Shows objects with ShowOnPause tag.
    /// </summary>
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    /// <summary>
    /// Hides objects with ShowOnPause tag.
    /// </summary>
    public void HidePaused()
    {
        Cursor.visible = false;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if game is paused.
    /// </summary>
    /// <returns></returns>
    public bool IsPaused()
    {
        return paused;
    }

    /// <summary>
    /// Quits game (disabled for now)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
        //SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Displays wave number as a string.
    /// </summary>
    /// <returns></returns>
    public string ShowWave()
    {
        return ("Wave: " + waveManager.GetWave());
    }

    public void ChangeWaveUI()
    {
        waveDisplay.GetComponent<TextMesh>().text = ShowWave();
        Debug.Log("Changed wave display.");
    }
}
