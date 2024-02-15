using Spine.Unity;
using UnityEngine;

namespace Creatures {
public class Crab : MonoBehaviour {
    [SerializeField] float moveSpeed;

    new Transform transform;
    new SkeletonAnimation animation;
    
    Vector3 initialPosition;
    Vector3 destination;
    float time;
    float totalTime;

    void Awake() {
        transform = base.transform;
        animation = GetComponent<SkeletonAnimation>();
        
        initialPosition = transform.localPosition;
        destination = initialPosition;
        destination.x = -destination.x;

        totalTime = Vector3.Distance(initialPosition, destination) / moveSpeed;
    }

    void Start() {
        animation.AnimationState.SetAnimation(0, "Walk", true);
    }

    void Update() {
        time += Time.deltaTime;
        if (time < totalTime) {
            transform.localPosition = Vector3.Lerp(initialPosition, destination, time / totalTime);
        } else {
            time = 0;
            transform.localPosition = destination;
            (initialPosition, destination) = (destination, initialPosition);
            flipX();
        }
    }

    void flipX() {
        var angles = transform.eulerAngles;
        angles.y = transform.localPosition.x < 0 ? 180 : 0;
        transform.eulerAngles = angles;
    }
}
}