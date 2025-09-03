using UnityEngine;

public abstract class PlayerSpecialAttack : MonoBehaviour
{
	protected float specialMeter = 0;

	[SerializeField] protected float specialMeterMax = 10;

	private void Update()
	{
		specialMeter += Time.deltaTime;
		Mathf.Clamp(specialMeter, 0, specialMeterMax);
	}

	public abstract void SpecialAttack();

}
