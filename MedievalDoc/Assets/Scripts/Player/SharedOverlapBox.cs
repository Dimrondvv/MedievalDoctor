using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedOverlapBox : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private Vector3 boxOffset;
    [SerializeField] private bool drawBox;
    private static Collider highestCollider;
    private GameObject playerObject;
    public static Collider HighestCollider { get { return highestCollider; } }

    private static Transform itemPoint;
    public static Transform ItemPoint { get { return itemPoint; } }

    private static bool isInteractable;
    public static bool IsInteractable { get { return isInteractable; } }


    void Start()
    {
        playerObject = App.Instance.GameplayCore.PlayerManager.playerObject;
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.4f)) + transform.position + boxOffset, boxSize, transform.rotation);
        if(hitColliders.Length > 0)
        {
            foreach (var col in hitColliders)
            {
                if (col.gameObject.GetComponent<ECM2.Character>())
                {
                    return;
                }
            }
            highestCollider = GetBestCollider(hitColliders);

            itemPoint = null;
            if (highestCollider.GetComponentInChildren<ItemLayDownPoint>() || highestCollider.GetComponentInChildren<PatientLayDownPoint>() || highestCollider.GetComponent<Crafting>() || highestCollider.GetComponent<ItemChest>())
                itemPoint = highestCollider.transform;

            if (highestCollider.gameObject.layer == 7 && isInteractable == false)
            {
                isInteractable = true;
            }
        }
        else
        {
            highestCollider = null;
            itemPoint = null;
            isInteractable = false;
        }
        if (drawBox)
            VisualiseBox.DisplayBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.4f)) + transform.position + boxOffset, boxSize, transform.rotation);
    }

    private Collider GetBestCollider(Collider[] hits)
    {
        Collider closestCollider = hits[0];


        List<Collider> higherCollider = new List<Collider>();

        foreach (Collider collider in hits)
        {
            if (collider.transform.position.y >= closestCollider.transform.position.y)
            {
                closestCollider = collider;
            }
        }
        foreach (Collider collider in hits) //Loop to determine colliders with higher y points
        {
            if (collider.transform.position.y > closestCollider.transform.position.y)
            {
                higherCollider.Add(collider);
            }
        }

        foreach(Collider collider in higherCollider)
        {
            float dist1 = Vector3.Distance(closestCollider.transform.position, playerObject.transform.position);
            float dist2 = Vector3.Distance(collider.transform.position, playerObject.transform.position);
            if (dist2 < dist1)
            {
                closestCollider = collider;
            }
        }

        return closestCollider;
    }
}
