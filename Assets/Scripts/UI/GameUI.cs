using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;

    [Header("Knife Count Display")]
    [SerializeField]
    private GameObject knivesPanel;
    [SerializeField]
    private GameObject knifeIcon;
    [SerializeField]
    private Color usedKnifeIconColor;

    private ushort knifeIconIndexToChange = 0;

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    // this function displays all available knives for throwing
    public void ShowInitialDisplayedKnifeCount(int knivesNumber)
    {
        for (int i = 0; i < knivesNumber; i++)
            Instantiate(knifeIcon, knivesPanel.transform);
    }

    public void DecrementDisplayedKnifeCount()
    {
        // i should read about GetChild() function. how it works?
        knivesPanel.transform.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
        /* TOTRY:
         * knivesPanel.transform.GetChild(knifeIconIndexToChange).GetComponent<Image>().color = usedKnifeIconColor;
         * knifeIconIndexToChange++;
         */
    }
}
