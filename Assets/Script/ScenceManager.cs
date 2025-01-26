using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{


    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBn()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        Debug.Log("미구현 기능입니다.");
    }
}
