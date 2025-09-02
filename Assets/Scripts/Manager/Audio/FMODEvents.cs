using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Knight SFX")]
    [field: SerializeField] public EventReference knightSwordSwing { get; private set; }
    [field: SerializeField] public EventReference knightWalk { get; private set; }
    [field: SerializeField] public EventReference knightShield { get; private set; }
    [field: SerializeField] public EventReference knightBlock { get; private set; }
    [field: SerializeField] public EventReference knightHurt { get; private set; }

    [field: Header("Wizard SFX")]
    [field: SerializeField] public EventReference wizardPrimary { get; private set; }
    [field: SerializeField] public EventReference wizardWalk { get; private set; }
    [field: SerializeField] public EventReference wizardSecondary { get; private set; }
    [field: SerializeField] public EventReference wizardHurt { get; private set; }

    [field: Header("Skeleton Enemy SFX")]
    [field: SerializeField] public EventReference skeletonAttack { get; private set; }
    [field: SerializeField] public EventReference skeletonDeath { get; private set; }
    [field: SerializeField] public EventReference skeletonHit { get; private set; }

    [field: Header("Goblin Enemy SFX")]
    [field: SerializeField] public EventReference GoblinAttack { get; private set; }
    [field: SerializeField] public EventReference GoblinDeath { get; private set; }
    [field: SerializeField] public EventReference GoblinHit { get; private set; }

    [field: Header("Eyebat Enemy SFX")]
    [field: SerializeField] public EventReference EyebatAttack { get; private set; }
    [field: SerializeField] public EventReference EyebatDeath { get; private set; }
    [field: SerializeField] public EventReference EyebatHit { get; private set; }

    [field: Header("Menu SFX")]
    [field: SerializeField] public EventReference UIClick {  get; private set; }
    [field: SerializeField] public EventReference UIPlayButton {  get; private set; }
    [field: SerializeField] public EventReference UIPause {  get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference forestAmbience { get; private set; }
    [field: SerializeField] public EventReference castleAmbience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference forestMusic { get; private set; }
    [field: SerializeField] public EventReference castleMusic { get; private set; }
    [field: SerializeField] public EventReference menuMusic { get; private set; }
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
