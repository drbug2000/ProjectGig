using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    public Gig thegig;

    void Start()
    {
        gunTransform = GameObject.Find("Gig").GetComponent<Transform>();

        settingscreensize();
    }

    void FixedUpdate()
    {
        // 카메라 범위 제한
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        settingscreensize();
        transform.position = Vector3.Lerp(transform.position, gunTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
    // 카메라 크기 변경
    private void settingscreensize() {
        if (thegig.isfire == true && Camera.main.orthographicSize < 10) {
            Camera.main.orthographicSize = Mathf.Lerp(7, 10, 0.5f);
        }
        else if (Camera.main.orthographicSize > 7) {
            Camera.main.orthographicSize = Mathf.Lerp(10, 7, 0.5f);
        }
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
