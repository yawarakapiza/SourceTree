using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public GameObject[] targetObjects; // 対象のオブジェクトを格納する配列
    private Vector3[] objectPositions; // 対象のオブジェクトの初期位置を格納する配列
    int x = 0;

    public bool isModeChange = false; // モード変更フラグ
    public float cooldownTime = 1.0f; // クールダウン時間
    private float cooldownTimer = 0.0f; // クールダウンタイマー

    void Start()
    {
        objectPositions = new Vector3[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            objectPositions[i] = targetObjects[i].transform.position;
        }
    }

    void Update()
    {
        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i].transform.position != objectPositions[i])
            {
                objectPositions[i] = targetObjects[i].transform.position;
                x = i;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isModeChange = !isModeChange;
            Debug.Log("Mode change: " + isModeChange);

            if (isModeChange)
            {
                if (targetObjects.Length > 1 && targetObjects[0] != null && targetObjects[1] != null)
                {
                    targetObjects[0].SetActive(false);
                    ClearTrail(targetObjects[0]);  // トレイルをクリア
                    targetObjects[1].SetActive(true);
                    targetObjects[1].transform.position = objectPositions[x];
                }
            }
            else
            {
                if (targetObjects.Length > 1 && targetObjects[0] != null && targetObjects[1] != null)
                {
                    Rigidbody rbElement1 = targetObjects[1].GetComponent<Rigidbody>(); // Element 1のRigidbodyコンポーネントを取得
                    if (rbElement1 != null)
                    {
                        rbElement1.velocity = Vector3.zero; // 速度を0に設定
                        rbElement1.angularVelocity = Vector3.zero; // 角速度を0に設定
                    }

                    targetObjects[1].SetActive(false);
                    ClearTrail(targetObjects[1]);  // トレイルをクリア
                    targetObjects[0].SetActive(true);
                    targetObjects[0].transform.position = objectPositions[x];
                }
            }

            cooldownTimer = cooldownTime;
        }
    }

    // トレイルをクリアする関数
    private void ClearTrail(GameObject obj)
    {
        // 指定したオブジェクトとその子オブジェクトからすべてのTrailRendererを取得しクリアする
        TrailRenderer[] trailRenderers = obj.GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trailRenderer in trailRenderers)
        {
            if (trailRenderer != null)
            {
                trailRenderer.Clear(); // 各TrailRendererの軌跡をクリア
            }
        }
    }
}
