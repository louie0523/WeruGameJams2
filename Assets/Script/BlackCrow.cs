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
    public Image 가리기;
    public GameObject NpcDIeobj;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject 칼;
    public GameObject 칼사용;
    public GameObject 까마귀눈;
    public bool 근처였어 = false;
    public int LuneString = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("주인공");
        가리기 = GameObject.Find("가리기").GetComponent<Image>();
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < NeedDistence && !isDie && 칼.activeSelf)
        {
            칼사용.SetActive(true);
            근처였어 = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDie = true;
                Color currentColor = 가리기.color;
                currentColor.a = 1f;
                가리기.color = currentColor;
                StartCoroutine("NpcDie");
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= NeedDistence && 근처였어 && !isDie)
        {
            칼사용.SetActive(false);
            근처였어 = false;
        }
    }

    IEnumerator NpcDie()
    {
        yield return new WaitForSeconds(0.1f);
        칼사용.SetActive(false);
        까마귀눈.SetActive(true);
        LuneString = Random.Range(1, 4);
        PlaySoundEffect(0);
        Renderer NpcRen = this.GetComponent<Renderer>();
        NpcRen.enabled = false;
        NpcDIeobj.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        Color currentColor = 가리기.color;
        currentColor.a = 0f;
        가리기.color = currentColor;
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
