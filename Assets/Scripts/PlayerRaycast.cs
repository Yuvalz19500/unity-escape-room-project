using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float rayLength = 50f;
    [SerializeField] private Vector3 rayHeightOffset = new Vector3(0f, 1f, 0);
    [SerializeField] private Transform cameraTransform;
    
    public bool ShootRaycast(out RaycastHit hit, LayerMask layersToCheck, bool debug = false)
    {
        Ray ray = new Ray(cameraTransform.position + rayHeightOffset, cameraTransform.forward * rayLength);

        if (debug)
        {
            Debug.DrawRay(cameraTransform.position + rayHeightOffset, cameraTransform.forward * rayLength, Color.blue, 1f);
        }
        return Physics.Raycast(ray, out hit, rayLength, layersToCheck);
    }
}
