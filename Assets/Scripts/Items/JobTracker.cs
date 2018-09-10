using UnityEngine;
using System.Collections;
using PickAR.Controller;
using PickAR.Navigation;
using PickAR.Overlay;
using PickAR.Util;

namespace PickAR.Items {

    /// <summary>
    /// Keeps track of the user's progress in a picking job.
    /// </summary>
    class JobTracker : Singleton<JobTracker> {

        /// <summary> The items to pick in the job. </summary>
        [SerializeField]
        [Tooltip("The items to pick in the job.")]
        private Item[] jobItems;

        /// <summary> The user's progress within the job. </summary>
        private int jobProgress;

        /// <summary> The current item being picked. </summary>
        private Item currentItem {
            get { return jobItems[jobProgress]; }
        }

        private bool jobCompleted {
            get { return jobProgress >= jobItems.Length; }
        }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Start() {
            currentItem.isCurrent = true;
        }

        /// <summary>
        /// Updates the path lines.
        /// </summary>
        private void Update() {
            if (jobCompleted) {
                Navigator.Instance.RemoveLines();
            } else {
                Navigator.Instance.DrawShortestPath(PlayerController.Instance.transform.position, currentItem.transform.position);
            }
        }

        /// <summary>
        /// Displays the item's details and picks it after a delay.
        /// </summary>
        /// <param name="item">The item to be picked.</param>
        internal void PickItem(Item item) {
            StartCoroutine(DisplayItemDetails(item));
        }

        /// <summary>
        /// Coroutine for displaying item details for a set amount of time.
        /// </summary>
        /// <param name="item">The item to be picked.</param>
        private IEnumerator DisplayItemDetails(Item item) {
            ItemDetails.Instance.DisplayDetails(item);

            yield return new WaitForSeconds(ItemDetails.Instance.displayTime);

            ItemDetails.Instance.HideDetails();

            jobProgress++;
            if (!jobCompleted) {
                currentItem.isCurrent = true;
            }
        }
    }
}