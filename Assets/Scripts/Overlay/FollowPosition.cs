using UnityEngine;

namespace PickAR.Overlay {

    /// <summary>
    /// Causes the object to maintain a fixed offset to another object's position in the xz plane.
    /// </summary>
    class FollowPosition : MonoBehaviour {

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
            offset = transform.position - target.transform.position;
        }

        /// <summary>
        /// Moves the object according to the offset from the target.
        /// </summary>
        private void Update() {
            Vector3 newPos = target.transform.position + offset;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
}
