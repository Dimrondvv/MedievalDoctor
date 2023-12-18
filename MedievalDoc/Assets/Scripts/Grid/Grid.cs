using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Grid
{
    private static Cell[,] grid;
    
    private static int sizeX;
    private static int sizeZ;



    public static void SetSize(int x, int y) //Set x and y size
    {
        sizeX = x;
        sizeZ = y;
    }

    public static void InitializeArray() //Initialize array if the x and y sizes are set
    {
        if (sizeX == 0 || sizeZ == 0)
            return;
        grid = new Cell[sizeX, sizeZ];
    }

    public static void PopulateGrid() //Populate grid cells if the x and y sizes are set
    {
        if (sizeX == 0 || sizeZ == 0)
            return;

        for(int x = 0; x < sizeX; x++)
        {
            for(int z = 0; z < sizeZ; z++)
            {
                grid[x, z] = new Cell(new Vector3(x, 0, z));
            }
        }
    }
    public static void LogGrid()
    {
        foreach(var cell in grid)
        {
            cell.DebugPosition();
        }
    }
}
