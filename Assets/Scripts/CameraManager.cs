using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } = null;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ResetCamera(int x, int y)
    {
        mainCamera.transform.position = new Vector3((float)(x / 2), mainCamera.transform.position.y, -(7-y)/3);
    }
}
