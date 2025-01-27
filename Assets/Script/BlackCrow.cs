using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class BlackCrow : MonoBehaviour
{
    public bool isDie = false;
    public float NeedDistence = 2.5f;
    private SpriteRenderer spriteRenderer;
    public GameObject Player;
    public Image ������;
    public GameObject NpcDIeobj;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject Į;
    public GameObject Į���;
    public GameObject ��ʹ�;
    public bool ��ó���� = false;
    public int LuneString = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("���ΰ�");
        ������ = GameObject.Find("������").GetComponent<Image>();
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < NeedDistence && !isDie && Į.activeSelf)
        {
            Į���.SetActive(true);
            ��ó���� = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDie = true;
                Color currentColor = ������.color;
                currentColor.a = 1f;
                ������.color = currentColor;
                StartCoroutine("NpcDie");
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= NeedDistence && ��ó���� && !isDie)
        {
            Į���.SetActive(false);
            ��ó���� = false;
        }
    }

    IEnumerator NpcDie()
    {
        yield return new WaitForSeconds(0.1f);
        Į���.SetActive(false);
        ��ʹ�.SetActive(true);
        LuneString = Random.Range(1, 4);
        PlaySoundEffect(0);
        Renderer NpcRen = this.GetComponent<Renderer>();
        NpcRen.enabled = false;
        NpcDIeobj.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Color currentColor = ������.color;
        currentColor.a = 0f;
        ������.color = currentColor;
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
