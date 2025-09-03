using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{

    private UIDocument uiDocument;
    private Button leveltestButton;
    private Button level1Button;
    private Button fivehundredwolvesButton;
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        leveltestButton = uiDocument.rootVisualElement.Q<Button>("btnlvltest");
        level1Button = uiDocument.rootVisualElement.Q<Button>("btnlvl1");
        fivehundredwolvesButton = uiDocument.rootVisualElement.Q<Button>("btnlvlwolf");

        leveltestButton.clicked += () => GoToLevel(2);
        level1Button.clicked += () => GoToLevel(3);
        fivehundredwolvesButton.clicked += () => GoToLevel(4);

    }
    public void GoToLevel(int index)
    {
        GameManager.Instance.selectedLevel = index;
        //Debug.Log("Loading level: " + name);
        GameManager.Instance.GoToLevel(1);
    }
}
