using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform playerTransform;
    public Vector3 FromPlaerToCamera;
    public float smoothing = 29f;
    void Start () {
        FromPlaerToCamera = transform.position - playerTransform.position;

    }
	
	void Update () {
        if (playerTransform)
        {
            Vector3 newCamPosition = playerTransform.position + FromPlaerToCamera;
            transform.position = Vector3.Lerp(transform.position, newCamPosition, smoothing * Time.deltaTime);
        }
        

    }
}
