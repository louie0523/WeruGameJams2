using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    public GameObject itemIcon;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject GetTrue;
    Animator animator;
    public GameObject[] PlusObject;
    public enum ItemType
    {
        Key,
        MagicBook,
        Knife,
        CrowEye,
    }
    public ItemType itemType;
    public float Distance = 2f;
    public GameObject Player;
    public bool thisItemGet = false;
    private void Start()
    {
        Player = GameObject.Find("���ΰ�");
        animator = this.GetComponent<Animator>();
        GetTrue.SetActive(false);
    }

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, Player.transform.position) <= Distance && !thisItemGet )
        {
            GetTrue.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(itemType + " �������� ȹ���ϼ̽��ϴ�.");
                if(itemType == ItemType.Knife)
                {
                    PlusObject[0].SetActive(true);
                }
                PlaySoundEffect(0);
                thisItemGet = true;
                itemIcon.SetActive(true);
                gameObject.SetActive(false);
                GetTrue.SetActive(false);
            }
        } else
        {
            GetTrue.SetActive(false);
        }
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
