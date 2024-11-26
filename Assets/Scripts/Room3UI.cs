using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room3UI : MonoBehaviour
{
    [SerializeField] private TMP_Text pillarsText;
    [SerializeField] private GameObject allPillarsCollected;

    private int _pillarsCollected = 0;
    
    public bool ShowUI()
    {
        gameObject.SetActive(true);
        return true;
    }

    public bool HideUI()
    {
        gameObject.SetActive(false);
        return true;
    }

    public void PillarCollected()
    {
        _pillarsCollected++;
        pillarsText.text = "Pillars Collected: " + _pillarsCollected + "/4";

        if (_pillarsCollected >= 4)
        {
            allPillarsCollected.SetActive(true);
        }
    }
}
