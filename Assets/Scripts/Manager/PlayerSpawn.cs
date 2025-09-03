using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
		switch (GameManager.Instance.selectedCharacter1)
		{
			case "KnightPrefab":
				Instantiate(GameManager.Instance.Characters[0]);
				break;
			case "MagePrefab":
				Instantiate(GameManager.Instance.Characters[1]);
				break;
		}
	}
}
