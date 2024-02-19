using Config;
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

    void Awake() {
        var config = GameObject.FindWithTag("Config").GetComponent<GlobalConfig>();
        config.isFirstScene = false;
        shark.audioPlayer = audioPlayer;
    }

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