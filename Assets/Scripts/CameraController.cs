using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform playerpos;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;
    [SerializeField]
    private Camera cam;
    // [SerializeField]
    private float targetposition = 10f;
    private float lastZoomSpeed = 1f;
    private float smoothTime = 3f;

    void Start()
    {
        // Camera cam = GetComponent<Camera>();
        // cam.orthographicSize = 5f;
    
        gunTransform = GameObject.Find("Gig").GetComponent<Transform>();
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
        MoveCameraSize();
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, gunTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);
        float clampX = Mathf.Clamp(transform.position.x, -45 + center.x, 45 + center.x);
        float clampY = Mathf.Clamp(transform.position.y, -40 + center.y, 40 + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void MoveCameraSize() {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetposition, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
