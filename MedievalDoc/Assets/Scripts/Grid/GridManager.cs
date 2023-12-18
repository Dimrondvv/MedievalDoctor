using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeZ;
    [SerializeField] private int gridSpacing = 1;
    // Start is called before the first frame update
    void Start()
    {
        Grid.SetSize(sizeX, sizeZ, gridSpacing);
        Grid.InitializeArray();
        Grid.PopulateGrid();
        Grid.LogGrid();
    }

    private void OnDrawGizmos()
    {
        for(int x = 0; x < sizeX; x+=gridSpacing)
        {
            for (int z = 0; z < sizeZ; z+=gridSpacing)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(new Vector3(x, 0, z), new Vector3(0.1f, 0.1f, 0.1f));
            }
        }
    }
}
