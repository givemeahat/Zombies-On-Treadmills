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

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
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
            return;
        }
        float fillAmt = zombieCount / maxCapacity;
        StartCoroutine(FillGauge(fillAmt));
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
            yield return null;
        }
        fullnessBar.fillAmount = fillAmt;
    }
}
