using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    // Start is called before the first frame update
    void Start()
    {
        Grid.SetSize(sizeX, sizeY);
        Grid.InitializeArray();
        Grid.PopulateGrid();
        
    }
}
