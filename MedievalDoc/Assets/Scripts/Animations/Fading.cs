using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    public Animator animator;

    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }
}
