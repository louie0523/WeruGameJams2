using UnityEngine;

public class FloorTileGenerator : MonoBehaviour
{
    public GameObject floorPrefab; // �ٴ� ������
    public int tileCount = 10; // ������ �ٴ� Ÿ�� ����
    public float xOffset = 7f; // �� Ÿ�� ���� X�� ����

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

        // ���ο� Ÿ�� ����
        for (int i = 0; i < tileCount; i++)
        {
            GameObject tile = Instantiate(floorPrefab, new Vector3(i * xOffset, 0, 0), Quaternion.identity);

            // Sorting Order ����
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i;
            }

            // ������ Ÿ���� ���� ������Ʈ�� �ڽ����� ����
            tile.transform.SetParent(transform);
        }
    }
}
