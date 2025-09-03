using FMOD.Studio;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int selectedLevel { get; set; }
    public string selectedCharacter1 { get; set; } = null;
    public string selectedCharacter2 { get; set; } = null;
    [SerializeField] public GameObject[] Characters;
    public int Score { get; set; }
    public Scene Level { get; set; }
    public float Timer { get; set; }
    public IPlayer Player1 { get; set; }
    public IPlayer Player2 { get; set; }

    [SerializeField] public PlayerInputManager playerInputManager;

    [SerializeField] public UIVariables uiVariables;

    private EventInstance pauseSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Level = SceneManager.GetActiveScene();
        Score = 0;
        Timer = 0f;
        pauseSFX = AudioManager.instance.CreateInstance(FMODEvents.instance.UIPause);
    }

    void Update()
    {
        Timer += Time.deltaTime;
        uiVariables.UpdateVars();
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void Death()
    {
        Destroy(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToLevel(int level)
    {
        if (level < 0 || level >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid level index: " + level);
            return;
        }
        string sceneName = SceneUtility.GetScenePathByBuildIndex(level);
        sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        pauseSFX.start();
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
