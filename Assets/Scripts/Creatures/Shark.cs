using System.Collections.Generic;
using Misc;
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

    [SerializeField] float rightPunchDelayOffset;

    [HideInInspector] public AudioPlayer audioPlayer;

    public event SharkAnimationStarted animationStarted;
    
    new SkeletonAnimation animation;
    List<string> animationList;
    int currentIndex;

    void Awake() {
        animation = GetComponent<SkeletonAnimation>();
        animationList = new List<string> {
            Attack1, Attack2, Attack3, Dead,
        };
    }

    void Start() {
        animation.AnimationState.Start += entry => {
            animationStarted?.Invoke(entry.animation.name);
        };
        animation.AnimationState.SetAnimation(0, Walk, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        var nextAnimation = getNextAnimation();
        animation.AnimationState.SetAnimation(0, nextAnimation, false);
        animation.AnimationState.AddAnimation(0, Walk, true, 0);
        playSound(nextAnimation);
    }

    public string getCurrentAnimation() => animation.AnimationName;

    string getNextAnimation() {
        if (currentIndex == animationList.Count) {
            currentIndex = 0;
        }
        return animationList[currentIndex++];
    }

    void playSound(string animation) {
        switch (animation) {
            case Attack1:
                audioPlayer.play(AudioPlayer.AudioId.SharkPunch1);
                break;
            case Attack2:
                audioPlayer.play(AudioPlayer.AudioId.SharkPunch2);
                break;
            case Attack3:
                audioPlayer.play(AudioPlayer.AudioId.SharkPunch2);
                var delay = audioPlayer.getAudioLength(AudioPlayer.AudioId.SharkPunch2) + rightPunchDelayOffset;
                audioPlayer.play(AudioPlayer.AudioId.SharkPunch1, delay);
                break;
            case Dead:
                audioPlayer.play(AudioPlayer.AudioId.SharkFall);
                break;
        }
    }
}

public delegate void SharkAnimationStarted(string animationName);
}