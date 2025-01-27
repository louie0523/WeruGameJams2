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
    public GameObject 상호작용;
    public bool[] 근처였어;
    private void Start()
    {
        Player = GameObject.Find("주인공");
        player = Player.GetComponent<Player>();
    }
    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.position) < Distance)
        {
            상호작용.SetActive(true);
            근처였어[DoorNum] = true;
            if (Input.GetKeyDown(KeyCode.F) && Key.activeSelf && !player.GoingNow)
            {
                Debug.Log("집 내/외부로 이동합니다.");
                Player.transform.position = GoPostion.transform.position;
                player.ImNowGo();
            }
        } else if(Vector3.Distance(Player.transform.position, this.transform.position) >= Distance && 근처였어[DoorNum])
        {
            상호작용.SetActive(false);
            근처였어[DoorNum] = false;
        }

        
    }
}
