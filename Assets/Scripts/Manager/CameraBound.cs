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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            GetComponent<Collider2D>().isTrigger = false;
            used = true;
            var players = FindObjectsByType<IPlayer>(FindObjectsSortMode.None);
            foreach (var player in players)
            {
                player.transform.position = transform.position + new Vector3(2, 0, 0);
            }
            cameraManager.NextSection();
        }
    }
}
