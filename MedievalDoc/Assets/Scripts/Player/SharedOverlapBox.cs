using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedOverlapBox : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private Vector3 boxOffset;
    [SerializeField] private bool drawBox;
    private static Collider highestCollider;
    public static Collider HighestCollider { get { return highestCollider; } }

    private static Transform itemPoint;
    public static Transform ItemPoint { get { return itemPoint; } }

    private static bool isInteractable;
    public static bool IsInteractable { get { return isInteractable; } }


    void Start()
    {
        
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.4f)) + transform.position + boxOffset, boxSize, transform.rotation);
        if(hitColliders.Length > 0)
        {
            highestCollider = hitColliders[0];
            itemPoint = null;
            foreach (Collider collider in hitColliders)
            {
                if (collider.transform.position.y > highestCollider.transform.position.y && collider.gameObject != gameObject)
                    highestCollider = collider;

                if (collider.GetComponentInChildren<ItemLayDownPoint>() || collider.GetComponentInChildren<PatientLayDownPoint>() || collider.GetComponent<Crafting>() || collider.GetComponent<ItemChest>())
                    itemPoint = collider.transform;

                if (collider.gameObject.layer == 7 && isInteractable == false)
                {
                    isInteractable = true;
                }
                
            }
        }
        else
        {
            highestCollider = null;
            itemPoint = null;
            isInteractable = false;
        }

        if(drawBox)
            VisualiseBox.DisplayBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.4f)) + transform.position + boxOffset, boxSize, transform.rotation);
    }
}
