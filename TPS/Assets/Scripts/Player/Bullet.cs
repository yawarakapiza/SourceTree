using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 2f; // 消去までの待機時間（秒）
    public int damage = 10; // 弾のダメージ量

    void Start()
    {
        // 指定秒後にDestroyメソッドを呼び出して自分自身（このBulletオブジェクト）を消去する
        Destroy(gameObject, destroyDelay);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            // 弾がエネミーに当たった場合、エネミーにダメージを与える
            other.GetComponent<Enemy>().TakeDamage(damage);
            // 弾の消去処理
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            Debug.Log("hit");
            // 弾がエネミーに当たった場合、エネミーにダメージを与える
            other.GetComponent<EnemyBoss1>().TakeDamage(damage);
            // 弾の消去処理
            Destroy(gameObject);
        }
    }
}
