using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    public float activationDistance = 10f; // 敵がアクティブになる距離
    private GameObject player;
    private bool isActive = false; // 敵がアクティブかどうかのフラグ

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!isActive && distanceToPlayer <= activationDistance)
        {
            // 敵が非アクティブで、プレイヤーが特定の距離内にいる場合、敵をアクティブにする
            ActivateEnemy();
        }
        else if (isActive && distanceToPlayer > activationDistance)
        {
            // 敵がアクティブで、プレイヤーが特定の距離外に出た場合、敵を非アクティブにする
            DeactivateEnemy();
        }
    }

    void ActivateEnemy()
    {
        // 敵をアクティブにする
        gameObject.SetActive(true);
        isActive = true;
    }

    void DeactivateEnemy()
    {
        // 敵を非アクティブにする
        gameObject.SetActive(false);
        isActive = false;
    }
}
