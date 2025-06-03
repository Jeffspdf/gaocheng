using UnityEngine;
using System.Collections;
public abstract class Enemy : Entity
{
    [Header("���˻�������")]
    [SerializeField] protected EnemyData data;          // ScriptableObject����
    [SerializeField] protected LayerMask playerLayer;   // ��Ҳ㼶
    [SerializeField] protected Projectile projectilePrefab; // �ӵ�Ԥ����
    protected Transform player;                         // �������
    protected EnemyState currentState;                  // ��ǰAI״̬
    protected override void Awake()
    {
        // ���ø���� currentHP = maxHP
        base.Awake();

        // �ٰ� Data ���ֵд�������ֶ�
        if (data != null)
        {
            maxHP = data.baseHP;
            moveSpeed = data.moveSpeed;
            attackPower = data.attackDamage;
        }
        // ���� currentHP ͬ�����µ� maxHP
        currentHP = maxHP;

    }
    protected override void Start()
    {
        base.Start();
        LoadFromData(data);
    }

    protected override void Update()
    {
        if (isDead) return;
        UpdateAIState();
    }

    // AI״̬�������߼�
    protected abstract void UpdateAIState();

    // ���䵯Ļ��ͨ�÷���
    protected Projectile ShootProjectile(Vector2 direction)
    {
        GameObject bulletObj = BulletManager.Instance.GetBullet(
          BulletType.Enemy,
          transform.position,
          Quaternion.identity
        );
        if (bulletObj.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Initialize(transform, data.attackDamage, direction);
        }
        return projectile;
    }

}
