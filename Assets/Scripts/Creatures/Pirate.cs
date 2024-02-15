using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Creatures {
public class Pirate : MonoBehaviour, IPointerClickHandler {
    static readonly string Idle = "Idle";
    static readonly string Attack1 = "Attack_1";
    static readonly string Attack2 = "Attack_2";
    new SkeletonAnimation animation;

    void Awake() {
        animation = GetComponent<SkeletonAnimation>();
    }

    void Start() {
        animation.AnimationState.SetAnimation(0, Idle, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        var animationName = Random.value < 0.5f ? Attack1 : Attack2;
        animation.AnimationState.SetAnimation(0, animationName, false);
        animation.AnimationState.AddAnimation(0, Idle, true, 0);
    }
}
}