using UnityEngine;

public class PlayerSpecialFighter : PlayerSpecialAttack
{
	public override void SpecialAttack()
	{
		if(specialMeter == specialMeterMax)
		{
			specialMeter = 0;
			
		}
	}
}
