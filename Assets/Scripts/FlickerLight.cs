using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    [SerializeField] private GameObject lampCase;
    [SerializeField] private Material lampOnMat;
    [SerializeField] private Material lampOffMat;
    [SerializeField] private float minWaitTimeBetweenFlicks = 0.2f;
    [SerializeField] private float maxWaitTimeBetweenFlicks = 0.8f;
    [SerializeField] private float minWaitTimeAfterFlicks = 1f;
    [SerializeField] private float maxWaitTimeAfterFlicks = 2f;

    private bool _isFlicking = false;
    private readonly List<Task> _flicksCoroutines = new List<Task>();
    private MeshRenderer _lampCaseMeshRenderer;

    private void Awake()
    {
        _lampCaseMeshRenderer = lampCase.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (_isFlicking)
        {
            CheckIfFlicksCompleted();
        }
        else
        {
            StartFlicks();
        }
    }

    private IEnumerator FlickLight(GameObject lightGo)
    {
        ToggleLight(lightGo, false);
        yield return new WaitForSeconds(Random.Range(minWaitTimeBetweenFlicks, maxWaitTimeBetweenFlicks));
        ToggleLight(lightGo, true);
        yield return new WaitForSeconds(Random.Range(minWaitTimeBetweenFlicks, maxWaitTimeBetweenFlicks));
        ToggleLight(lightGo, false);
        yield return new WaitForSeconds(Random.Range(minWaitTimeBetweenFlicks, maxWaitTimeBetweenFlicks));
        ToggleLight(lightGo, true);
    }

    private IEnumerator WaitTimeAfterFlick()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTimeAfterFlicks, maxWaitTimeAfterFlicks));
        _isFlicking = false;
    }

    private void StartFlicks()
    {
        _isFlicking = true;
        foreach (GameObject lightGo in lights)
        {
            _flicksCoroutines.Add(new Task(FlickLight(lightGo)));
        }
    }

    private void CheckIfFlicksCompleted()
    {
        bool flicksCompleted = true;
        
        foreach (Task flickCoroutine in _flicksCoroutines)
        {
            if (flickCoroutine.Running)
            {
                flicksCompleted = false;
            }
        }

        if (flicksCompleted)
        {
            StartCoroutine(WaitTimeAfterFlick());
        }
    }

    private void ToggleLight(GameObject lightGo ,bool toggle)
    {
        lightGo.SetActive(toggle);
        _lampCaseMeshRenderer.material = toggle ? lampOnMat : lampOffMat;
    }
}
