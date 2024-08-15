using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
namespace Items.Crafting
{

    public class ItemChanger : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private float interactionTime;
        public float InteractionTime { get => interactionTime; set => interactionTime = value; }
        private Data.ItemChanger changerData;

        // Start is called before the first frame update
        void Start()
        {
            changerData = HelperFunctions.ChangerLookup(gameObject.name);
            PickupController.OnInteract.AddListener(ItemChangerInteract);
        }

        private void ItemChangerInteract(GameObject interactionObject, PickupController player)
        {
            if (interactionObject != gameObject || player.PickedItem == null)
                return;

            if(player.PickedItem.name == changerData.itemReq)
            {
                Destroy(player.PickedItem.gameObject);
                player.PickedItem = null;
                var item = Instantiate(Resources.Load<GameObject>("Items/" + changerData.itemResult));
                player.SetPickedItem(item);
            }
        }
        
    }
}
