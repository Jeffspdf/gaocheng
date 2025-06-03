using UnityEngine;

public class SplittingProjectile : Projectile
{
    [Header("��������")]
    public GameObject subBulletPrefab;
    public int splitCount = 7; // ��Ϊ4��
    public float splitAngleRange = 360f;
    public float subBulletSpeed = 3f;

    // �Ƴ���ʱ���߼�����Ϊ���������ڽ���ʱ����
    public override void Initialize(Transform shooter, float dmg, Vector2 dir, float speedMultiplier = 1f)
    {
        base.Initialize(shooter, dmg, dir, speedMultiplier);
        CancelInvoke(nameof(DestroySelf)); // ȡ���Զ�����
        Invoke(nameof(Split), lifetime - 0.1f); // ����������ǰ����
    }

    void Split()
    {
        float angleStep = splitAngleRange / splitCount;
        for (int i = 0; i < splitCount; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            GameObject bullet = Instantiate(subBulletPrefab, transform.position, Quaternion.identity);
            Projectile sub = bullet.GetComponent<Projectile>();
            sub.Initialize(transform, damage * 0.5f, dir.normalized, subBulletSpeed);
        }
        Destroy(gameObject);
    }
}