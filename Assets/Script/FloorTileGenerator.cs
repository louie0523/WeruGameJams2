using UnityEngine;

public class FloorTileGenerator : MonoBehaviour
{
    public GameObject floorPrefab; // �ٴ� ������
    public int tileCount = 10; // ������ �ٴ� Ÿ�� ����
    public float xOffset = 7f; // �� Ÿ�� ���� X�� ����
    public int minOrder = 0;

    // ��ư���� ȣ���� �޼���
    public void GenerateFloorTiles()
    {
        if (floorPrefab == null)
        {
            Debug.LogError("Floor Prefab is not assigned!");
            return;
        }

        // ���� Ÿ�� ���� (�ߺ� ����)
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // �θ� ��ü�� X, Y ��ǥ ��������
        float parentX = transform.position.x;
        float parentY = transform.position.y;

        // ���ο� Ÿ�� ����
        for (int i = 0; i < tileCount; i++)
        {
            // �߽��� �������� ���ʿ� Ÿ�� ����
            float offsetX = parentX + (i - tileCount / 2f) * xOffset;
            Vector3 tilePosition = new Vector3(offsetX, parentY, 0);
            GameObject tile = Instantiate(floorPrefab, tilePosition, Quaternion.identity);

            // Sorting Order ����
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i + minOrder;
            }

            // ������ Ÿ���� ���� ������Ʈ�� �ڽ����� ����
            tile.transform.SetParent(transform);
        }
    }
}
