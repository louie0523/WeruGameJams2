using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public int CurrentTime = 0;
    public float TimeReWinderWait = 120f;
    public GameObject LargeLine;
    public GameObject SmallLine;
    Animator animator;
    bool First = false;
    bool End = false;

    private void Start()
    {
        if(!First)
        {
            animator = this.GetComponent<Animator>();
        }
        if(!End)
        {
            InvokeRepeating("TimeReWinder", TimeReWinderWait, TimeReWinderWait);
            Debug.Log("시계바늘 돌아감");
        }
    }

    void TimeReWinder()
    {
        RectTransform LL = LargeLine.GetComponent<RectTransform>();
        RectTransform SL = SmallLine.GetComponent<RectTransform>();
        CurrentTime += 2;
        LL.eulerAngles += new Vector3(0, 0, -60);
        SL.eulerAngles += new Vector3(0, 0, -58);
        Debug.Log($"현재 시간: {CurrentTime}");
        if(CurrentTime >= 12 && !End)
        {
            End = true;
            Devil();
        }
        
    }

    void Devil()
    {
        animator.SetTrigger("Start");
    }
}
