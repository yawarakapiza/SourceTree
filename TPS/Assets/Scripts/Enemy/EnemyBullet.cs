using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float destroyDelay = 2f; // 消去までの待機時間（秒）
    public int damage = 1; // 弾のダメージ量

    void Start()
    {
        // 指定秒後にDestroyメソッドを呼び出して自分自身（このBulletオブジェクト）を消去する
        Destroy(gameObject, destroyDelay);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            // 弾がPlayerに当たった場合、Playerにダメージを与える
            other.GetComponent<Player>().TakeDamage(damage);
            // 弾の消去処理
            Destroy(gameObject);
        }
    }
}