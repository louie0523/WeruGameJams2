using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Delay : MonoBehaviour
{
    Animator animator;
    public float delay; 
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        Invoke("Wait", delay);
        
    }

    void Wait()
    {
        animator.enabled = true;
    }
}
