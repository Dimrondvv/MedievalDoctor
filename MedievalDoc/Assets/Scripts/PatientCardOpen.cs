using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientCardOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(OpenCard);
    }

    private void OpenCard(GameObject obj, PickupController pick)
    {
        if (obj != gameObject)
            return;

        if (!UIManager.Instance.IsNotebookEnabled)
        {
            UIManager.Instance.EnableNotebook(2, true);
        }
        else
        {
            UIManager.Instance.DisableNoteBook();
        }


    }
}
