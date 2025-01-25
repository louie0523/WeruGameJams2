using UnityEngine;

public class FloorTileGenerator : MonoBehaviour
{
    public GameObject floorPrefab; // 바닥 프리팹
    public int tileCount = 10; // 생성할 바닥 타일 개수
    public float xOffset = 7f; // 각 타일 간의 X축 간격
    public int minOrder = 0;

    // 버튼으로 호출할 메서드
    public void GenerateFloorTiles()
    {
        if (floorPrefab == null)
        {
            Debug.LogError("Floor Prefab is not assigned!");
            return;
        }

        // 기존 타일 제거 (중복 방지)
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // 부모 객체의 X, Y 좌표 가져오기
        float parentX = transform.position.x;
        float parentY = transform.position.y;

        // 새로운 타일 생성
        for (int i = 0; i < tileCount; i++)
        {
            // 중심을 기준으로 양쪽에 타일 생성
            float offsetX = parentX + (i - tileCount / 2f) * xOffset;
            Vector3 tilePosition = new Vector3(offsetX, parentY, 0);
            GameObject tile = Instantiate(floorPrefab, tilePosition, Quaternion.identity);

            // Sorting Order 설정
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i + minOrder;
            }

            // 생성된 타일을 현재 오브젝트의 자식으로 설정
            tile.transform.SetParent(transform);
        }
    }
}
