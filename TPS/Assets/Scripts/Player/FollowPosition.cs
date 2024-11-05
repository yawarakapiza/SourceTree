using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target; // ボールのTransformを指定
    public float fixedYPosition; // 固定するY座標の値

    void Update()
    {
        // ボールのX座標とZ座標に追従し、Y座標は固定
        transform.position = new Vector3(target.position.x, fixedYPosition, target.position.z);
    }
}
