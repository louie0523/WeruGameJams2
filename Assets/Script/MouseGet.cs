using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGet : MonoBehaviour
{
    public int Num = 0;
    public bool isDie = false;
    public float NeedDistence = 2.5f;
    private SpriteRenderer spriteRenderer;
    public GameObject Player;
    public GameObject NpcDIeobj;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject[] ���������;
    public GameObject ������;
    public bool ��ó���� = false;
    public GameManageer gm;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("���ΰ�");
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < NeedDistence && !isDie && ������.activeSelf)
        {
            ���������[Num].SetActive(true);
            ��ó���� = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDie = true;
                StartCoroutine("NpcDie");
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= NeedDistence && ��ó���� && !isDie)
        {
            ���������[Num].SetActive(false);
            ��ó���� = false;
        }
    }

    IEnumerator NpcDie()
    {
        PlaySoundEffect(0);
        yield return new WaitForSeconds(0.1f);
        Renderer NpcRen = this.GetComponent<Renderer>();
        NpcRen.enabled = false;
        NpcDIeobj.SetActive(true);
        gm.MouseCount++;
        ���������[Num].SetActive(false);
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
