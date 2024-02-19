using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Misc {
public class AudioPlayer : MonoBehaviour {
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource creatureSource;
    
    GlobalConfig globalConfig;
    AudioConfig config;
    List<AudioClip> audioClips;

    void Awake() {
        globalConfig = GameObject.FindWithTag("Config").GetComponent<GlobalConfig>();
        config = globalConfig.audio;
        audioClips = new List<AudioClip> {
            config.sharkPunch1, config.sharkPunch2, config.sharkFall,
            config.pirateAttack1, config.pirateAttack2,
            config.shipBackground, config.beachBackground,
        };
    }

    void Start() {
        backgroundSource.clip = globalConfig.isFirstScene ? config.shipBackground : config.beachBackground;
        backgroundSource.Play();
    }

    public void play(AudioId id, float delay = 0f, Action action = null) {
        StopAllCoroutines();
        StartCoroutine(delayAction(delay, () => {
            creatureSource.clip = getAudioClip(id);
            creatureSource.Play();
            action?.Invoke();
        }));
    }

    public float getAudioLength(AudioId id) {
        return audioClips.Find(c => c == getAudioClip(id)).length;
    }

    AudioClip getAudioClip(AudioId id) {
        return id switch {
            AudioId.SharkPunch1 => config.sharkPunch1,
            AudioId.SharkPunch2 => config.sharkPunch2,
            AudioId.SharkFall => config.sharkFall,
            AudioId.PirateAttack1 => config.pirateAttack1,
            AudioId.PirateAttack2 => config.pirateAttack2,
            AudioId.ShipBackground => config.shipBackground,
            AudioId.BeachBackground => config.beachBackground,
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
    
    IEnumerator delayAction(float delay, Action action) {
        var time = 0f;
        while (time < delay) {
            time += Time.deltaTime;
            yield return null;
        }
        action.Invoke();
    }

    public enum AudioId {
        SharkPunch1,
        SharkPunch2,
        SharkFall,
        PirateAttack1,
        PirateAttack2,
        ShipBackground,
        BeachBackground,
    }
}
}