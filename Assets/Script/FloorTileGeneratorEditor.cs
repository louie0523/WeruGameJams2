using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorTileGenerator))]
public class FloorTileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // FloorTileGenerator ������Ʈ�� ������
        FloorTileGenerator generator = (FloorTileGenerator)target;

        // ��ư �߰�
        if (GUILayout.Button("Generate Floor Tiles"))
        {
            generator.GenerateFloorTiles(); // ��ư�� ������ �� �޼��� ����
        }
    }
}
