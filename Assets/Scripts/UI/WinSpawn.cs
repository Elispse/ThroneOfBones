using UnityEngine;

public class WinSpawn : MonoBehaviour
{
	
	[SerializeField] public GameObject WinScreen;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameObject.Instantiate(WinScreen);
		}
	}
}
