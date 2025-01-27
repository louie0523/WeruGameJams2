using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLune : MonoBehaviour
{
    public GameObject Player;
    public Player player;
    public float Distance = 2f;
    public GameObject[] �鹮��;
    public BlackCrow BC;
    private void Start()
    {
        Player = GameObject.Find("���ΰ�");
        player = Player.GetComponent<Player>();
        BC = GameObject.Find("�渶�����").GetComponent<BlackCrow>();
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < Distance && BC.LuneString >= 1)
        {
            for(int i = 0; i < �鹮��.Length; i++)
            {
                if (i == BC.LuneString)
                {
                    �鹮��[i].SetActive(true);
                } else
                {
                    �鹮��[i].SetActive(false);
                }
            }
        }
    }
}
