using UnityEngine;

public class CursorOffset : MonoBehaviour
{
    // �I�t�Z�b�g�l
    public Vector2 offset = new Vector2(50f, 50f);

    void Update()
    {
        // �}�E�X�̌��݈ʒu���擾
        Vector2 mousePosition = Input.mousePosition;

        // �I�t�Z�b�g��K�p���ĐV�����ʒu���v�Z
        Vector2 newPosition = new Vector2(mousePosition.x + offset.x, mousePosition.y + offset.y);

        // �V�����ʒu�Ƀ}�E�X���ړ�
        Cursor.SetCursor(null, newPosition, CursorMode.Auto);
    }
}
