using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int DoorNum = 0;
    public GameObject Player;
    public Player player;
    public GameObject Key;
    public float Distance = 2f;
    public Transform GoPostion;
    public GameObject ��ȣ�ۿ�;
    public bool[] ��ó����;
    private void Start()
    {
        Player = GameObject.Find("���ΰ�");
        player = Player.GetComponent<Player>();
    }
    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.position) < Distance)
        {
            ��ȣ�ۿ�.SetActive(true);
            ��ó����[DoorNum] = true;
            if (Input.GetKeyDown(KeyCode.F) && Key.activeSelf && !player.GoingNow)
            {
                Debug.Log("�� ��/�ܺη� �̵��մϴ�.");
                Player.transform.position = GoPostion.transform.position;
                player.ImNowGo();
            }
        } else if(Vector3.Distance(Player.transform.position, this.transform.position) >= Distance && ��ó����[DoorNum])
        {
            ��ȣ�ۿ�.SetActive(false);
            ��ó����[DoorNum] = false;
        }

        
    }
}
