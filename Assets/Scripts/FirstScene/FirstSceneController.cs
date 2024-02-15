using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstScene {
public class FirstSceneController : MonoBehaviour {
    void Awake() {
        Application.targetFrameRate = 60;
    }

    public void loadNextScene() {
        SceneManager.LoadScene(1);
    }
}
}