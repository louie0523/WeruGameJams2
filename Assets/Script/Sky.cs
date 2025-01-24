using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position + new Vector3(0, 5.7f, 0);
    }
}
