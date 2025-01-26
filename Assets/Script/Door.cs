using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;
    public GameObject Key;
    public float Distance = 2f;
    public Transform GoPostion;
    private void Start()
    {
        Player = GameObject.Find("주인공");
        Key = GameObject.Find("Key");
    }
    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.position) < Distance && Input.GetKeyDown(KeyCode.F) && Key.activeSelf)
        {
            Debug.Log("집 내부로 이동합니다.");
            Player.transform.position = GoPostion.transform.position;

        }
    }
}
