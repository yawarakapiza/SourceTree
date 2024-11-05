using UnityEngine;
using System.Collections;
public class MainCamera : MonoBehaviour
{
    public WayPoint[] waypoints; // WayPointクラスを使用
    public float moveSpeed = 5f; // 移動速度
    public GameObject[] playerObjects; // 手動で選択するPlayerオブジェクトの配列

    private int currentWaypointIndex = 0; // 現在の目標地点のインデックス
    private bool isMoving = false; // 移動中かどうかのフラグ

    void Update()
    {
        // 現在のウェイポイントの判定方法を取得
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
                // 移動方向へ移動
                MoveToWaypoint();
            }
        }
    }

    bool IsPlayerInTopHalf()
    {
        foreach (var player in playerObjects)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPos.y > 0.4f) // カメラの上半分
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
            if (viewportPos.x > 0.6f) // カメラの右半分
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
            if (viewportPos.x < 0.6f) // カメラの左半分
            {
                return true;
            }
        }
        return false;
    }

    void MoveToWaypoint()
    {
        // 現在の目標地点が配列の範囲内にあるか確認
        if (currentWaypointIndex < waypoints.Length)
        {
            // 目標地点への方向ベクトルを計算
            Vector3 direction = waypoints[currentWaypointIndex].transform.position - transform.position;
            direction.y = 0f; // カメラを水平に制限する

            // 移動方向に基づいてカメラを移動させる
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

            // 目標地点に十分近づいたら次の目標地点へ移動
            if (direction.magnitude < 0.1f && !isMoving)
            {
                StartCoroutine(WaitAtWaypoint(waypoints[currentWaypointIndex].waitTime));
                isMoving = true;
            }
        }
    }

    IEnumerator WaitAtWaypoint(float waitTime)
    {
        // Waypointでの待機時間だけ待機
        yield return new WaitForSeconds(waitTime);

        // 待機後、次のWaypointへ移動
        currentWaypointIndex++;
        isMoving = false;
    }
}