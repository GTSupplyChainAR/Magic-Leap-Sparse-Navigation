using UnityEngine;
using System.Collections.Generic;

namespace PickAR.Navigation {

    /// <summary>
    /// A junction point in the graph of the area.
    /// </summary>
	public class Waypoint : MonoBehaviour {

        /// <summary> The color of waypoints in the editor. </summary>
        private static Color gizmoColor = Color.yellow;

        /// <summary> The radius of the gizmo displaying the waypoint. </summary>
        private const float GIZMO_RADIUS = 0.15f;

        /// <summary> The waypoints adjacent to this point. </summary>
        [Tooltip("The waypoints adjacent to this point.")]
        public List<Waypoint> adjacent = new List<Waypoint>();

        /// <summary> The unique index of the waypoint. </summary>
        [HideInInspector]
        public int index;

        /// <summary>
        /// Gets the distance between the waypoint and a position.
        /// </summary>
        /// <returns>The distance between the waypoint and the position.</returns>
        /// <param name="position">The position to get the distance from the waypoint.</param>
        public float GetDistance(Vector3 position) {
            return Vector3.Distance(position, transform.position);
        }

        /// <summary>
        /// Gets the distance between two waypoints.
        /// </summary>
        /// <returns>The distance between the two waypoints.</returns>
        /// <param name="waypoint1">The first waypoint to get a distance between.</param>
        /// <param name="waypoint2">The second waypoint to get a distance between.</param>
        public static float GetDistance(Waypoint waypoint1, Waypoint waypoint2) {
            return Vector3.Distance(waypoint1.transform.position, waypoint2.transform.position);
        }

        /// <summary>
        /// Sets the waypoint's material.
        /// </summary>
        /// <param name="material">The material to set the waypoint to.</param>
        internal void SetMaterial(Material material) {
            GetComponent<Renderer>().material = material;
        }

        /// <summary>
        /// Draws the waypoint in the editor.
        /// </summary>
        private void OnDrawGizmos() {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, GIZMO_RADIUS);
            if (adjacent != null) {
                foreach (Waypoint waypoint in adjacent) {
                    Gizmos.DrawLine(transform.position, waypoint.transform.position);
                }
            }
        }

        /// <summary>
        /// Adds a new waypoint to the list of connected waypoints.
        /// </summary>
        /// <param name="connectingWaypoint">The new connected waypoint to add.</param>
        public void AddConnectingWaypoint(Waypoint connectingWaypoint) {
            if (!adjacent.Contains(connectingWaypoint)) {
                adjacent.Add(connectingWaypoint);
                connectingWaypoint.adjacent.Add(this);
            }
        }

        /// <summary>
        /// Deletes a waypoint from the connected waypoint list.
        /// </summary>
        /// <param name="connectingWaypoint">The waypoint to delete.</param>
        public void DeleteConnectingWaypoint(Waypoint connectingWaypoint) {
            adjacent.Remove(connectingWaypoint);
            connectingWaypoint.adjacent.Remove(this);
        }
    }
}