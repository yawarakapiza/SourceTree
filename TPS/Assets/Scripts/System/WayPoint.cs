using UnityEngine;
public enum DetectionType
{
    TopHalf,
    TopRight,
    TopLeft,
    RightHalf,
    LeftHalf
}

public class WayPoint : MonoBehaviour
{
    public float waitTime = 2f; // 待機時間
    public DetectionType detectionType; // 検出方法
}