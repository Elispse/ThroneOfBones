using FMOD.Studio;
using UnityEngine;

public class KnightStep : MonoBehaviour
{
    private EventInstance stepSound;
    public void Step()
    {
        stepSound = AudioManager.instance.CreateInstance(FMODEvents.instance.knightWalk);
        stepSound.start();
    }
}
