using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; set; }
    public int Lives { get; set; }
    public Scene Level { get; set; }
    public float Timer { get; set; }

    [SerializeField] public UIVariables uiVariables;

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
        Level = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
        Score = 0;
        Lives = 3;
        Timer = 0f;
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
        Lives--;
        if (Lives <= 0)
        {   
            Score = 0;
            Lives = 3;
            Timer = 0f;
            Level = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            SceneManager.LoadScene(Level.name);
            Debug.Log("dead");
            GoToLevel(0);
        }
        else
        {
            Score = (int)(Score * 0.75f);
            GoToLevel(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("respawn, lives: " + Lives);
        }
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
}
