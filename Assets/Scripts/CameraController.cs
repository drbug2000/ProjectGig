using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 playerpos;
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

    private float distancex;
    private float distancey;
    private float distanceplayerandgig;

    void Start()
    {
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
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void MoveCameraSize() {
        distancex = gunTransform.position.x - playerpos.x;
        distancey = gunTransform.position.y - playerpos.y;
        distancex = Mathf.Pow(distancex, 2);
        distancey = Mathf.Pow(distancey, 2);
        distanceplayerandgig = distancex + distancey;
        distanceplayerandgig = Mathf.Pow(distanceplayerandgig, 1/2);
        this.camera.Size = distanceplayerandgig;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
