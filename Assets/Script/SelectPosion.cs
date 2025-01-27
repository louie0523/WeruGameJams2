using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPosion : MonoBehaviour
{
    public int PosionNum = 0;
    public int cureentPosion = 0;
    public GameObject[] SeePostion;
    public GameObject Player;
    public Player player;
    public float Distance = 2f;
    public GameObject ��ȣ�ۿ�;
    public GameObject ���Ȯ��;
    public bool[] ��ó����;
    public BlackCrow BC;
    public PosionResult PR;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject ������2;
    private void Start()
    {
        Player = GameObject.Find("���ΰ�");
        player = Player.GetComponent<Player>();
        BC = GameObject.Find("�渶�����").GetComponent<BlackCrow>();
        PR = GameObject.Find("��������").GetComponent<PosionResult>();
    }
    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < Distance)
        {
            ��ȣ�ۿ�.SetActive(true);
            ��ó����[PosionNum] = true;
            if (PosionNum != 2)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (cureentPosion >= 1)
                    {
                        cureentPosion--;
                    }
                    else
                    {
                        cureentPosion = 0;
                    }
                    PosionSelect();
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    if (cureentPosion < 1)
                    {
                        cureentPosion++;
                    }
                    else
                    {
                        cureentPosion = 2;
                    }
                    PosionSelect();
                }
            } else
            {
                if (Input.GetKeyDown(KeyCode.F) && ������2.activeSelf)
                {
                    if(BC.LuneString == PR.Result && PR.Result != 0)
                    {
                        Debug.Log("�˸°� ������ �����߽��ϴ�!");
                        PlaySoundEffect(0);
                        ������2.SetActive(false);
                    } else
                    {
                        Debug.Log("���� ���� ����...");
                        PlaySoundEffect(1);
                    }
                }
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= Distance && ��ó����[PosionNum])
        {
            ��ȣ�ۿ�.SetActive(false);
            ��ó����[PosionNum] = false;
        }
    }

    void PosionSelect()
    {
        foreach(GameObject obj in SeePostion)
        {
            if(obj != SeePostion[cureentPosion])
            {
                obj.SetActive(false);
            } else
            {
                obj.SetActive(true);
            }
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
