using System.Collections;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public GameObject MagicBook;
    public float GreatResult = 55f;
    public GameObject Player; // �÷��̾� ������Ʈ
    public GameObject MagicUI; // ������ UI ������Ʈ
    public GameObject JudgmentCircle; // ���� ��
    public GameObject GrowingCircle; // Ŀ���� ��
    public float Distance = 1.5f; // ���������� �Ÿ�
    public int MaxRound = 5; // �ִ� ���� ��
    public float MinSpeed = 1f; // �ּ� �ӵ�
    public float MaxSpeed = 2f; // �ִ� �ӵ�
    public bool isClear = false; // ������ Ŭ���� ����

    private bool isActive = false; // UI Ȱ��ȭ ����
    private int currentRound = 0; // ���� ����
    private bool isInRange = false; // �÷��̾ ���� ���� �ִ��� ����
    private bool roundInProgress = false; // ���� ���� �� ����
    private bool isWaitingForInput = false; // �Է� ��� �� ����
    private bool isInputReceived = false; // �Է��� ���ŵǾ����� ����
    private float roundDuration = 0f; // ���� ������ ���� �ð�
    private float elapsedTime = 0f; // ��� �ð�
    private bool imThisRoundClear = false;

    public AudioSource sfxSource;
    public AudioClip[] soundEffects;
    public GameObject MagicBookIcon;

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) <= Distance && MagicBook.activeSelf)
        {
            isInRange = true;
            MagicBookIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && !isClear && !isActive)
            {
                ActivateMagicUI(); // ���� UI Ȱ��ȭ
            }
        }
        else
        {
            isInRange = false;
            MagicBookIcon.SetActive(false);
            if (isActive) // ���� ������ ������ UI ��Ȱ��ȭ
            {
                DeactivateMagicUI();
            }
        }

        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // ESC �Է� �� UI ����
            {
                DeactivateMagicUI();
            }

            // ���� ���� ���� �� �� ũ�� ������Ʈ
            if (roundInProgress)
            {
                UpdateGrowingCircle();

                // ���� �Է� ��� ���¿��� �� ũ�Ⱑ ���� �̻� Ŀ���� ���� �Է� ��� ����
                if (GrowingCircle.GetComponent<RectTransform>().localScale.x >= 25 && !isWaitingForInput && GrowingCircle.GetComponent<RectTransform>().localScale.x < 770)
                {
                    isWaitingForInput = true; // �Է� ��� ����
                    Debug.Log("���� �Է� ���...");
                }

                // Ŀ���� �� ũ�Ⱑ 700 �̻����� Ŀ���� �� �ڵ� Ż��
                if (GrowingCircle.GetComponent<RectTransform>().localScale.x >= 770f && !imThisRoundClear)
                {
                    Debug.Log("Ŀ���� �� ũ�Ⱑ �ʹ� Ŀ�����ϴ�! �ڵ� Ż��.");
                    PlaySoundEffect(1);
                    DeactivateMagicUI(); // ���� �� UI ����
                }
            }

            // ���� ����
            if (roundInProgress && isWaitingForInput && !isInputReceived)
            {
                CheckPlayerInput();
            }
        }
    }

    private void ActivateMagicUI()
    {
        Debug.Log("�������� ���ϴ�.");
        isActive = true;
        MagicUI.SetActive(true); // UI Ȱ��ȭ
        currentRound = 0;
        StartCoroutine(MagicRoundRoutine()); // ���� �ڷ�ƾ ����
    }

    private IEnumerator MagicRoundRoutine()
    {
        while (currentRound < MaxRound)
        {
            Debug.Log($"���� {currentRound + 1} ����");
            imThisRoundClear = false;

            if (!isActive)
            {
                Debug.Log("UI�� ��Ȱ��ȭ �Ǿ����Ƿ� ���带 �����մϴ�.");
                yield break;
            }

            // ���� ���� �غ�
            roundInProgress = true;
            ResetGrowingCircle();
            roundDuration = Random.Range(MinSpeed, MaxSpeed);
            elapsedTime = 0f;
            isWaitingForInput = false;
            isInputReceived = false;

            // ���� ����
            yield return new WaitUntil(() => !roundInProgress);

            currentRound++;
        }

        Debug.Log("������ Ŭ����!");
        MagicBookIcon.SetActive(false);
        PlaySoundEffect(2);
        isClear = true;
        isActive = false;
        MagicUI.SetActive(false); // UI ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void CheckPlayerInput()
    {
        if (isInputReceived) return;

        float judgmentScaleX = JudgmentCircle.GetComponent<RectTransform>().localScale.x;
        float growingScaleX = GrowingCircle.GetComponent<RectTransform>().localScale.x;

        if (Mathf.Abs(judgmentScaleX - growingScaleX) <= GreatResult)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("���� ����!" + (judgmentScaleX - growingScaleX));
                isInputReceived = true;
                roundInProgress = false;
                imThisRoundClear = true;
                PlaySoundEffect(0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("���� ����! UI ����." + (judgmentScaleX - growingScaleX));
                PlaySoundEffect(1);
                DeactivateMagicUI();
            }
        }
    }

    private void UpdateGrowingCircle()
    {
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / roundDuration;
        float size = Mathf.Lerp(0f, 775f, progress);
        GrowingCircle.GetComponent<RectTransform>().localScale = new Vector3(size, size, size);

        if (elapsedTime >= roundDuration)
        {
            roundInProgress = false; // ���� ����
        }
    }

    private void ResetGrowingCircle()
    {
        GrowingCircle.GetComponent<RectTransform>().localScale = Vector3.zero; // ũ�� �ʱ�ȭ
    }

    private void DeactivateMagicUI()
    {
        Debug.Log("�������� �����մϴ�.");
        MagicBookIcon.SetActive(false);
        isActive = false;
        MagicUI.SetActive(false);
        currentRound = 0;
        roundInProgress = false;
        isWaitingForInput = false;
        isInputReceived = false;
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
