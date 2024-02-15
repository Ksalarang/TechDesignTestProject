using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Creatures {
public class Pirate : MonoBehaviour, IPointerClickHandler {
    const string Idle = "Idle";
    const string Attack1 = "Attack_1";
    const string Attack2 = "Attack_2";

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
    }

    public string getCurrentAnimation() => animation.AnimationName;
}

public delegate void PirateAnimationStarted(string animationName);
}