using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManageer : MonoBehaviour
{
    public GameObject[] �渶������;
    public GameObject[] ��Ÿ���������;
    public GameObject[] ���;
    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public bool[] isFirst;
    public int MouseCount;

    public float CureentTime;

    public Clock clock;

    private void Start()
    {
        clock = GameObject.Find("�ð�").GetComponent<Clock>();
    }

    private void Update()
    {
        // ���� �ð��� ������ �ʾҴٸ� �ð� ������Ʈ
        if (!clock.End)
        {
            CureentTime += Time.deltaTime;
        }

        // ���� 1: ��Ÿ������� ���� �˻�
        if (!isFirst[0] && !��Ÿ���������[0].activeSelf && !��Ÿ���������[1].activeSelf && !��Ÿ���������[2].activeSelf && !��Ÿ���������[3].activeSelf && !��Ÿ���������[4].activeSelf)
        {
            isFirst[0] = true;
            StartCoroutine(MouseSee());
        }

        // ���� 2: ��� ���� �˻�
        if (!isFirst[1] && isFirst[0] && MouseCount >= 3)
        {
            isFirst[1] = true;
            �渶������[2].SetActive(false);
        }

        // ���� 3: �渶���� ���� �˻�
        if (!isFirst[2] && !�渶������[0].activeSelf && !�渶������[1].activeSelf && !�渶������[2].activeSelf && isFirst[1])
        {
            isFirst[2] = true;

            // ���� ���� ó��
            if (!clock.End)
            {
                clock.Clear(CureentTime);
            }
        }
    }

    IEnumerator MouseSee()
    {
        // ��� �㸦 Ȱ��ȭ
        foreach (GameObject obj in ���)
        {
            obj.SetActive(true);
        }

        // �Ҹ� ���
        yield return new WaitForSeconds(0.5f);
        PlaySoundEffect(0);
    }

    private void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length && sfxSource != null)
        {
            sfxSource.PlayOneShot(soundEffects[index]);
            Debug.Log($"���� {index} ���");
        }
    }
}
