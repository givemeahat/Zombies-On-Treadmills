using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : MonoBehaviour
{
    public GameManager gm;
    public bool isFrenzy;
    public List<ZombieSpawner> zombieSpawners;

    //Random Drop Settings
    public int numberOfSpawns = 0;
    public int zombiesToSpawn = 0;
    public GameObject spawnerPrefab;
    public List<GameObject> treadmills;

    private void Start()
    {
        gm = this.GetComponent<GameManager>();
        if (!isFrenzy)
        {
            foreach (ZombieSpawner spawner in zombieSpawners)
            {
                spawner.numberOfZombies = zombiesToSpawn;
            }
        }
    }

    public void Spawn()
    {
        foreach(GameObject tm in treadmills)
        {
            tm.SendMessage("PlayAnimation");
        }
        if (isFrenzy)
        {
            for (var x = 0; x < numberOfSpawns; x++)
            {
                int treadmillPick = Random.Range(0, treadmills.Count);
                GameObject go = Instantiate(spawnerPrefab, treadmills[treadmillPick].transform, false);
                go.transform.position = treadmills[treadmillPick].transform.position;
                ZombieSpawner goSpawner = go.GetComponent<ZombieSpawner>();
                goSpawner.numberOfZombies = zombiesToSpawn;
                goSpawner.gm = gm;
                goSpawner.startingTreadmill = treadmills[treadmillPick].GetComponent<Treadmill>();
                treadmills.Remove(treadmills[treadmillPick]);
                zombieSpawners.Add(goSpawner);
                Debug.Log("Spawning Drop Points");
            }
            foreach (ZombieSpawner spawner in zombieSpawners)
            {
                spawner.BeginSpawn();
            }
        }
        else
        {
            foreach (ZombieSpawner spawner in zombieSpawners)
            {
                spawner.BeginSpawn();
            }
        }
    }
}
