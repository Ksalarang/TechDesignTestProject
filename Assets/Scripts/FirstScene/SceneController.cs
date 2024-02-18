using Creatures;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstScene {
public class SceneController : MonoBehaviour {
    [SerializeField] Pirate pirate;
    [SerializeField] TMP_Text pirateAnimationLabel;

    [SerializeField] AudioPlayer audioPlayer;
    
    void Awake() {
        Application.targetFrameRate = 60;
        
        audioPlayer.isFirstScene = true;
        pirate.audioPlayer = audioPlayer;
    }

    void Start() {
        pirate.animationStarted += animationName => {
            pirateAnimationLabel.text = animationName;
        };
        pirateAnimationLabel.text = pirate.getCurrentAnimation();
    }

    public void loadNextScene() {
        SceneManager.LoadScene(1);
    }
}
}