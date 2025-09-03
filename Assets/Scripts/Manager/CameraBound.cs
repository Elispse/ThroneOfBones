using UnityEngine;

public class CameraBound : MonoBehaviour
{
    [SerializeField] CameraManager cameraManager;
    void Start()
    {
        if (cameraManager == null)
        {
            cameraManager = Camera.main.GetComponent<CameraManager>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraManager.NextSection();
            Destroy(gameObject);
        }
    }
}
