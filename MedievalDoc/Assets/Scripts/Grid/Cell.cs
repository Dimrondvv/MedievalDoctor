using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private Vector3 cellPosition;
    private GameObject cellContent; //GameObject that is put inside the cell (e.g Table, bed etc.)
    private bool isInRoom;
    private bool isTaken;
    private RoomState roomState;

    public enum RoomState //enum keeps state of in which room the grid cell is located
    {
        Reception,
        Clinic,
        Workshop
    }

    public Cell(Vector3 position) //Constructor that sets only position
    {
        cellPosition = position;
        isTaken = false;
    }
    public Cell(Vector3 position, bool isInRoom) //Constructor to set position and if the cell is in room
    {
        cellPosition = position;
        this.isInRoom = isInRoom;
        isTaken = false;
    }
    public void UpdateContent(GameObject content) //Update the content of the cell
    {
        cellContent = content;
    }
    
    public void CheckForState() //Raycast at position and traslate tag of the floor under to enum state
    {
        RaycastHit hit;
        if(Physics.Raycast(cellPosition, Vector3.down, out hit))
        {
            
            string tag = hit.transform.gameObject.tag;
            switch (tag)
            {
                case "Reception":
                    roomState = RoomState.Reception;
                    break;
                case "Clinic":
                    roomState = RoomState.Clinic;
                    break;
                case "Workshop":
                    roomState = RoomState.Workshop;
                    break;
                default:
                    Debug.Log("Wrong tag");
                    break;
            }
        }
    }
    public void DebugPosition()
    {
        Debug.Log(cellPosition + " " + roomState);
    }
}
