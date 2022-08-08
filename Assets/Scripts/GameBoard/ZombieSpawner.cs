using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] zombiePrefabs;
    public List<Zombie> zombiesSpawned;
    public int numberOfZombies;

    public float timeBetweenZombies = .2f;
    float randomness;

    public Treadmill startingTreadmill;

    public Animator anim;

    public void Awake()
    {
        startingTreadmill = gameObject.GetComponentInParent<Treadmill>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        randomness = gm.randomness;
        if (!gm.gameObject.GetComponent<SpawnManager>().isFrenzy)
        {
            anim.Play("Target_Set");
        }
    }

    public void BeginSpawn()
    {
        Debug.Log("Spawning");
        for (var x = 0; x < numberOfZombies; x++)
        {
            GameObject zombie = Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], new Vector3(this.transform.position.x, this.transform.position.y+10, this.transform.position.z), Quaternion.identity);
            zombie.GetComponent<Zombie>().gm = gm;
            zombie.GetComponent<Zombie>().spawner = this;
            zombie.GetComponent<Zombie>().randomness = randomness;
            //zombie.name = "Zombie " + x;
            zombie.GetComponent<Zombie>().startTreadmill = startingTreadmill;
            zombiesSpawned.Add(zombie.GetComponent<Zombie>());
            gm.zombiesInScene.Add(zombie);
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
