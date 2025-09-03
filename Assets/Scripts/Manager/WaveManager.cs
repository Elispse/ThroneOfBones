using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private int waveNumber = 0;
    [SerializeField] public Wave Wave1;
    [SerializeField] public Wave Wave2;
    [SerializeField] public Wave Wave3;
    [SerializeField] public Wave Wave4;
    [SerializeField] public Wave Wave5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNumber > 0)
        {
            Wave currentWave = null;
            Wave nextWave = null;
            switch (waveNumber)
            {
                case 1:
                    currentWave = Wave1;
                    nextWave = Wave2;
                    break;
                case 2:
                    currentWave = Wave2;
                    nextWave = Wave3;
                    break;
                case 3:
                    currentWave = Wave3;
                    nextWave = Wave4;
                    break;
                case 4:
                    currentWave = Wave4;
                    nextWave = Wave5;
                    break;
                case 5:
                    currentWave = Wave5;
                    break;
                default:
                    break;
            }
            if (currentWave != null)
            {
                bool allDead = currentWave.allDead();
                if (allDead && nextWave.isFollowupWave)
                {
                    waveNumber++;
                    StartCoroutine(StartNewWave());
                }
            }
        }
    }

    public IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(0.25f);

        nextWave();
    }
    public void nextWave()
    {
        waveNumber++;
        switch (waveNumber)
        {
            case 1:
                Wave1.Begin();
                break;
            case 2:
                Wave2.Begin();
                break;
            case 3:
                Wave3.Begin();
                break;
            case 4:
                Wave4.Begin();
                break;
            case 5:
                Wave5.Begin();
                break;
            default:
                break;
        }
    }
}
