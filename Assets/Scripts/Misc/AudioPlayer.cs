using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Misc {
public class AudioPlayer : MonoBehaviour {
    [HideInInspector] public bool isFirstScene;

    AudioConfig config;
    AudioSource audioSource;
    List<AudioClip> audioClips;

    void Awake() {
        config = GameObject.FindWithTag("Config").GetComponent<GlobalConfig>().audio;
        audioSource = GetComponent<AudioSource>();
        audioClips = new List<AudioClip> {
            config.sharkPunch1, config.sharkPunch2, config.sharkFall,
            config.pirateAttack1, config.pirateAttack2,
            config.shipBackground, config.beachBackground,
        };
    }

    void Start() {
        audioSource.PlayOneShot(isFirstScene ? config.shipBackground : config.beachBackground);
    }

    public void play(AudioId id, float delay = 0f, Action action = null) {
        StopAllCoroutines();
        StartCoroutine(delayAction(delay, () => {
            audioSource.clip = getAudioClip(id);
            audioSource.Play();
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