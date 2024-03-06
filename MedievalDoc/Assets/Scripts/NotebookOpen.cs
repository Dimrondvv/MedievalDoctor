using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(OpenNotebook);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OpenNotebook(GameObject obj, PickupController pickc)
    {
        if (obj != gameObject)
            return;
        if(!UIManager.Instance.IsNotebookEnabled)
            UIManager.Instance.EnableNotebook();
        else
            UIManager.Instance.DisableNoteBook();
    }
}
