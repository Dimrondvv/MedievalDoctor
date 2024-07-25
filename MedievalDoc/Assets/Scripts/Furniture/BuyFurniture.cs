using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyFurniture : MonoBehaviour, IInteractable
{
    [SerializeField] int price;
    [SerializeField] List<Behaviour> componentsToEnable;
    [SerializeField] bool isInteractable;
    [SerializeField] GameObject boughtModel;
    [SerializeField] GameObject notBoughtModel;
    [SerializeField] private float interactionTime;
    [SerializeField] private TextMeshPro priceText;
    bool textflag = false;
    public float InteractionTime { get { return interactionTime;} set { } }

    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(Buy);    
    }
    public void Buy(GameObject obj, PickupController player)
    {
        if (obj != gameObject)
            return;

        foreach(var cmp in componentsToEnable)
        {
            cmp.enabled = true;
        }
        if (isInteractable)
            gameObject.layer = 7;
        App.Instance.GameplayCore.PlayerManager.Money -= price;
        boughtModel.SetActive(true);
        notBoughtModel.SetActive(false);
        Destroy(priceText.gameObject);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Interactor.InteractableCollider.gameObject == gameObject && !textflag)
        {
            priceText.gameObject.SetActive(true);
            priceText.text = price.ToString() + "$";
            textflag = true;
        }
        else if(Interactor.InteractableCollider.gameObject != gameObject && textflag)
        {
            priceText.gameObject.SetActive(false);
            textflag = false;
        }
    }
}
