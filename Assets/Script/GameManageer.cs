using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManageer : MonoBehaviour
{
    public GameObject[] 흑마법진들;
    public GameObject[] 길거리마법진들;
    public GameObject[] 쥐들;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public bool[] isFirst;
    public int MouseCount;

    public float CureentTime;

    public Clock clock;

    private void Start()
    {
        clock = GameObject.Find("시계").GetComponent<Clock>();
    }

    private void Update()
    {
        // 게임 시간이 끝나지 않았다면 시간 업데이트
        if (!clock.End)
        {
            CureentTime += Time.deltaTime;
        }

        // 조건 1: 길거리마법진 조건 검사
        if (!isFirst[0] && !길거리마법진들[0].activeSelf && !길거리마법진들[1].activeSelf && !길거리마법진들[2].activeSelf && !길거리마법진들[3].activeSelf && !길거리마법진들[4].activeSelf)
        {
            isFirst[0] = true;
            StartCoroutine(MouseSee());
        }

        // 조건 2: 쥐들 조건 검사
        if (!isFirst[1] && isFirst[0] && MouseCount >= 3)
        {
            isFirst[1] = true;
            흑마법진들[2].SetActive(false);
        }

        // 조건 3: 흑마법진 조건 검사
        if (!isFirst[2] && !흑마법진들[0].activeSelf && !흑마법진들[1].activeSelf && !흑마법진들[2].activeSelf && isFirst[1])
        {
            isFirst[2] = true;

            // 게임 종료 처리
            if (!clock.End)
            {
                clock.Clear(CureentTime);
            }
        }
    }

    IEnumerator MouseSee()
    {
        // 모든 쥐를 활성화
        foreach (GameObject obj in 쥐들)
        {
            obj.SetActive(true);
        }

        // 소리 재생
        yield return new WaitForSeconds(0.5f);
        PlaySoundEffect(0);
    }

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"사운드 {index} 재생");
        }
    }
}
