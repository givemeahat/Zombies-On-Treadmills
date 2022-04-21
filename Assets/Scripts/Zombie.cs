using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameManager gm;
    public float speed = 5f;

    public Treadmill startTreadmill;
    public Treadmill currentTreadmill;

    public Transform nextWaypoint;

    public int currentDirection;

    public void Spawn()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        nextWaypoint = startTreadmill.waypoint;
        StartCoroutine(MoveToWaypoint());
        currentTreadmill = startTreadmill;
    }

    public void NextTreadmill()
    {
        currentDirection = currentTreadmill.currentDirection;
        startTreadmill = currentTreadmill;
        //south
        if (currentDirection == 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.up);
            if (hit.collider != null)
            {
                DetermineType(hit);
            }
            else
                Debug.Log("AH");
                return;
        }
        //east
        if (currentDirection == 90)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.right);
            if (hit.collider != null)
            {
                DetermineType(hit);
            }
            else Debug.Log("AH");
            return;
        }        
        //north
        if (currentDirection == 180)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, Vector2.up);
            if (hit.collider != null)
            {
                DetermineType(hit);
            }
            else Debug.Log("AH");
            return;
        }        
        //west
        if (currentDirection == 270)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTreadmill.waypoint.transform.position, -Vector2.right);
            if (hit.collider != null)
            {
                DetermineType(hit);
            }
            else Debug.Log("AH");
            return;
        }
    }

    public void DetermineType(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.tag == "Treadmill")
        {
            Debug.Log("hello " + hit.collider.name);
            currentTreadmill = hit.collider.gameObject.GetComponent<Treadmill>();
            nextWaypoint = currentTreadmill.waypoint;
            StartCoroutine(MoveToWaypoint());
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

    IEnumerator MoveToWaypoint()
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
