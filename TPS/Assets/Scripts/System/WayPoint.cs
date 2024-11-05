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
    public float waitTime = 2f; // ‘Ò‹@ŠÔ
    public DetectionType detectionType; // ŒŸo•û–@
}