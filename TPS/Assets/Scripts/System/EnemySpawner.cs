using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ��������G�l�~�[��Prefab
    public int enemyCount = 5; // ��������G�l�~�[�̐�
    public float spawnRadius = 5f; // �������锼�a
    public float spawnInterval = 1f; // ��������Ԋu
    public float detectionRange = 10f; // �v���C���[�����m����͈�

    private GameObject player;
    private bool hasSpawned = false; // �G�l�~�[�̐�������x�s��ꂽ���ǂ������m�F����t���O

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!hasSpawned && player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            hasSpawned = true; // �����t���O���Z�b�g
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = transform.position.y; // ���������ɐ���

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
