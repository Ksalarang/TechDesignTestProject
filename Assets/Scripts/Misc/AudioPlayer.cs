using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc {
public class AudioPlayer : MonoBehaviour {
    [Header("Scene 1")]
    [SerializeField] AudioClip pirateAttack1;
    [SerializeField] AudioClip pirateAttack2;
    [SerializeField] AudioClip shipBackground;
    [Header("Scene 2")]
    [SerializeField] AudioClip sharkPunch1;
    [SerializeField] AudioClip sharkPunch2;
    [SerializeField] AudioClip sharkFall;
    [SerializeField] AudioClip beachBackground;

    public bool isFirstScene;
    
    AudioSource audioSource;
    List<AudioClip> audioClips;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioClips = new List<AudioClip> {
            sharkPunch1, sharkPunch2, sharkFall,
            pirateAttack1, pirateAttack2,
            shipBackground, beachBackground,
        };
    }

    void Start() {
        play(isFirstScene ? AudioId.ShipBackground : AudioId.BeachBackground);
    }

    public void play(AudioId id, float delay = 0f, Action action = null) {
        StartCoroutine(delayAction(delay, () => {
            switch (id) {
                case AudioId.SharkPunch1:
                    audioSource.PlayOneShot(sharkPunch1);
                    break;
                case AudioId.SharkPunch2:
                    audioSource.PlayOneShot(sharkPunch2);
                    break;
                case AudioId.SharkFall:
                    audioSource.PlayOneShot(sharkFall);
                    break;
                case AudioId.PirateAttack1:
                    audioSource.PlayOneShot(pirateAttack1);
                    break;
                case AudioId.PirateAttack2:
                    audioSource.PlayOneShot(pirateAttack2);
                    break;
                case AudioId.ShipBackground:
                    audioSource.PlayOneShot(shipBackground);
                    break;
                case AudioId.BeachBackground:
                    audioSource.PlayOneShot(beachBackground);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
            action?.Invoke();
        }));
    }

    public float getAudioLength(AudioId id) {
        return audioClips.Find(c => isMatch(c, id)).length;
    }

    bool isMatch(AudioClip clip, AudioId id) => clip == getAudioClip(id);

    AudioClip getAudioClip(AudioId id) {
        return id switch {
            AudioId.SharkPunch1 => sharkPunch1,
            AudioId.SharkPunch2 => sharkPunch2,
            AudioId.PirateAttack1 => pirateAttack1,
            AudioId.PirateAttack2 => pirateAttack2,
            AudioId.BeachBackground => beachBackground,
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