using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Camera cam; 

    [SerializeField] Transform player;
    [SerializeField] float cameraStartFocus = 6f;
    //[SerializeField] float maxFocusOnPlayer = 3f;
    //[SerializeField] float minFocusOnPlayer = 8f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = cameraStartFocus;
    }

    void Update()
    {
        Vector3 distanceFromPlayer = new Vector3 (0, 0, -10);

        cam.transform.position = player.position + distanceFromPlayer;

        
    }
    
    private void ChangeCameraFocus(float changeToValue)
    {
        float speedOfChange = 1f;
        float currentCameraFocus = cam.orthographicSize;

        cam.orthographicSize = Mathf.Lerp(currentCameraFocus, changeToValue, speedOfChange * Time.deltaTime);

    }
}
