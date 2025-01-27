using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isDie = false;  
    public float NeedDistence = 3f;
    public GameObject Player;
    public Image ������;
    public GameObject NpcDIeobj;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject Į;
    public GameObject �渶����ǥ��;
    public GameObject Į���;
    public bool ��ó���� = false;
    public GameObject ������1;

    public enum NpcActive
    {
        Walk,
        Idle,
    }

    public NpcActive npcActive;

    public enum NpcType
    {
        Npc,
        BadNpc,
    }

    public NpcType npcType;

    private float waitTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("���ΰ�");
        ������ = GameObject.Find("������").GetComponent<Image>();
        StartPosition = transform.position;
        LeftEndPosition = StartPosition - new Vector3(Left, 0, 0);
        RightEndPosition = StartPosition + new Vector3(Right, 0, 0);
        if(npcActive == NpcActive.Walk)
        {
            StartCoroutine(NPCMovement());
        } else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void Update()
    {
        if(npcType == NpcType.BadNpc && Vector3.Distance(Player.transform.position, this.transform.position) < NeedDistence && !isDie && Į.activeSelf)
        {
            Į���.SetActive(true);
            ��ó���� = true;
            if(Input.GetKeyDown(KeyCode.F))
            {
                isDie = true;
                Speed = 0f;
                Color currentColor = ������.color;
                currentColor.a = 1f;
                ������.color = currentColor;
                StartCoroutine("NpcDie");
            }
        }
        else if(npcType == NpcType.BadNpc && Vector3.Distance(Player.transform.position, this.transform.position) >= NeedDistence && ��ó���� && !isDie)
        {
            Į���.SetActive(false);
            ��ó���� = false;
        }
    }

    IEnumerator NpcDie()
    {
        yield return new WaitForSeconds(0.1f);
        Į���.SetActive(false);
        PlaySoundEffect(0);
        Renderer NpcRen = this.GetComponent<Renderer>();
        NpcRen.enabled = false;
        NpcDIeobj.SetActive(true);
        �渶����ǥ��.SetActive(false);
        yield return new WaitForSeconds(0.9f);
        Color currentColor = ������.color;
        currentColor.a = 0f;
        ������.color = currentColor;
        PlaySoundEffect(1);
        ������1.SetActive(false);
    }

    private IEnumerator NPCMovement()
    {
        while (!isDie)
        {
            yield return StartCoroutine(WalkToPosition(LeftEndPosition));
            yield return StartCoroutine(WaitAtPosition());
            if (!isFirst)
            {
                Vector3 currentRotation = this.transform.eulerAngles;
                currentRotation.y += 180f;
                this.transform.rotation = Quaternion.Euler(currentRotation);
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

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"���� {index} ���");
        }
    }
}
