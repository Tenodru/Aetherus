using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    /// <summary>
    /// Called when the player is killed. Resets the scene.
    /// </summary>
    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
