using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    private Animator animator;
    private bool locked;

    public bool Locked {
        get { return locked; }
        set { locked = value; }
    }

    private void Start() {
        animator = GetComponent<Animator>();
        locked = true;
    }

    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }
    public void FadeIn() {
        animator.ResetTrigger("FadeOut");
        animator.Play("Fade_In");
    }

    public void Unlock() {
        locked = false;
    }
}
