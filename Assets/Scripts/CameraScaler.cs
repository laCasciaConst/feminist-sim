using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // 기준 비율
    private float initialSize;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        initialSize = cam.orthographicSize;

        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scale = windowAspect / targetAspect;

        if (scale < 1.0f)
        {
            // 화면이 더 세로로 긴 경우, 잘려도 상관 없이 비율 유지
            cam.orthographicSize = initialSize / scale;
        }
        else
        {
            cam.orthographicSize = initialSize;
        }

        cam.rect = new Rect(0, 0, 1, 1); // 카메라 비율 왜곡 방지
    }
}
