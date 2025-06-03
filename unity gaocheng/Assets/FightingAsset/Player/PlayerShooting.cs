using UnityEngine;
public enum AttackMode
{
    Normal,
    CircularSpread
}

public class PlayerShooting : MonoBehaviour
{
    [Header("�������")]
    [SerializeField] private Transform firePoint;       // �����
    [SerializeField] private BulletType currentBulletType = BulletType.Standard;  // ��ǰ�ӵ�����
    private PlayerStats stats;          // ����PlayerStats

    void Start()
    {
        // ��ȡPlayerStats���
        stats = GetComponent<PlayerStats>();
    }


    public float GetAttackCooldown()
    {
        return 1f / stats.AttackSpeed;  // ����AttackSpeed������ȴʱ��
    }
    public void Shoot()
    {
        // �����ӵ�
        GameObject bullet = BulletManager.Instance.GetBullet(currentBulletType, firePoint.position, firePoint.rotation);

        // �����ӵ�����
        if (bullet.TryGetComponent<Projectile>(out var projectile))
        {
            // ������ƫ��
            Vector2 deviation = Random.insideUnitCircle * stats.ShotSpread;

            // ��ʼ���ӵ�
            projectile.Initialize(
                transform,             // ������
                stats.AttackPower,         // �˺�
                (firePoint.up + (Vector3)deviation).normalized,  // �������
                stats.ShotSpeed       // ����ٶ�
            );
        }
    }
}
