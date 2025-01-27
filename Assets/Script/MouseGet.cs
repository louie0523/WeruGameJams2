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
    public GameObject[] 마도서사용;
    public GameObject 마도서;
    public bool 근처였어 = false;
    public GameManageer gm;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("주인공");
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < NeedDistence && !isDie && 마도서.activeSelf)
        {
            마도서사용[Num].SetActive(true);
            근처였어 = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDie = true;
                StartCoroutine("NpcDie");
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= NeedDistence && 근처였어 && !isDie)
        {
            마도서사용[Num].SetActive(false);
            근처였어 = false;
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
        마도서사용[Num].SetActive(false);
    }

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"사운드 {index} 재생");
        }
    }
}
