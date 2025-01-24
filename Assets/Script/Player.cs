using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    public bool FaceRight = true;
    public float Speed = 1.0f;
    bool isWalk = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Walk();
    }

    void Walk()
    {
        isWalk = false;

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-this.transform.right * Speed * Time.deltaTime);
            isWalk = true;

            if (FaceRight)
            {
                FaceRotate();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(this.transform.right * Speed * Time.deltaTime);
            isWalk = true;

            if (!FaceRight)
            {
                FaceRotate();
            }
        }

        if (isWalk)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    void FaceRotate()
    {
        FaceRight = !FaceRight;
        spriteRenderer.flipX = !FaceRight;

    }
}
