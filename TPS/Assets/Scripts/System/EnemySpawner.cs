using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 生成するエネミーのPrefab
    public int enemyCount = 5; // 生成するエネミーの数
    public float spawnRadius = 5f; // 生成する半径
    public float spawnInterval = 1f; // 生成する間隔
    public float detectionRange = 10f; // プレイヤーを検知する範囲

    private GameObject player;
    private bool hasSpawned = false; // エネミーの生成が一度行われたかどうかを確認するフラグ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!hasSpawned && player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            hasSpawned = true; // 生成フラグをセット
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = transform.position.y; // 同じ高さに生成

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
