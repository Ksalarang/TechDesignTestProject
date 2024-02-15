using Creatures;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstScene {
public class SceneController : MonoBehaviour {
    [SerializeField] Pirate pirate;
    [SerializeField] TMP_Text pirateAnimationLabel;
    
    void Awake() {
        Application.targetFrameRate = 60;
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