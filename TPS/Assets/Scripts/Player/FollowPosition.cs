using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target; // �{�[����Transform���w��
    public float fixedYPosition; // �Œ肷��Y���W�̒l

    void Update()
    {
        // �{�[����X���W��Z���W�ɒǏ]���AY���W�͌Œ�
        transform.position = new Vector3(target.position.x, fixedYPosition, target.position.z);
    }
}
