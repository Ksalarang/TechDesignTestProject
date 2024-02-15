using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Creatures {
public class Shark : MonoBehaviour, IPointerClickHandler {
    const string Walk = "Walk";
    const string Dead = "Dead";
    const string Attack1 = "Attack_1";
    const string Attack2 = "Attack_2";
    const string Attack3 = "Attack_3";
    
    new SkeletonAnimation animation;
    List<string> animationList;

    void Awake() {
        animation = GetComponent<SkeletonAnimation>();
        animationList = new List<string> {
            Dead, Attack1, Attack2, Attack3
        };
    }

    void Start() {
        animation.AnimationState.SetAnimation(0, Walk, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        var nextAnimation = animationList[Random.Range(0, animationList.Count)];
        animation.AnimationState.SetAnimation(0, nextAnimation, false);
        animation.AnimationState.AddAnimation(0, Walk, true, 0);
    }
}
}