using Misc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Creatures {
public class Pirate : MonoBehaviour, IPointerClickHandler {
    const string Idle = "Idle";
    const string Attack1 = "Attack_1";
    const string Attack2 = "Attack_2";

    [SerializeField] float attack1OffsetDivisor = 1f;
    [SerializeField] float attack2OffsetDivisor = 1f;

    [HideInInspector] public AudioPlayer audioPlayer;
    
    public event PirateAnimationStarted animationStarted;
    
    new SkeletonAnimation animation;

    void Awake() {
        animation = GetComponent<SkeletonAnimation>();
    }

    void Start() {
        animation.AnimationState.Start += entry => {
            animationStarted?.Invoke(entry.animation.name);
        };
        animation.AnimationState.SetAnimation(0, Idle, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        var animationName = Random.value < 0.5f ? Attack1 : Attack2;
        animation.AnimationState.SetAnimation(0, animationName, false);
        animation.AnimationState.AddAnimation(0, Idle, true, 0);
        playSound(animationName);
    }

    public string getCurrentAnimation() => animation.AnimationName;

    void playSound(string animation) {
        if (animation == Attack2) {
            var delay = audioPlayer.getAudioLength(AudioPlayer.AudioId.PirateAttack2) / attack2OffsetDivisor;
            audioPlayer.play(AudioPlayer.AudioId.PirateAttack2, delay);
        } else if (animation == Attack1) {
            var delay = audioPlayer.getAudioLength(AudioPlayer.AudioId.PirateAttack1) / attack1OffsetDivisor;
            audioPlayer.play(AudioPlayer.AudioId.PirateAttack1, delay);
        }
    }
}

public delegate void PirateAnimationStarted(string animationName);
}