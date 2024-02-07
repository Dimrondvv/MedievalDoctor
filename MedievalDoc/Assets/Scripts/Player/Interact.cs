using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    InteractEvent interact = new InteractEvent();
    PickupController playerController;


    public InteractEvent GetInteractEvent
    {
        get 
        { 
            if(interact == null) interact = new InteractEvent();
            return interact; 
        }
    }

    public PickupController GetPlayer
    {
        get { return playerController; }
    }

    public void CallInteract(PickupController interactedPlayer)
    {
        playerController = interactedPlayer;
        interact.CallInteractEvent();
    }
}

public class InteractEvent
{
    public delegate void InteractHandler();
    public InteractHandler HasInteracted;

    public void CallInteractEvent() => HasInteracted?.Invoke();
}