using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    public WaveManager waveManager;
    public PlayerClickManager player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnWave();
    }

    /// <summary>
    /// Checks if button was clicked by player.
    /// </summary>
    /// <returns></returns>
    public bool CheckIfClicked()
    {
        if (player.CheckForHit() == 1)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Interacts with a Wave Manager to spawn wave.
    /// </summary>
    public void SpawnWave()
    {
        if (CheckIfClicked() == true)
        {
            waveManager.WaveSpawner();
            Debug.Log("Wave Spawned.");
        }
    }
}
