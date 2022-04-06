using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wave management utility.
/// </summary>
public class WaveManager : MonoBehaviour
{
    //Transforms of the bridges at which enemies can spawn.
    public Transform enemySpawnW;
    public Transform enemySpawnNW;
    public Transform enemySpawnNE;
    public Transform enemySpawnE;
    public Transform enemySpawnSE;
    public Transform enemySpawnS;
    public Transform enemySpawnSW;

    //References to spawnable enemies.
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    private int wave;
    private int spawnpointCount;
    private bool waveInProgress;

    List <Transform> spawnpoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        spawnpointCount = 7;
        waveInProgress = false;
        addSpawnpoints();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWave();
    }

    /// <summary>
    /// Adds enemy spawnpoints to a Transform List.
    /// </summary>
    private void addSpawnpoints()
    {
        spawnpoints.Add(enemySpawnW);
        spawnpoints.Add(enemySpawnNW);
        spawnpoints.Add(enemySpawnNE);
        spawnpoints.Add(enemySpawnE);
        spawnpoints.Add(enemySpawnSE);
        spawnpoints.Add(enemySpawnS);
        spawnpoints.Add(enemySpawnSW);
    }

    /// <summary>
    /// Returns the current wave number.
    /// </summary>
    /// <returns> 
    /// The current wave number. 
    /// </returns>
    public int GetWave()
    {
        return wave;
    }

    /// <summary>
    /// Increases wave number by 1.
    /// </summary>
    public void AddWave()
    {
        wave++;
    }

    /// <summary>
    /// Changes wave number using AddWave().
    /// </summary>
    public void ChangeWave()
    {
        if (CheckEnemyNumber(1) == 0 && WaveInProgress())
        {
            Debug.Log("All enemy1 defeated.");
            AddWave();
            WaveInProgressChange(false);
        }
    }

    /// <summary>
    /// Spawns the wave.
    /// </summary>
    public void WaveSpawner()
    {
        for (int bridge = 0; bridge < SelectedBridges().Count; bridge++)
        {
            for (int count = 1; count < SpawnPool().Capacity; count++)
            {
                Instantiate(enemy1, new Vector3(SelectedBridges()[bridge].position.x, SelectedBridges()[bridge].position.y + 3, SelectedBridges()[bridge].position.z), SelectedBridges()[bridge].rotation);
                WaveInProgressChange(true);
                Debug.Log("Spawned");
            }
        }
        
    }

    /// <summary>
    /// Randomly chooses which bridges to spawn enemies at.
    /// </summary>
    /// <returns> An Integer List. </returns>
    public List<int> ChooseSpawnpoints()
    {
        List<int> selected = new List<int>();
        if (GetWave() < 5)
        {
            int i = 1;
            Debug.Log("Wave is less than 5");
            while (i <= 3)
            {
                int rand = Random.Range(0, spawnpoints.Count);
                if (CheckForDuplicate(selected, rand) == false)
                {
                    selected.Add(rand);
                    i++;
                    //Debug.Log(rand);
                }
            }
        }
        else if (GetWave() >= 5 && GetWave() <= 8)
        {
            int i = 1;
            while (i <= 4)
            {
                int rand = Random.Range(0, spawnpoints.Count);
                if (CheckForDuplicate(selected, rand) == false)
                {
                    selected.Add(rand);
                    i++;
                }
            }
        }
        else if (GetWave() >= 9)
        {
            int i = 1;
            while (i <= 5)
            {
                int rand = Random.Range(0, spawnpoints.Count);
                if (CheckForDuplicate(selected, rand) == false)
                {
                    selected.Add(rand);
                    i++;
                }
            }
        }
        Debug.Log("Spawnpoints chosen.");
        Debug.Log(selected.Count);
        return selected;
    }

    /// <summary>
    /// Checks if integer "sel" already exists in given List. Returns true if so, returns false otherwise.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="sel"></param>
    /// <returns> Whether the given list contains integer "sel." </returns>
    public bool CheckForDuplicate(List<int> list, int sel)
    {
        if (list.Contains(sel))
        {
            Debug.Log("Checked.");
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Parses the Int List created by ChooseSpawnPoints() and creates Transform List of selected bridges.
    /// </summary>
    /// <returns> A Transform List of the selected bridges to spawn enemies at. </returns>
    public List<Transform> SelectedBridges()
    {
        List<Transform> parsed = new List<Transform>();
        for(int i = 0; i < ChooseSpawnpoints().Count; i++)
        {
            parsed.Add(spawnpoints[i]);
            Debug.Log("Bridges selected.");
        }
        
        return parsed;
    }

    /// <summary>
    /// Scales the number of enemies spawned based on wave number.
    /// </summary>
    /// <returns> An integer. </returns>
    public int SpawnPoolScale()
    {
        int scale = (int)Mathf.Round((GetWave() + 4) * 0.8f);
        Debug.Log("Spawn pool scale calculated.");
        return scale;
    }

    /// <summary>
    /// Calculates final amount of enemies to spawn, using SpawnPoolScale to scale amount based on wave number.
    /// </summary>
    /// <returns> An ArrayList of integers. </returns>
    public ArrayList SpawnPool()
    {
        ArrayList spawnPool = new ArrayList();

        for (int i = 0; i < SelectedBridges().Count; i++)
        {
            spawnPool.Add(SpawnPoolScale() + Random.Range(1, 2));
        }
        Debug.Log("Spawn pool calculated.");
        return spawnPool;
    }

    /// <summary>
    /// Checks how many of each enemy type is left. | 1 = Enemy1.
    /// </summary>
    /// <returns></returns>
    public int CheckEnemyNumber(int code)
    {
        int enemyType1Num;
        enemyType1Num = GameObject.FindGameObjectsWithTag("Enemy1").Length;
        if (code == 1)
        {
            return enemyType1Num;
        }
        else return 0;
    }

    /// <summary>
    /// Gets whether the wave is in progress or not.
    /// </summary>
    /// <returns></returns>
    public bool WaveInProgress()
    {
        return waveInProgress;
    }

    /// <summary>
    /// Changes the status of waveInProgress.
    /// </summary>
    /// <param name="status"></param>
    public void WaveInProgressChange(bool status)
    {
        waveInProgress = status;
    }
}
