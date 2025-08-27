using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "UIVariables", menuName = "Scriptable Objects/UIVariables")]
public class UIVariables : ScriptableObject
{
    public float Timer = 0f;
    public int Score = 0;
    public int lives = 3;
    public float p1Health = 100f;
    public float p2Health = 100f;
    public float p1Special = 0f;
    public float p2Special = 0f;
    public void UpdateVars()
    {
        Score = GameManager.Instance.Score;
        Timer = (int)GameManager.Instance.Timer;
        lives = GameManager.Instance.Lives;
    }
}
