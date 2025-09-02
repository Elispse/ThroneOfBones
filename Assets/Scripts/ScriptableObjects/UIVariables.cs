using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "UIVariables", menuName = "Scriptable Objects/UIVariables")]
public class UIVariables : ScriptableObject
{
    public float Timer = 0f;
    public int Score = 0;
    public int lives = 3;
    public int p1Health = 100;
    public int p2Health = 100;
    public string p1Combo = "0 combo";
    public string p2Combo = "0 combo";
    public void UpdateVars()
    {
        Score = GameManager.Instance.Score;
        Timer = (int)GameManager.Instance.Timer;
        lives = GameManager.Instance.Lives;
        p1Health = GameManager.Instance.Player1.Health;
        p2Health = GameManager.Instance.Player2.Health;
        p1Combo = GameManager.Instance.Player1.Combo + " combo";
        p2Combo = GameManager.Instance.Player2.Combo + " combo";
    }
}
