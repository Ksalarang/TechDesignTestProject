using System;
using System.Collections;
using System.Collections.Generic;
using Misc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

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

    void Awake() {
        animation = GetComponent<SkeletonAnimation>();
        animationList = new List<string> {
            Dead, Attack1, Attack2, Attack3
        };
    }

    void Start() {
        animation.AnimationState.Start += entry => {
            animationStarted?.Invoke(entry.animation.name);
        };
        animation.AnimationState.SetAnimation(0, Walk, true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        var nextAnimation = animationList[Random.Range(0, animationList.Count)];
        animation.AnimationState.SetAnimation(0, nextAnimation, false);
        animation.AnimationState.AddAnimation(0, Walk, true, 0);
        playSound(nextAnimation);
    }

    public string getCurrentAnimation() => animation.AnimationName;

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
                StartCoroutine(delayAction(delay, () => {
                    audioPlayer.play(AudioPlayer.AudioId.SharkPunch1);
                }));
                break;
        }
    }

    IEnumerator delayAction(float delay, Action action) {
        var time = 0f;
        while (time < delay) {
            time += Time.deltaTime;
            yield return null;
        }
        action.Invoke();
    }
}

public delegate void SharkAnimationStarted(string animationName);
}