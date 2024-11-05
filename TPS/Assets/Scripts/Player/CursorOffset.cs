using UnityEngine;

public class CursorOffset : MonoBehaviour
{
    // オフセット値
    public Vector2 offset = new Vector2(50f, 50f);

    void Update()
    {
        // マウスの現在位置を取得
        Vector2 mousePosition = Input.mousePosition;

        // オフセットを適用して新しい位置を計算
        Vector2 newPosition = new Vector2(mousePosition.x + offset.x, mousePosition.y + offset.y);

        // 新しい位置にマウスを移動
        Cursor.SetCursor(null, newPosition, CursorMode.Auto);
    }
}
