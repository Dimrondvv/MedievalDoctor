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

    void Start()
    {
        
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.25f)) + transform.position + boxOffset, boxSize, transform.rotation);
        if(hitColliders.Length > 0)
        {
            highestCollider = hitColliders[0];

            foreach (Collider collider in hitColliders)
            {
                if (collider.transform.position.y > highestCollider.transform.position.y && collider.gameObject != gameObject)
                    highestCollider = collider;
            }
        }

        if(drawBox)
            VisualiseBox.DisplayBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.25f)) + transform.position + boxOffset, boxSize, transform.rotation);
    }
}
