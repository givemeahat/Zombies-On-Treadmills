using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombieTemperament : MonoBehaviour
{
    public GameManager gm;

    public TextMeshProUGUI temperamentLabel;
    public Image temperamentLevel;

    public Color[] colorCode;

    public void Start()
    {
        float _rate = gm.randomness;
        if (_rate > 0)
            temperamentLevel.fillAmount = _rate * 2;
        else temperamentLevel.fillAmount = 0;

        if (_rate <= .1) temperamentLabel.text = "DOCILE";
        else if (_rate > .1 && _rate <= .15) temperamentLabel.text = "NUISANCE";
        else if (_rate > .15 && _rate <= .2) temperamentLabel.text = "ENERGETIC";
        else if (_rate > .2 && _rate <= .25) temperamentLabel.text = "ROWDY";
        else if (_rate > .25 && _rate <= .3) temperamentLabel.text = "WILY";
        else if (_rate > .3 && _rate <= .35) temperamentLabel.text = "DISRUPTIVE";
        else if (_rate > .35 && _rate <= .4) temperamentLabel.text = "AGGRESSIVE";
        else if (_rate > .4 && _rate <= .45) temperamentLabel.text = "DANGEROUS";
        else if (_rate > .45 && _rate <= .5) temperamentLabel.text = "CRITICAL";

        if (_rate <= .1) temperamentLabel.color = colorCode[0];
        else if (_rate > .1 && _rate <= .2) temperamentLabel.color = colorCode[1];
        else if (_rate > .2 && _rate <= .3) temperamentLabel.color = colorCode[2];
        else if (_rate > .3 && _rate <= .4) temperamentLabel.color = colorCode[3];
        else if (_rate > .4 && _rate <= .5) temperamentLabel.color = colorCode[4];
    }
}
