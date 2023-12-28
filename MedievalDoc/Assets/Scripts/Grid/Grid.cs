using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Grid
{
    private static Cell[,] grid;
    
    private static int sizeX;
    private static int sizeZ;
    private static int gridSpacing;


    public static void SetSize(int x, int y, int spacing) //Set x and y size
    {
        sizeX = x;
        sizeZ = y;
        gridSpacing = spacing;
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
                grid[x, z] = new Cell(new Vector3(x*gridSpacing, 0, z*gridSpacing));
                grid[x, z].CheckForState();
            }
        }
    }
    public static void LogGrid()
    {
        foreach(var cell in grid)
        {
            //cell.DebugPosition();
        }
    }
}
