using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    private AudioSource footstep;
    // Start is called before the first frame update
    void Start()
    {
        footstep = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        footstep.Play();
    }
    

}
