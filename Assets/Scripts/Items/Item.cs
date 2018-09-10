using UnityEngine;

namespace PickAR.Items {

    /// <summary>
    /// An item that needs to be picked.
    /// </summary>
    public class Item : MonoBehaviour {

        /// <summary> The color of items in the editor. </summary>
        private static Color gizmoColor = Color.cyan;

        /// <summary> The radius of the gizmo displaying the item. </summary>
        private const float GIZMO_RADIUS = 0.3f;

        /// <summary> The description to display for the item in the overlay. </summary>
        [Tooltip("The description to display for the item in the overlay.")]
        [TextArea(2, 2)]
        public string description;

        /// <summary> The column of the shelf that the item is on. </summary>
        [Tooltip("The column of the shelf that the item is on.")]
        public int shelfColumn;

        /// <summary> The row of the shelf that the item is on. </summary>
        [Tooltip("The row of the shelf that the item is on.")]
        public int shelfRow;

        /// <summary> Whether the item is the current item being picked in the job. </summary>
        internal bool isCurrent;

        /// <summary>
        /// Draws the waypoint in the editor.
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, GIZMO_RADIUS);
        }

        /// <summary>
        /// Acknowledges that the item will be picked.
        /// </summary>
        /// <param name="other">The user's collider.</param>
        private void OnTriggerEnter(Collider other) {
            if (isCurrent) {
                JobTracker.Instance.PickItem(this);
                isCurrent = false;
            }
        }
    }
}