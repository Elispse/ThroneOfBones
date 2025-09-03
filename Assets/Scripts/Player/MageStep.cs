using FMOD.Studio;
using UnityEngine;

public class MageStep : MonoBehaviour
{
    private EventInstance stepSound;
    public void Step()
    {
        stepSound = AudioManager.instance.CreateInstance(FMODEvents.instance.wizardWalk);
        stepSound.start();
    }
}
