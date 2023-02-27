using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dumpster : MonoBehaviour
{
    public Volcano volcanoScript;

    public float zombieCount;
    public float maxCapacity;

    public Image fullnessBar;
    public Color gaugeColor;

    public Sprite fullDumpsterSprite;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gaugeColor = fullnessBar.color;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) CheckCount();
    }
    public void CheckCount()
    {
        zombieCount = zombieCount + 1;
        if (zombieCount == maxCapacity)
        {
            this.gameObject.tag = "Full";
            fullnessBar.fillAmount = 1;
            fullnessBar.color = Color.red;
            StartCoroutine(LockDumpster());
            return;
        }
        float fillAmt = zombieCount / maxCapacity;
        StartCoroutine(FillGauge(fillAmt));
    }
    IEnumerator LockDumpster()
    {
        yield return new WaitForSeconds(.5f);
        this.GetComponent<SpriteRenderer>().sprite = fullDumpsterSprite;
        fullnessBar.gameObject.SetActive(false);
    }
    IEnumerator FillGauge(float fillAmt)
    {
        float _currentFill = fullnessBar.fillAmount;
        float _time = 0f;
        float _duration = .1f;
        while (_time < _duration)
        {
            _time += Time.deltaTime;
            fullnessBar.fillAmount = Mathf.Lerp(_currentFill, fillAmt, (_time / _duration));
            if (fullnessBar.fillAmount <= .25f) fullnessBar.color = Color.green;
            else if (fullnessBar.fillAmount > .25f && fullnessBar.fillAmount <= .5f) fullnessBar.color = Color.yellow;
            else if (fullnessBar.fillAmount > .5f && fullnessBar.fillAmount <= .75f) fullnessBar.color = new Color (1, 0.6470588f, 0, 1);
            else if (fullnessBar.fillAmount > .75f && fullnessBar.fillAmount <= 1f) fullnessBar.color = Color.red;
            yield return null;
        }
        fullnessBar.fillAmount = fillAmt;
    }
}
