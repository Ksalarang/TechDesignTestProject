using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc {
public class AudioPlayer : MonoBehaviour {
    public AudioClip sharkPunch1;
    public AudioClip sharkPunch2;

    AudioSource audioSource;
    List<AudioClip> audioClips;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioClips = new List<AudioClip> {
            sharkPunch1, sharkPunch2,
        };
    }

    public void play(AudioId id) {
        switch (id) {
            case AudioId.SharkPunch1:
                audioSource.PlayOneShot(sharkPunch1);
                break;
            case AudioId.SharkPunch2:
                audioSource.PlayOneShot(sharkPunch2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }
    }

    public float getAudioLength(AudioId id) {
        return audioClips.Find(c => isMatch(c, id)).length;
    }

    bool isMatch(AudioClip clip, AudioId id) => clip == getAudioClip(id);

    AudioClip getAudioClip(AudioId id) {
        return id switch {
            AudioId.SharkPunch1 => sharkPunch1,
            AudioId.SharkPunch2 => sharkPunch2,
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }

    public enum AudioId {
        SharkPunch1,
        SharkPunch2,
    }
}
}