using UnityEngine;

public class LevelTransition : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player")){
			GameManager.Instance.GoToLevel(3);
		}
		
	}
}
