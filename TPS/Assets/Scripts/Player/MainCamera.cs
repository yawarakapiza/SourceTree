using UnityEngine;
using System.Collections;
public class MainCamera : MonoBehaviour
{
    public WayPoint[] waypoints; // WayPoint�N���X���g�p
    public float moveSpeed = 5f; // �ړ����x
    public GameObject[] playerObjects; // �蓮�őI������Player�I�u�W�F�N�g�̔z��

    private int currentWaypointIndex = 0; // ���݂̖ڕW�n�_�̃C���f�b�N�X
    private bool isMoving = false; // �ړ������ǂ����̃t���O

    void Update()
    {
        // ���݂̃E�F�C�|�C���g�̔�����@���擾
        if (currentWaypointIndex < waypoints.Length)
        {
            WayPoint currentWaypoint = waypoints[currentWaypointIndex];
            bool shouldMove = false;

            switch (currentWaypoint.detectionType)
            {
                case DetectionType.TopHalf:
                    shouldMove = IsPlayerInTopHalf();
                    break;
                case DetectionType.TopRight:
                    shouldMove = IsPlayerInTopRight();
                    break;
                case DetectionType.TopLeft:
                    shouldMove = IsPlayerInTopLeft();
                    break;
                case DetectionType.RightHalf:
                    shouldMove = IsPlayerInRightHalf();
                    break;
                case DetectionType.LeftHalf:
                    shouldMove = IsPlayerInLeftHalf();
                    break;
            }

            if (shouldMove)
            {
                // �ړ������ֈړ�
                MoveToWaypoint();
            }
        }
    }

    bool IsPlayerInTopHalf()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.y > 0.4f) // �J�����̏㔼��
            {
                return true;
            }
        }
        return false;
    }
    bool IsPlayerInTopRight()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.y > 0.4f || viewportPos.x > 0.6f) 
            {
                return true;
            }
        }
        return false;
    }
    bool IsPlayerInTopLeft()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.y > 0.4f || viewportPos.x < 0.6f) 
            {
                return true;
            }
        }
        return false;
    }

    bool IsPlayerInRightHalf()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.x > 0.6f) // �J�����̉E����
            {
                return true;
            }
        }
        return false;
    }

    bool IsPlayerInLeftHalf()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.x < 0.6f) // �J�����̍�����
            {
                return true;
            }
        }
        return false;
    }

    void MoveToWaypoint()
    {
        // ���݂̖ڕW�n�_���z��͈͓̔��ɂ��邩�m�F
        if (currentWaypointIndex < waypoints.Length)
        {
            // �ڕW�n�_�ւ̕����x�N�g�����v�Z
            Vector3 direction = waypoints[currentWaypointIndex].transform.position - transform.position;
            direction.y = 0f; // �J�����𐅕��ɐ�������

            // �ړ������Ɋ�Â��ăJ�������ړ�������
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

            // �ڕW�n�_�ɏ\���߂Â����玟�̖ڕW�n�_�ֈړ�
            if (direction.magnitude < 0.1f && !isMoving)
            {
                StartCoroutine(WaitAtWaypoint(waypoints[currentWaypointIndex].waitTime));
                isMoving = true;
            }
        }
    }

    IEnumerator WaitAtWaypoint(float waitTime)
    {
        // Waypoint�ł̑ҋ@���Ԃ����ҋ@
        yield return new WaitForSeconds(waitTime);

        // �ҋ@��A����Waypoint�ֈړ�
        currentWaypointIndex++;
        isMoving = false;
    }
}