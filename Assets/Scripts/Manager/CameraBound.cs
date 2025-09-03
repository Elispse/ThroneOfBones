using UnityEngine;

public class CameraBound : MonoBehaviour
{
    [SerializeField] CameraManager cameraManager;
    private bool used = false;
    void Start()
    {
        if (cameraManager == null)
        {
            cameraManager = Camera.main.GetComponent<CameraManager>();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Camera Bound Triggered");
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            cameraManager.NextSection();
            used = true;
        }
    }
}
