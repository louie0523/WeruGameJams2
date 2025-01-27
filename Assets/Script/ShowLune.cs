using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLune : MonoBehaviour
{
    public GameObject Player;
    public Player player;
    public float Distance = 2f;
    public GameObject[] ·é¹®ÀÚ;
    public BlackCrow BC;
    private void Start()
    {
        Player = GameObject.Find("ÁÖÀÎ°ø");
        player = Player.GetComponent<Player>();
        BC = GameObject.Find("Èæ¸¶¹ý±î¸¶±Í").GetComponent<BlackCrow>();
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < Distance && BC.LuneString >= 1)
        {
            for(int i = 0; i < ·é¹®ÀÚ.Length; i++)
            {
                if (i == BC.LuneString)
                {
                    ·é¹®ÀÚ[i].SetActive(true);
                } else
                {
                    ·é¹®ÀÚ[i].SetActive(false);
                }
            }
        }
    }
}
