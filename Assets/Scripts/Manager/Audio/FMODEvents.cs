using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Knight SFX")]
    [field: SerializeField] public EventReference knightSwordSwing { get; private set; }
    [field: SerializeField] public EventReference knightWalk { get; private set; }
    [field: SerializeField] public EventReference knightBlock { get; private set; }

    [field: Header("Wizard SFX")]
    [field: SerializeField] public EventReference wizardPrimary { get; private set; }
    [field: SerializeField] public EventReference wizardWalk { get; private set; }
    [field: SerializeField] public EventReference wizardSecondary { get; private set; }

    [field: Header("Menu SFX")]
    [field: SerializeField] public EventReference UIClick {  get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference primaryTrack { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more that one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
