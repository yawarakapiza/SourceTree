using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    public float activationDistance = 10f; // �G���A�N�e�B�u�ɂȂ鋗��
    private GameObject player;
    private bool isActive = false; // �G���A�N�e�B�u���ǂ����̃t���O

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
            // �G����A�N�e�B�u�ŁA�v���C���[������̋������ɂ���ꍇ�A�G���A�N�e�B�u�ɂ���
            ActivateEnemy();
        }
        else if (isActive && distanceToPlayer > activationDistance)
        {
            // �G���A�N�e�B�u�ŁA�v���C���[������̋����O�ɏo���ꍇ�A�G���A�N�e�B�u�ɂ���
            DeactivateEnemy();
        }
    }

    void ActivateEnemy()
    {
        // �G���A�N�e�B�u�ɂ���
        gameObject.SetActive(true);
        isActive = true;
    }

    void DeactivateEnemy()
    {
        // �G���A�N�e�B�u�ɂ���
        gameObject.SetActive(false);
        isActive = false;
    }
}
