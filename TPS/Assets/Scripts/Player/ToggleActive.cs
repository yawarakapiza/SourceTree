using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public GameObject[] targetObjects; // �Ώۂ̃I�u�W�F�N�g���i�[����z��
    private Vector3[] objectPositions; // �Ώۂ̃I�u�W�F�N�g�̏����ʒu���i�[����z��
    int x = 0;

    public bool isModeChange = false; // ���[�h�ύX�t���O
    public float cooldownTime = 1.0f; // �N�[���_�E������
    private float cooldownTimer = 0.0f; // �N�[���_�E���^�C�}�[

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
                    ClearTrail(targetObjects[0]);  // �g���C�����N���A
                    targetObjects[1].SetActive(true);
                    targetObjects[1].transform.position = objectPositions[x];
                }
            }
            else
            {
                if (targetObjects.Length > 1 && targetObjects[0] != null && targetObjects[1] != null)
                {
                    Rigidbody rbElement1 = targetObjects[1].GetComponent<Rigidbody>(); // Element 1��Rigidbody�R���|�[�l���g���擾
                    if (rbElement1 != null)
                    {
                        rbElement1.velocity = Vector3.zero; // ���x��0�ɐݒ�
                        rbElement1.angularVelocity = Vector3.zero; // �p���x��0�ɐݒ�
                    }

                    targetObjects[1].SetActive(false);
                    ClearTrail(targetObjects[1]);  // �g���C�����N���A
                    targetObjects[0].SetActive(true);
                    targetObjects[0].transform.position = objectPositions[x];
                }
            }

            cooldownTimer = cooldownTime;
        }
    }

    // �g���C�����N���A����֐�
    private void ClearTrail(GameObject obj)
    {
        // �w�肵���I�u�W�F�N�g�Ƃ��̎q�I�u�W�F�N�g���炷�ׂĂ�TrailRenderer���擾���N���A����
        TrailRenderer[] trailRenderers = obj.GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trailRenderer in trailRenderers)
        {
            if (trailRenderer != null)
            {
                trailRenderer.Clear(); // �eTrailRenderer�̋O�Ղ��N���A
            }
        }
    }
}
