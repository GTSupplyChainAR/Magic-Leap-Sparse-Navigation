#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace PickAR.Navigation {

    /// <summary>
    /// Used to create the editor GUI for waypoints.
    /// </summary>
    [CustomEditor(typeof(Waypoint))]
    class WaypointEditor : Editor {

        /// <summary> The distance ahead of the current waypoint that a new waypoint will be placed at. </summary>
        private const float NEW_WAYPOINT_DISTANCE = 1;

        /// <summary> The name of the action to add a waypoint. </summary>
        private const string ADD_ACTION = "Add Waypoint";

        /// <summary> The name of the action to delete a waypoint. </summary>
        private const string DELETE_ACTION = "Delete Waypoint";

        /// <summary> The name of the action to create a waypoint connection. </summary>
        private const string CREATE_CONNECTION_ACTION = "Create Waypoint Connection";

        /// <summary> The name of the action to delete a waypoint connection. </summary>
        private const string DELETE_CONNECTION_ACTION = "Delete Waypoint Connection";

        /// <summary> The waypoint to use in drag-in boxes. </summary>
        private Waypoint waypointToAttach;

        /// <summary> The array of waypoints connected to this waypoint. </summary>
        private SerializedProperty waypointArray;

        /// <summary> The waypoint that is being examined. </summary>
        private Waypoint targetWaypoint {
            get { return (Waypoint) target; }
        }

        /// <summary>
        /// Finds properties on the object to show in the editor GUI.
        /// </summary>
        private void OnEnable() {
            waypointArray = serializedObject.FindProperty("adjacent");
        }

        /// <summary>
        /// Draws the component editor for waypoints.
        /// </summary>
        public override void OnInspectorGUI() {
            serializedObject.Update();
            if (GUILayout.Button(ADD_ACTION)) {
                Undo.RecordObjects(new Object[] { targetWaypoint }, ADD_ACTION);
                Waypoint newWaypoint = Instantiate(targetWaypoint, targetWaypoint.transform.position + targetWaypoint.transform.forward * NEW_WAYPOINT_DISTANCE, Quaternion.identity);
                newWaypoint.adjacent = new List<Waypoint>();
                newWaypoint.name = targetWaypoint.name;
                newWaypoint.transform.parent = targetWaypoint.transform.parent;
                Undo.RegisterCreatedObjectUndo(newWaypoint.gameObject, ADD_ACTION);
                targetWaypoint.AddConnectingWaypoint(newWaypoint);
                Selection.activeGameObject = newWaypoint.gameObject;
            }

            if (GUILayout.Button(DELETE_ACTION)) {
                Undo.RecordObjects(targetWaypoint.adjacent.ToArray(), DELETE_ACTION);

                if (targetWaypoint.adjacent.Count > 0) {
                    Selection.activeGameObject = targetWaypoint.adjacent[targetWaypoint.adjacent.Count - 1].gameObject;
                }

                foreach (Waypoint connectedWaypoint in targetWaypoint.adjacent) {
                    connectedWaypoint.DeleteConnectingWaypoint(targetWaypoint);
                }

                Undo.DestroyObjectImmediate(targetWaypoint.gameObject);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(CREATE_CONNECTION_ACTION);
            waypointToAttach = EditorGUILayout.ObjectField(waypointToAttach, typeof(Waypoint), true) as Waypoint;
            EditorGUILayout.Space();
            if (waypointToAttach != null) {
                if (waypointToAttach == targetWaypoint) {
                    Debug.LogWarning("Cannot connect a waypoint to itself.");
                } else {
                    Undo.RecordObjects(new Object[] { waypointToAttach, targetWaypoint }, CREATE_CONNECTION_ACTION);
                    waypointToAttach.AddConnectingWaypoint(targetWaypoint);
                }
                waypointToAttach = null;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(DELETE_CONNECTION_ACTION);
            waypointToAttach = EditorGUILayout.ObjectField(waypointToAttach, typeof(Waypoint), true) as Waypoint;
            EditorGUILayout.Space();
            if (waypointToAttach != null) {
                Undo.RecordObjects(new Object[] { waypointToAttach, targetWaypoint }, DELETE_CONNECTION_ACTION);
                waypointToAttach.DeleteConnectingWaypoint(targetWaypoint);
                waypointToAttach = null;
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(waypointArray, true);
            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

#endif