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
    public float waitTime = 2f; // �ҋ@����
    public DetectionType detectionType; // ���o���@
}