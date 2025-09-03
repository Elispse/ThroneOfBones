using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform[] sections;
    private int currentSection = 0;
    [SerializeField] WaveManager waveManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextSection()
    {
        if (currentSection < sections.Length - 1)
        {
            currentSection++;
        }
        transform.position = new Vector3(sections[currentSection].position.x, sections[currentSection].position.y, gameObject.transform.position.z);
        waveManager.StartCoroutine(waveManager.StartNewWave());
    }
}
