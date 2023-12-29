using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlueprint : MonoBehaviour
{
    [SerializeField] private string blueprintPrefabName;
    private GameObject blueprintPrefab;

    public void InstantiateBlueprint()
    {
        Instantiate(blueprintPrefab, )
    }

    void Start()
    {
        blueprintPrefab = Resources.Load<GameObject>("Resources/"+blueprintPrefabName);
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 blueprintPosition = transform.rotation * Vector3.forward;
        
    }
}
