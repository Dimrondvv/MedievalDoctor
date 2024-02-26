using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOutline : MonoBehaviour
{
    private GameObject overlapObject;
    // Update is called once per frame


    void Update()
    {
        if (SharedOverlapBox.HighestCollider == null)
        {
            if(overlapObject != null)
                Destroy(overlapObject.GetComponent<Outline>());
            return;
        }

        if (SharedOverlapBox.HighestCollider.gameObject.layer == 7 || SharedOverlapBox.HighestCollider.gameObject.layer == 10)
        {
            overlapObject = SharedOverlapBox.HighestCollider.gameObject;
            if (overlapObject.GetComponent<Outline>() == null)
            {
                var outline = SharedOverlapBox.HighestCollider.gameObject.AddComponent<Outline>();

                outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
                outline.OutlineColor = Color.white;
                outline.OutlineWidth = 5f;
            }
        }
        else if(overlapObject != SharedOverlapBox.HighestCollider.gameObject && overlapObject != null)
        {
            Destroy(overlapObject.GetComponent<Outline>());
        }
    }
}
