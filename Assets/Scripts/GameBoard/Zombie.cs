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
    public Material zombieMat;

    public void Spawn()
    {
        Renderer rend = this.GetComponent<SpriteRenderer>();
        rend.material = new Material(zombieMat);

        this.GetComponent<Animator>().Play("Zombie_Fall In");
        nextWaypoint = startTreadmill.waypoint;
        StartCoroutine(MoveToWaypoint(fallSpeed));
        currentTreadmill = startTreadmill;
        zombieRend = GetComponent<SpriteRenderer>();
    }

    public void NextTreadmill()
    {
        if (!GetComponent<Animator>().GetBool("IsWalking")) GetComponent<Animator>().SetBool("IsWalking", true);
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

        currentTreadmill.rotation.SetActive(true);
        if (hit.collider.gameObject.tag == "Rotation")
        {
            //if (!hit.collider.gameObject.GetComponent<Treadmill>().isFrozen) currentTreadmill = hit.collider.gameObject.GetComponent<Rotate>().tm;
            currentTreadmill = hit.collider.gameObject.GetComponentInParent<Treadmill>();
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
            if (currentDirection == 0 || currentDirection == 180) StartCoroutine(BeginScaleZombieVertical());
            else StartCoroutine(BeginScaleZombieHorizontal());
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Volcano")
        {
            if (coll.gameObject.tag == "Volcano") coll.gameObject.GetComponent<Volcano>().Burn();
            //if (coll.gameObject.tag == "House") coll.gameObject.GetComponent<House>().Flash();
        }
    }
    IEnumerator BeginScaleZombieVertical()
    {
        float time = 0f;
        float waitTime = .5f;

        float startBurnPerc = 0f;
        float endBurnPerc = 0.019f;
        float startDisPerc = 0f;
        float endDisPerc = 1f;

        yield return new WaitForSeconds(.5f);
        zombieRend.material.SetFloat("_SourceAlphaDissolveNoiseFactor", -0.65f);
        //StartCoroutine(DestroyZombie());
        while (time < waitTime)
        {
            time += Time.deltaTime;
            float burnPerc = Mathf.Lerp(startBurnPerc, endBurnPerc, time / (waitTime / 1.5f));
            float disPerc = Mathf.Lerp(startDisPerc, endDisPerc, time / (waitTime));

            zombieRend.material.SetFloat("_BurnFade", burnPerc);
            zombieRend.material.SetFloat("_SourceAlphaDissolveFade", disPerc);


            yield return null;
        }
        gm.zombiesInScene.Remove(this.gameObject);
        gm.CheckZombies();
        Destroy(this.gameObject);
    }

    IEnumerator BeginScaleZombieHorizontal()
    {
        float yVal = this.transform.localPosition.y;
        float jumpHeight = yVal + .25f;
        float startScale = 1.25f;
        float endScale = 0f;
        float time = 0f;
        float waitTime = .5f;
        while (time < waitTime)
        {
            time += Time.deltaTime;

            float newYVal = Mathf.Lerp(yVal, jumpHeight, time / waitTime);
            this.transform.localPosition = new Vector3(this.transform.position.x, newYVal, this.transform.position.z);

            yield return null;
        }
        time = 0f;
        waitTime = .5f;

        float startBurnPerc = 0f;
        float endBurnPerc = 0.019f;
        float startDisPerc = 0f;
        float endDisPerc = 1f;
        zombieRend.material.SetFloat("_SourceAlphaDissolveNoiseFactor", -0.65f);
        //StartCoroutine(DestroyZombie());
        while (time < waitTime)
        {
            time += Time.deltaTime;

            float newYVal = Mathf.Lerp(jumpHeight, yVal, time / waitTime);
            //float newScale = Mathf.Lerp(1.25f, .0f, time / (waitTime/2));
            float burnPerc = Mathf.Lerp(startBurnPerc, endBurnPerc, time / (waitTime / 1.5f));
            float disPerc = Mathf.Lerp(startDisPerc, endDisPerc, time / (waitTime));

            this.transform.localPosition = new Vector3(this.transform.position.x, newYVal, this.transform.position.z);
            //this.transform.localScale = new Vector3(newScale, newScale, newScale);
            zombieRend.material.SetFloat("_BurnFade", burnPerc);
            zombieRend.material.SetFloat("_SourceAlphaDissolveFade", disPerc);


            yield return null;
        }
        gm.zombiesInScene.Remove(this.gameObject);
        gm.CheckZombies();
        Destroy(this.gameObject);
    }
    IEnumerator DestroyZombie()
    {
        float startScale = 1.25f;
        float endScale = 0f;
        float time = 0f;
        float waitTime = .5f;
        while (time < waitTime)
        {
            time += Time.deltaTime;

            float newScale = Mathf.Lerp(startScale, endScale, time / waitTime);
            this.transform.localScale = new Vector3(newScale, newScale, newScale);
            yield return null;
        }
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
            StartCoroutine(DestroyZombie());
            gm.UpdatePeopleKilled();
        }
        if (finalGO.tag == "Volcano")
        {
            //finalGO.GetComponent<Volcano>().smokeFX.GetComponent<Animator>().SetTrigger("ZombieDeath");
            gm.UpdateZombiesKilled();
        }
    }
}
