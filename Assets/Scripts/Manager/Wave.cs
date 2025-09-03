using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public bool isFollowupWave = false;
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }
    public void Begin()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    public bool allDead()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
