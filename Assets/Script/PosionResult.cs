using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionResult : MonoBehaviour
{
    public SelectPosion[] selectPosions;
    public GameObject ResultPosion;
    public int Result = 0;
    private SpriteRenderer spriteRenderer;
    Color Default = new Color(255f, 255f, 255f);
    Color Red = new Color(255f, 0, 0);
    public Color BluePosion;
    Color Bule = new Color(0, 54f, 255f);
    Color Yellow = new Color(255f, 215f, 0);
    private void Start()
    {
        spriteRenderer = ResultPosion.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if ((selectPosions[0].cureentPosion == 1 && selectPosions[1].cureentPosion == 1))
        {
            spriteRenderer.color = BluePosion;
            Result = 1;
        } else if ((selectPosions[0].cureentPosion == 1 && selectPosions[1].cureentPosion == 2) || (selectPosions[0].cureentPosion == 2 && selectPosions[1].cureentPosion == 1))
        {
            spriteRenderer.color = Red;
            Result = 2;
        } else if((selectPosions[0].cureentPosion == 2 && selectPosions[1].cureentPosion == 2))
        {
            spriteRenderer.color = Yellow;
            Result = 3;
        } else
        {
            spriteRenderer.color = Default;
            Result = 0;
        }
    }
}
