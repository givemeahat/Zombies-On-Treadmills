using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLevelScreens : MonoBehaviour
{
    public GameObject neighborhoodScreen;
    public GameObject mallScreen;

    public GameObject currentScreen;

    public GameObject[] mallLevelButtons;

    public void Start()
    {
        DetectCurrentScreen();
    }
    private void OnEnable()
    {
        DetectCurrentScreen();
    }
    public void DetectCurrentScreen()
    {
        int _currentLevel = GetComponentInParent<DataManager>().currentLevel;
        if (_currentLevel <= 7)
        {
            neighborhoodScreen.GetComponent<Canvas>().sortingOrder = 1001;
            mallScreen.GetComponent<Canvas>().sortingOrder = 1000;
            foreach (GameObject button in mallLevelButtons)
            {
                button.GetComponent<Canvas>().sortingOrder = 1000;
            }
            currentScreen = neighborhoodScreen;
        }
        else if (_currentLevel > 7)
        {
            neighborhoodScreen.GetComponent<Canvas>().sortingOrder = 1000;
            mallScreen.GetComponent<Canvas>().sortingOrder = 1001;
            foreach (GameObject button in mallLevelButtons)
            {
                button.GetComponent<Canvas>().sortingOrder = 1020;
            }
            currentScreen = mallScreen;
        }
    }
    public void SwapScreens()
    {
        if (currentScreen == mallScreen) { StartCoroutine(SwitchScreenOut(neighborhoodScreen)); return; }
        else if (currentScreen == neighborhoodScreen) { StartCoroutine(SwitchScreenOut(mallScreen)); return; }
    }
    IEnumerator SwitchScreenOut(GameObject _nextScreen)
    {
        Debug.Log("Bonk");
        currentScreen.GetComponent<Animator>().SetTrigger("Swap");
        yield return new WaitForSeconds(.5f);
        if (_nextScreen == neighborhoodScreen)
        {
            foreach (GameObject button in mallLevelButtons)
            {
                button.GetComponent<Canvas>().sortingOrder = 1000;
            }
        }
        else
        {
            foreach (GameObject button in mallLevelButtons)
            {
                button.GetComponent<Canvas>().sortingOrder = 1020;
            }
        }
        _nextScreen.GetComponent<Canvas>().sortingOrder = 1001;
        currentScreen.GetComponent<Canvas>().sortingOrder = 1000;
        currentScreen = _nextScreen;
    }
}
