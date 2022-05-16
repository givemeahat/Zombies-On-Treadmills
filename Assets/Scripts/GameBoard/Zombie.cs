using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameManager gm;
    public float speed;
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
        nextWaypoint = startTreadmill.waypoint;
        StartCoroutine(MoveToWaypoint(fallSpeed));
        currentTreadmill = startTreadmill;
        zombieRend = GetComponent<SpriteRenderer>();
    }

    public void NextTreadmill()
    {
        float devianceRoll = Random.Range(.01f, 1);
        //Debug.Log("rolled " + devianceRoll);
        if (devianceRoll >= randomness)
        {
            currentDirection = currentTreadmill.currentDirection;
            isCompliant = true;
        }
        else
        {
            int dirRoll = Random.Range(0, 3);
            if (dirRoll == 0)
            {
                currentDirection = 0;
            }
            if (dirRoll == 1)
            {
                currentDirection = 90;
            }
            if (dirRoll == 2)
            {
                currentDirection = 180;
            }
            if (dirRoll == 3)
            {
                currentDirection = 270;
            }
            isCompliant = false;
        }
        startTreadmill = currentTreadmill;
        CheckDirections();
    }

    public void CheckDirections()
    {
        int layerMask = 1 << 7;
        layerMask = ~layerMask;
        //south
        if (currentDirection == 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.up, 1f, layerMask);
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
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.right, 1f, layerMask);
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
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.up, 1f, layerMask);
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
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.right, 5f, layerMask);
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
            gm.UpdateZombiesKilled();
        }
    }
}
