using System;
using UnityEngine;

namespace Config {
public class GlobalConfig : MonoBehaviour {
    public new AudioConfig audio;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}

[Serializable]
public class AudioConfig {
    public AudioClip pirateAttack1;
    public AudioClip pirateAttack2;
    public AudioClip shipBackground;
    public AudioClip sharkPunch1;
    public AudioClip sharkPunch2;
    public AudioClip sharkFall;
    public AudioClip beachBackground;
}
}