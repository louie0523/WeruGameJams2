using System.Collections;
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
    private bool End = false;

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
        if (CurrentTime >= 12 && !End)
        {
            CancelInvoke("TimeReWinder");
            End = true;
            StartCoroutine("Devil");
        }
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

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"사운드 {index} 재생");
        }
    }

    public void ReRoad()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
