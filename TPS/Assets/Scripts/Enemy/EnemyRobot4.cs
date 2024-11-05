using UnityEngine;

public class EnemyRobot4 : Enemy
{
    public float approachSpeed = 3f; // �v���C���[�ɋ߂Â����x
    public float maintainDistance = 5f; // �v���C���[�Ƃ̈ێ����鋗��
    public float swayDistance = 1f; // �h��鋗��
    public float swaySpeed = 2f; // �h��鑬�x
    public float fireInterval = 2f; // �e�𔭎˂���Ԋu

    private float lastFireTime = 0f;
    private Vector3 initialPosition;

    protected override void EngagePlayer()
    {
        // �v���C���[�Ƃ̋������v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // �v���C���[�̕���������
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * approachSpeed);

        // �v���C���[�ɋ߂Â�
        if (distanceToPlayer > maintainDistance)
        {
            transform.Translate(directionToPlayer * approachSpeed * Time.deltaTime, Space.World);
            initialPosition = transform.position; // �����ʒu���L��
        }
        else
        {
            // ��苗�����ێ����Ă���ԁA���E�ɗh���
            float swayAmount = Mathf.Sin(Time.time * swaySpeed) * swayDistance;

            // ���E�ɗh���
            Vector3 swayPosition = initialPosition + transform.right * swayAmount;
            transform.position = new Vector3(swayPosition.x, transform.position.y, swayPosition.z);
        }

        // ���̊Ԋu�Ŏˌ�����
        if (Time.time >= lastFireTime + fireInterval && distanceToPlayer <= maintainDistance)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
