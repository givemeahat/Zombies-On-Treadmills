using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieSpawner spawner;
    public GameManager gm;
    float speed = .65f;
    public float fallSpeed;
    public Transform startPosition;

    public Treadmill startTreadmill;
    public Treadmill currentTreadmill;
    public Transform nextWaypoint;

    public int currentDirection;
    SpriteRenderer zombieRend;

    public float randomness;
    bool isCompliant;

    public void Spawn()
    {
        this.GetComponent<Animator>().Play("Zombie_Fall In");
        nextWaypoint = startTreadmill.waypoint;
        StartCoroutine(MoveToWaypoint(fallSpeed));
        currentTreadmill = startTreadmill;
        zombieRend = GetComponent<SpriteRenderer>();
    }

    public void NextTreadmill()
    {
        if (gameObject.name == "Zombie_Sasha(Clone)")
        {
            if (!GetComponent<Animator>().GetBool("IsWalking")) GetComponent<Animator>().SetBool("IsWalking", true);
        }
        float devianceRoll = Random.Range(.01f, 1);
        if (devianceRoll >= randomness)
        {
            currentDirection = currentTreadmill.currentDirection;
            isCompliant = true;
        }
        else
        {
            int dirRoll = Random.Range(0, 3);
            currentDirection = dirRoll * 90;
            isCompliant = false;
        }
        startTreadmill = currentTreadmill;
        CheckDirections();
    }

    public void CheckDirections()
    {
        currentTreadmill.rotation.SetActive(false);
        int layerMask = 1 << 7;
        layerMask = ~layerMask;
        //south
        if (currentDirection == 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.up, .5f, layerMask);
            if (hit.collider != null)
            {
                DetermineType(hit);
                return;
            }
            else if (isCompliant)
            {
                currentDirection += 90;
            }
            else currentDirection += 90;

        }
        //east
        if (currentDirection == 90)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.right, .5f, layerMask);
            zombieRend.flipX = false;
            if (hit.collider != null)
            {
                DetermineType(hit);
                return;
            }
            else if (isCompliant)
            {
                currentDirection += 90;
            }
            else currentDirection += 90;
        }
        //north
        if (currentDirection == 180)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.up, .5f, layerMask);
            if (hit.collider != null)
            {
                DetermineType(hit);
                return;
            }
            else if (isCompliant)
            {
                currentDirection += 90;
            }
            else currentDirection += 90;
        }
        //west
        if (currentDirection == 270)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.right, .5f, layerMask);
            zombieRend.flipX = true;
            if (hit.collider != null)
            {
                DetermineType(hit);
                return;
            }
            currentDirection = 0;
            CheckDirections();
        }
    }

    public void DetermineType(RaycastHit2D hit)
    {
        if (gameObject.name == "Zombie_Sasha(Clone)")
        {
            switch (currentDirection)
            {
                case 0:
                    GetComponent<Animator>().SetBool("IsVertical", true);
                    GetComponent<Animator>().SetBool("IsFacingAway", false);
                    break;
                case 90:
                    GetComponent<Animator>().SetBool("IsVertical", false);
                    GetComponent<Animator>().SetBool("IsFacingAway", false);
                    break;
                case 180:
                    GetComponent<Animator>().SetBool("IsVertical", true);
                    GetComponent<Animator>().SetBool("IsFacingAway", true);
                    break;
                case 270:
                    GetComponent<Animator>().SetBool("IsVertical", false);
                    GetComponent<Animator>().SetBool("IsFacingAway", false);
                    break;
            }
        }

        currentTreadmill.rotation.SetActive(true);
        if (hit.collider.gameObject.tag == "Rotation")
        {
            currentTreadmill = hit.collider.gameObject.GetComponent<Rotate>().tm;
            nextWaypoint = currentTreadmill.waypoint;
            StartCoroutine(MoveToWaypoint(speed));
        }
        if (hit.collider.gameObject.tag == "Treadmill")
        {
            currentTreadmill = hit.collider.gameObject.GetComponent<Treadmill>();
            nextWaypoint = currentTreadmill.waypoint;
            StartCoroutine(MoveToWaypoint(speed));
        }
        if (hit.collider.gameObject.tag == "House")
        {
            House house = hit.collider.gameObject.GetComponent<House>();
            nextWaypoint = house.waypoint;
            StartCoroutine(MoveToFinalWaypoint(house.gameObject));
        }
        if (hit.collider.gameObject.tag == "Volcano")
        {
            Volcano volcano = hit.collider.gameObject.GetComponent<Volcano>();
            currentTreadmill = null;
            nextWaypoint = volcano.waypoint;
            StartCoroutine(MoveToFinalWaypoint(volcano.gameObject));
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "House" || coll.gameObject.tag == "Volcano")
        {
            StartCoroutine(DestroyZombie());
        }
    }

    IEnumerator DestroyZombie()
    {
        yield return new WaitForSeconds(speed + .1f);
        gm.zombiesInScene.Remove(this.gameObject);
        gm.CheckZombies();
        Destroy(this.gameObject);
    }

    IEnumerator MoveToWaypoint(float moveSpeed)
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = nextWaypoint.position;
        float time = 0;
        while (time < moveSpeed)
        {
            time += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(startPos, endPos, time / moveSpeed);
            this.transform.position = pos;
            yield return null;
        }
        if (!GetComponent<BoxCollider2D>().enabled)
        {
            yield return new WaitForSeconds(.5f);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        NextTreadmill();
    }
    IEnumerator MoveToFinalWaypoint(GameObject finalGO)
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = nextWaypoint.position;
        float time = 0;
       
        while (time < speed)
        {
            time += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(startPos, endPos, time / speed);
            this.transform.position = pos;
            yield return null;
        }
        if (finalGO.tag == "House")
        {
            gm.UpdatePeopleKilled();
        }
        if (finalGO.tag == "Volcano")
        {
            //finalGO.GetComponent<Volcano>().smokeFX.GetComponent<Animator>().SetTrigger("ZombieDeath");
            gm.UpdateZombiesKilled();
        }
    }
}
