using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] specialZombiePrefabs;
    public GameObject[] basicZombiePrefabs;
    public List<Zombie> zombiesSpawned;
    public int numberOfZombies;

    int currentDirection;

    public float timeBetweenZombies = .2f;
    float randomness;

    public Treadmill startingTreadmill;

    public Animator anim;

    public void Awake()
    {
        startingTreadmill = gameObject.GetComponentInParent<Treadmill>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        randomness = gm.randomness;
        currentDirection = this.GetComponentInParent<Treadmill>().currentDirection;
        if (currentDirection == 0 || currentDirection == 180)
        {
            this.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localPosition = new Vector3(0, 0.06f, 0);
        }
        if (!gm.gameObject.GetComponent<SpawnManager>().isFrenzy)
        {
            anim.Play("Target_Set");
        }
    }

    private void Update()
    {
        if (this.GetComponentInParent<Treadmill>().currentDirection != currentDirection)
        {
            currentDirection = this.GetComponentInParent<Treadmill>().currentDirection;
            if (currentDirection == 0 || currentDirection == 180)
            {
                this.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                this.transform.localPosition = new Vector3(0, 0.06f, 0);
            }
        }
    }

    public void BeginSpawn()
    {
        Debug.Log("Spawning");
        for (var x = 0; x < numberOfZombies; x++)
        {
            GameObject zombie;
            int roll = Random.Range(0, 100);
            if (roll < 97)
            {
                zombie = Instantiate(basicZombiePrefabs[Random.Range(0, basicZombiePrefabs.Length)], new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
            }
            else
            {
                zombie = Instantiate(specialZombiePrefabs[Random.Range(0, specialZombiePrefabs.Length)], new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
            }
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
