using UnityEngine;

namespace PickAR.Overlay {

    /// <summary>
    /// Causes the object to maintain a fixed offset to another object's yaw.
    /// </summary>
    class FollowRotation : MonoBehaviour {

        /// <summary> The object to follow. </summary>
        [SerializeField]
        [Tooltip("The object to follow.")]
        private GameObject target;

        /// <summary> The offset to maintain from the target object. </summary>
        private Vector3 offset;

        /// <summary>
        /// Registers the initial offset between the objects.
        /// </summary>
        private void Start() {
            offset = transform.eulerAngles - target.transform.eulerAngles;
        }

        /// <summary>
        /// Rotates the object according to the offset from the target.
        /// </summary>
        private void Update() {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y = target.transform.eulerAngles.y + offset.y;
            transform.eulerAngles = newRotation;
        }
    }
}
