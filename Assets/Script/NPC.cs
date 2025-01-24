using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float Speed = 3f;
    public float Left = 5f;
    public float Right = 5f;
    public float WaitTime = 3f;
    public bool FaceRight = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 StartPosition;
    private Vector3 LeftEndPosition;
    private Vector3 RightEndPosition;
    public bool isFirst = false;

    private float waitTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        StartPosition = transform.position;
        LeftEndPosition = StartPosition - new Vector3(Left, 0, 0);
        RightEndPosition = StartPosition + new Vector3(Right, 0, 0);

        StartCoroutine(NPCMovement());
    }

    private IEnumerator NPCMovement()
    {
        while (true)
        {
            yield return StartCoroutine(WalkToPosition(LeftEndPosition));
            yield return StartCoroutine(WaitAtPosition());
            if (!isFirst)
            {
                this.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                isFirst = true;
            }
            yield return StartCoroutine(WalkToPosition(RightEndPosition));
            yield return StartCoroutine(WaitAtPosition());
        }
    }

    private IEnumerator WalkToPosition(Vector3 targetPosition)
    {
        animator.SetBool("Walk", true);
        if (targetPosition.x > transform.position.x && !FaceRight)
        {
            FaceRotate();
        }
        else if (targetPosition.x < transform.position.x && FaceRight)
        {
            FaceRotate();
        }
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("Walk", false);
    }

    private IEnumerator WaitAtPosition()
    {
        waitTimer = WaitTime;
        yield return new WaitForSeconds(waitTimer);

        if (FaceRight)
        {
            FaceRotate();
        }
        else
        {
            FaceRotate();
        }
    }

    private void FaceRotate()
    {
        FaceRight = !FaceRight;
        spriteRenderer.flipX = !FaceRight;
    }
}
