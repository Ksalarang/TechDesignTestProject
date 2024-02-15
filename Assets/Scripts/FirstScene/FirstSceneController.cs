using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirstScene {
public class FirstSceneController : MonoBehaviour {
    public void loadNextScene() {
        SceneManager.LoadScene(1);
    }
}
}