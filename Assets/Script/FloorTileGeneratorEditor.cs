using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorTileGenerator))]
public class FloorTileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // FloorTileGenerator 컴포넌트를 가져옴
        FloorTileGenerator generator = (FloorTileGenerator)target;

        // 버튼 추가
        if (GUILayout.Button("Generate Floor Tiles"))
        {
            generator.GenerateFloorTiles(); // 버튼을 눌렀을 때 메서드 실행
        }
    }
}
