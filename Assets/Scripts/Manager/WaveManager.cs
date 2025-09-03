using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private int waveNumber = 0;
    [SerializeField] public GameObject Wave1;
    [SerializeField] public GameObject Wave2;
    [SerializeField] public GameObject Wave3;
    [SerializeField] public GameObject Wave4;
    [SerializeField] public GameObject Wave5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wave1.SetActive(false);
        Wave2.SetActive(false);
        Wave3.SetActive(false);
        Wave4.SetActive(false);
        Wave5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNumber > 0)
        {
            GameObject currentWave = null;
            switch (waveNumber)
            {
                case 1:
                    currentWave = Wave1;                
                    break;
                case 2:
                    currentWave = Wave2;
                    break;
                case 3:
                    currentWave = Wave3;
                    break;
                case 4:
                    currentWave = Wave4;
                    break;
                case 5:
                    currentWave = Wave5;
                    break;
                default:
                    break;
            }
            if (currentWave != null)
            {
                bool allDead = true;
                foreach (Transform enemy in currentWave.transform)
                {
                    if (enemy.gameObject.activeInHierarchy)
                    {
                        allDead = false;
                        break;
                    }
                }
                if (allDead)
                {
                    waveNumber++;
                    StartCoroutine(StartNewWave());
                }
            }
        }
    }

    public IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(0.5f);

        nextWave();
    }
    public void nextWave()
    {
        waveNumber++;
        switch (waveNumber)
        {
            case 1:
                Wave1.SetActive(true);
                foreach (Transform enemy in Wave1.transform)
                {
                    var enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.enabled = true;
                    }
                }
                break;
            case 2:
                Wave2.SetActive(true);
                foreach (Transform enemy in Wave2.transform)
                {
                    var enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.enabled = true;
                    }
                }
                break;
            case 3:
                Wave3.SetActive(true);
                foreach (Transform enemy in Wave3.transform)
                {
                    var enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.enabled = true;
                    }
                }
                break;
            case 4:
                Wave4.SetActive(true);
                foreach (Transform enemy in Wave4.transform)
                {
                    var enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.enabled = true;
                    }
                }
                break;
            case 5:
                Wave5.SetActive(true);
                foreach (Transform enemy in Wave5.transform)
                {
                    var enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        enemyScript.enabled = true;
                    }
                }
                break;
            default:
                break;
        }
    }
}
