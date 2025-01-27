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
    public GameObject 상호작용;
    public GameObject 결과확정;
    public bool[] 근처였어;
    public BlackCrow BC;
    public PosionResult PR;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject 마법진2;
    private void Start()
    {
        Player = GameObject.Find("주인공");
        player = Player.GetComponent<Player>();
        BC = GameObject.Find("흑마법까마귀").GetComponent<BlackCrow>();
        PR = GameObject.Find("포션제조").GetComponent<PosionResult>();
    }
    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < Distance)
        {
            상호작용.SetActive(true);
            근처였어[PosionNum] = true;
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
                if (Input.GetKeyDown(KeyCode.F) && 마법진2.activeSelf)
                {
                    if(BC.LuneString == PR.Result && PR.Result != 0)
                    {
                        Debug.Log("알맞게 포션을 조합했습니다!");
                        PlaySoundEffect(0);
                        마법진2.SetActive(false);
                    } else
                    {
                        Debug.Log("포션 조합 실패...");
                        PlaySoundEffect(1);
                    }
                }
            }
        }
        else if (Vector3.Distance(Player.transform.position, this.transform.position) >= Distance && 근처였어[PosionNum])
        {
            상호작용.SetActive(false);
            근처였어[PosionNum] = false;
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
            Debug.Log($"사운드 {index} 재생");
        }
    }
}
