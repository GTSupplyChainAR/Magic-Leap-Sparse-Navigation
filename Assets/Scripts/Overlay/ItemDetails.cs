using UnityEngine;
using UnityEngine.UI;
using PickAR.Items;
using PickAR.Util;

namespace PickAR.Overlay {

    /// <summary>
    /// The overlay displaying an item's location and description.
    /// </summary>
    public class ItemDetails : Singleton<ItemDetails> {

        /// <summary> The overlay to hide when displaying item details. </summary>
        [SerializeField]
        [Tooltip("The overlay to hide when displaying item details.")]
        private GameObject isometricOverlay;

        /// <summary> The text displaying the item description. </summary>
        [SerializeField]
        [Tooltip("The text displaying the item description.")]
        private Text descriptionText;

        /// <summary> The text displaying the shelf column. </summary>
        [SerializeField]
        [Tooltip("The text displaying the shelf column.")]
        private Text columnText;

        /// <summary> Shelf images to highlight the location of the item. </summary>
        [SerializeField]
        [Tooltip("Shelf images to highlight the location of the item.")]
        private Image[] shelves;

        /// <summary> The color of shelves that the item is not on. </summary>
        [SerializeField]
        [Tooltip("The color of shelves that the item is not on.")]
        private Color inactiveColor;

        /// <summary> The color of the shelf that the item is on. </summary>
        [SerializeField]
        [Tooltip("The color of the shelf that the item is on.")]
        private Color activeColor;

        /// <summary> The time to display the item details for before disappearing. </summary>
        [Tooltip("The time to display the item details for before disappearing.")]
        public int displayTime;

        /// <summary>
        /// Disables the overlay at the start.
        /// </summary>
        private void Start() {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows the item details overlay.
        /// </summary>
        /// <param name="item">The item to display details for.</param>
        public void DisplayDetails(Item item) {
            isometricOverlay.SetActive(false);
            gameObject.SetActive(true);
            descriptionText.text = item.description;
            columnText.text = item.shelfColumn.ToString();
            for (int i = 0; i < shelves.Length; i++) {
                shelves[i].color = i == item.shelfRow ? activeColor : inactiveColor;
            }
        }

        /// <summary>
        /// Hides the item details overlay.
        /// </summary>
        public void HideDetails() {
            isometricOverlay.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}