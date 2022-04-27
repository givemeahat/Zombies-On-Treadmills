using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameManager gm;
    public GameObject zombiePrefab;
    public List<Zombie> zombiesSpawned;
    public int numberOfZombies;

    public float timeBetweenZombies = .2f;

    public Treadmill startingTreadmill;

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void BeginSpawn()
    {   
        for (var x = 0; x < numberOfZombies; x++)
        {
            GameObject zombie = Instantiate(zombiePrefab, this.transform.position, Quaternion.identity);
            zombie.GetComponent<Zombie>().gm = gm;
            zombie.name = "Zombie " + x;
            zombie.GetComponent<Zombie>().startTreadmill = startingTreadmill;
            zombiesSpawned.Add(zombie.GetComponent<Zombie>());
        }
        StartCoroutine(AwakenZombies());
    }

    IEnumerator AwakenZombies()
    {
        foreach (Zombie zombie in zombiesSpawned)
        {
            yield return new WaitForSeconds(timeBetweenZombies);
            zombie.Spawn();
        }
        zombiesSpawned.Clear();
    }
}
