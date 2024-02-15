using System;
using Creatures;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SecondScene {
public class SceneController : MonoBehaviour {
    [SerializeField] Shark shark;
    [SerializeField] TMP_Text sharkAnimationLabel;

    void Start() {
        shark.animationStarted += animationName => {
            sharkAnimationLabel.text = animationName;
        };
        sharkAnimationLabel.text = shark.getCurrentAnimation();
    }

    public void loadPreviousScene() {
        SceneManager.LoadScene(0);
    }
}
}