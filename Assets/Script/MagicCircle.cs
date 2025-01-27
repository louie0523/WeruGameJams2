using System.Collections;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public GameObject MagicBook;
    public float GreatResult = 55f;
    public GameObject Player; // 플레이어 오브젝트
    public GameObject MagicUI; // 마법진 UI 오브젝트
    public GameObject JudgmentCircle; // 판정 원
    public GameObject GrowingCircle; // 커지는 원
    public float Distance = 1.5f; // 마법진과의 거리
    public int MaxRound = 5; // 최대 라운드 수
    public float MinSpeed = 1f; // 최소 속도
    public float MaxSpeed = 2f; // 최대 속도
    public bool isClear = false; // 마법진 클리어 여부

    private bool isActive = false; // UI 활성화 여부
    private int currentRound = 0; // 현재 라운드
    private bool isInRange = false; // 플레이어가 범위 내에 있는지 여부
    private bool roundInProgress = false; // 라운드 진행 중 여부
    private bool isWaitingForInput = false; // 입력 대기 중 여부
    private bool isInputReceived = false; // 입력이 수신되었는지 여부
    private float roundDuration = 0f; // 현재 라운드의 지속 시간
    private float elapsedTime = 0f; // 경과 시간
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
                ActivateMagicUI(); // 마법 UI 활성화
            }
        }
        else
        {
            isInRange = false;
            MagicBookIcon.SetActive(false);
            if (isActive) // 범위 밖으로 나가면 UI 비활성화
            {
                DeactivateMagicUI();
            }
        }

        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // ESC 입력 시 UI 종료
            {
                DeactivateMagicUI();
            }

            // 라운드 진행 중일 때 원 크기 업데이트
            if (roundInProgress)
            {
                UpdateGrowingCircle();

                // 판정 입력 대기 상태에서 원 크기가 일정 이상 커졌을 때만 입력 대기 시작
                if (GrowingCircle.GetComponent<RectTransform>().localScale.x >= 25 && !isWaitingForInput && GrowingCircle.GetComponent<RectTransform>().localScale.x < 770)
                {
                    isWaitingForInput = true; // 입력 대기 시작
                    Debug.Log("판정 입력 대기...");
                }

                // 커지는 원 크기가 700 이상으로 커졌을 때 자동 탈락
                if (GrowingCircle.GetComponent<RectTransform>().localScale.x >= 770f && !imThisRoundClear)
                {
                    Debug.Log("커지는 원 크기가 너무 커졌습니다! 자동 탈락.");
                    PlaySoundEffect(1);
                    DeactivateMagicUI(); // 실패 시 UI 종료
                }
            }

            // 판정 로직
            if (roundInProgress && isWaitingForInput && !isInputReceived)
            {
                CheckPlayerInput();
            }
        }
    }

    private void ActivateMagicUI()
    {
        Debug.Log("마법진을 엽니다.");
        isActive = true;
        MagicUI.SetActive(true); // UI 활성화
        currentRound = 0;
        StartCoroutine(MagicRoundRoutine()); // 라운드 코루틴 시작
    }

    private IEnumerator MagicRoundRoutine()
    {
        while (currentRound < MaxRound)
        {
            Debug.Log($"라운드 {currentRound + 1} 시작");
            imThisRoundClear = false;

            if (!isActive)
            {
                Debug.Log("UI가 비활성화 되었으므로 라운드를 종료합니다.");
                yield break;
            }

            // 라운드 실행 준비
            roundInProgress = true;
            ResetGrowingCircle();
            roundDuration = Random.Range(MinSpeed, MaxSpeed);
            elapsedTime = 0f;
            isWaitingForInput = false;
            isInputReceived = false;

            // 라운드 실행
            yield return new WaitUntil(() => !roundInProgress);

            currentRound++;
        }

        Debug.Log("마법진 클리어!");
        MagicBookIcon.SetActive(false);
        PlaySoundEffect(2);
        isClear = true;
        isActive = false;
        MagicUI.SetActive(false); // UI 비활성화
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
                Debug.Log("판정 성공!" + (judgmentScaleX - growingScaleX));
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
                Debug.Log("판정 실패! UI 종료." + (judgmentScaleX - growingScaleX));
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
            roundInProgress = false; // 라운드 종료
        }
    }

    private void ResetGrowingCircle()
    {
        GrowingCircle.GetComponent<RectTransform>().localScale = Vector3.zero; // 크기 초기화
    }

    private void DeactivateMagicUI()
    {
        Debug.Log("마법진을 종료합니다.");
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
            Debug.Log($"사운드 {index} 재생");
        }
    }
}
