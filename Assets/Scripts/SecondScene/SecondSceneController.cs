using UnityEngine;
using UnityEngine.SceneManagement;

namespace SecondScene {
public class SecondSceneController : MonoBehaviour {
    public void loadPreviousScene() {
        SceneManager.LoadScene(0);
    }
}
}