using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private float timeout;

    private float _currentTime = 0f;

    void Update(){
        _currentTime += Time.deltaTime;
        float seconds = Mathf.FloorToInt(_currentTime % 60);
        if(seconds > timeout){
            wall.SetActive(false);
        }
    }
}
