using System;
using Creatures;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SecondScene {
public class SceneController : MonoBehaviour {
    [SerializeField] Shark shark;
    [SerializeField] TMP_Text sharkAnimationLabel;

    [SerializeField] AudioPlayer audioPlayer;

    void Start() {
        shark.animationStarted += animationName => {
            sharkAnimationLabel.text = animationName;
        };
        sharkAnimationLabel.text = shark.getCurrentAnimation();

        shark.audioPlayer = audioPlayer;
    }

    public void loadPreviousScene() {
        SceneManager.LoadScene(0);
    }
}
}