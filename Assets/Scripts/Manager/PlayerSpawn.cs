using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
		switch (GameManager.Instance.selectedCharacter1)
		{
			case "KnightPrefab":
				var p1k = Instantiate(GameManager.Instance.Characters[0]);
                GameManager.Instance.Player1 = p1k.GetComponent<IPlayer>();
                break;
			case "MagePrefab":
				var p1m = Instantiate(GameManager.Instance.Characters[1]);
                GameManager.Instance.Player1 = p1m.GetComponent<IPlayer>();
                break;
		}
        GameManager.Instance.playerInputManager.JoinPlayer(0);
        if (GameManager.Instance.selectedCharacter2 != null)
        {
            switch (GameManager.Instance.selectedCharacter2)
            {
                case "KnightPrefab":
                    var p2k = Instantiate(GameManager.Instance.Characters[0]);
                    GameManager.Instance.Player1 = p2k.GetComponent<IPlayer>();
                    break;
                case "MagePrefab":
                    var p2m = Instantiate(GameManager.Instance.Characters[1]);
                    GameManager.Instance.Player1 = p2m.GetComponent<IPlayer>();
                    break;
            }
            GameManager.Instance.playerInputManager.JoinPlayer(1);
        }
    }
}
