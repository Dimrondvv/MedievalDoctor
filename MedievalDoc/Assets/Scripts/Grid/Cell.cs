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
        Workshop,
        NotValid
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
        if (Physics.Raycast(new Vector3(cellPosition.x, cellPosition.y +0.0001f, cellPosition.z), Vector3.down, out hit))
        {
            
            string tag = hit.transform.gameObject.tag;
            Debug.Log(tag + " " + cellPosition);
            switch (tag)
            {
                case "Reception":
                    roomState = RoomState.Reception;
                    isInRoom = true;
                    break;
                case "Clinic":
                    roomState = RoomState.Clinic;
                    isInRoom = true;
                    break;
                case "Workshop":
                    roomState = RoomState.Workshop;
                    isInRoom = true;
                    break;
                default:
                    roomState = RoomState.NotValid;
                    isInRoom = false;
                    break;
            }
        }
    }
    public void DebugPosition()
    {
        if(roomState!=RoomState.NotValid)
            Debug.Log(cellPosition + " " + roomState);
    }
}
