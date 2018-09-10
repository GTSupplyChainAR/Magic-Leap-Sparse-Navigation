using UnityEngine;
using PickAR.Util;

namespace PickAR.Controller {

    /// <summary>
    /// Takes keyboard and mouse input to move the player.
    /// </summary>
    public class PlayerController : Singleton<PlayerController> {

        /// <summary> The direction that the player is moving in. </summary>
        private Vector3 moveDirection;
        /// <summary> The maximum movement speed of the player. </summary>
        [SerializeField]
        [Tooltip("The maximum movement speed of the player.")]
        private float maxSpeed;
        /// <summary> The maximum acceleration of the player. </summary>
        [SerializeField]
        [Tooltip("The maximum acceleration of the player.")]
        private float acceleration;

        /// <summary> The maximum pitch of the camera. </summary>
        [SerializeField]
        [Tooltip("The maximum pitch of the camera.")]
        private float maxPitch;
        /// <summary> The turn speed of the camera. </summary>
        [SerializeField]
        [Tooltip("The turn speed of the camera.")]
        private float turnSpeed;

        /// <summary> The first-person camera attached to the player. </summary>
        private Camera playerCamera;

        /// <summary>
        /// Initializes the player controller.
        /// </summary>
        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerCamera = GetComponentInChildren<Camera>();
        }

        /// <summary>
        /// Updates the player every physics tick.
        /// </summary>
        private void FixedUpdate() {
            Move();
        }

        /// <summary>
        /// Moves the player around when keys are pressed.
        /// </summary>
        private void Move() {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            mouseMovement *= turnSpeed;
            transform.Rotate(-mouseMovement.y, mouseMovement.x, 0);
            Vector3 rotation = transform.eulerAngles;
            rotation.z = 0;
            if (rotation.x > 180) {
                rotation.x = Mathf.Max(rotation.x, 360 - maxPitch);
            } else {
                rotation.x = Mathf.Min(rotation.x, maxPitch);
            }
            transform.eulerAngles = rotation;

            Vector3 targetDirection = Vector3.zero;

            targetDirection += Vector3.right * Input.GetAxis("Horizontal");
            targetDirection += Vector3.forward * Input.GetAxis("Vertical");

            float speedScale = Mathf.Min(1, Vector3.Magnitude(targetDirection));

            targetDirection = RotateFacing(targetDirection);
            targetDirection.y = 0;
            targetDirection.Normalize();
            targetDirection *= speedScale;

            targetDirection *= maxSpeed;
            moveDirection = Vector3.MoveTowards(moveDirection, targetDirection, acceleration);

            transform.position += moveDirection;
        }

        /// <summary>
        /// Rotates a vector by the direction the player is facing.
        /// </summary>
        /// <returns>The rotated vector.</returns>
        /// <param name="vector">The vector to rotate.</param>
        private Vector3 RotateFacing(Vector3 vector) {
            return playerCamera.transform.rotation * vector;
        }
    }
}