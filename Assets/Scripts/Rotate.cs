using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    private RectTransform _rectTransform;

    void Start(){
        StartRotate();
    }

    void StartRotate(){
        obj.transform.DORotate(new Vector3(0,360,0), 1f);
    }
}
