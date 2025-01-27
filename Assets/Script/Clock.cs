using System.Collections;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public int CurrentTime = 0;
    public float TimeReWinderWait = 120f;
    public GameObject LargeLine;
    public GameObject SmallLine;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip[] DevilBgm;
    public AudioClip[] soundEffects;
    private Animator animator;
    private bool First = false;
    public bool End = false;
    public TextMeshProUGUI PlatTime;

    public GameObject[] DestroyGameObjectList;

    private void Start()
    {
        if (!First)
        {
            animator = this.GetComponent<Animator>();
        }
        if (!End)
        {
            InvokeRepeating("TimeReWinder", TimeReWinderWait, TimeReWinderWait);
            Debug.Log("�ð�ٴ� ���ư�");
        }
    }
    
    void TimeReWinder()
    {
        RectTransform LL = LargeLine.GetComponent<RectTransform>();
        RectTransform SL = SmallLine.GetComponent<RectTransform>();
        CurrentTime += 2;
        LL.eulerAngles += new Vector3(0, 0, -60);
        SL.eulerAngles += new Vector3(0, 0, -58);
        Debug.Log($"���� �ð�: {CurrentTime}");
        if (CurrentTime >= 12 && !End)
        {
            CancelInvoke("TimeReWinder");
            End = true;
            StartCoroutine("Devil");
        }
    }

    public void Clear(float time)
    {
        PlatTime.text = "Time : " + (int)time + "'s";
        End = true;
        Debug.Log("Ŭ����!");
        StartCoroutine("Win");
    }

    IEnumerator Devil()
    {
        foreach (GameObject go in DestroyGameObjectList)
        {
            go.SetActive(false);
        }
        animator.SetTrigger("Start");
        bgmSource.Stop();
        bgmSource.clip = DevilBgm[0];
        bgmSource.Play();
        yield return new WaitForSeconds(5.4f);
        PlaySoundEffect(0);
        yield return new WaitForSeconds(4.1f);
        int Ran = Random.Range(1, 12);
        PlaySoundEffect(Ran);
    }

    IEnumerator Win()
    {
        foreach (GameObject go in DestroyGameObjectList)
        {
            go.SetActive(false);
        }
        animator.SetTrigger("Clear");
        bgmSource.Stop();
        bgmSource.clip = DevilBgm[1];
        bgmSource.Play();
        yield return new WaitForSeconds(10f);
        bgmSource.Stop();
        PlaySoundEffect(12);
        //Btns[0].SetActive(false);
        //Btns[1].SetActive(false);
        //Btns[2].SetActive(true);
        //Btns[3].SetActive(true);
        yield return new WaitForSeconds(1f);
        PlaySoundEffect(13);

    }

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"���� {index} ���");
        }
    }

    public void ReRoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Title()
    {
        SceneManager.LoadScene(0);
    }
}
