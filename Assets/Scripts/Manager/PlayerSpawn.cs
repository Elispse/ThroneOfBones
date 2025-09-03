using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        GameObject p1 = null;
        GameObject p2 = null;
        Debug.Log("spawning player 1");
		switch (GameManager.Instance.selectedCharacter1)
		{
			case "KnightPrefab":
				p1 = Instantiate(GameManager.Instance.Characters[0]);
                GameManager.Instance.Player1 = p1.GetComponent<IPlayer>();
                break;
			case "MagePrefab":
				p1 = Instantiate(GameManager.Instance.Characters[1]);
                GameManager.Instance.Player1 = p1.GetComponent<IPlayer>();
                break;
            default:
                break;
		}
        Debug.Log("spawning player 2");
        if (GameManager.Instance.selectedCharacter2 != null)
        {
            switch (GameManager.Instance.selectedCharacter2)
            {
                case "KnightPrefab":
                    p2 = Instantiate(GameManager.Instance.Characters[0]);
                    GameManager.Instance.Player2 = p2.GetComponent<IPlayer>();
                    break;
                case "MagePrefab":
                    p2 = Instantiate(GameManager.Instance.Characters[1]);
                    GameManager.Instance.Player2 = p2.GetComponent<IPlayer>();
                    break;
                default:
                    break;
            }
        }
    }
}
