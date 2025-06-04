using UnityEngine;
using System.Collections;
public abstract class Enemy : Entity
{
    [Header("���˻�������")]
    [SerializeField] protected EnemyData data;          // ScriptableObject����
    [SerializeField] protected LayerMask playerLayer;   // ��Ҳ㼶
    protected Transform player;                         // �������
    protected EnemyState currentState;                  // ��ǰAI״̬
    protected BattleNode battleNode;                   // ��Ϊprotected�������������

    public virtual void SetBattleNode(BattleNode node)
    {
        battleNode = node;
    }

    protected override void Awake()
    {
        base.Awake();

        if (data != null)
        {
            maxHP = data.baseHP;
            moveSpeed = data.moveSpeed;
            attackPower = data.attackDamage;
            currentHP = maxHP;
        }
    }

    protected override void Start()
    {
        base.Start();
        if (data != null) LoadFromData(data);
    }

    protected override void Update()
    {
        if (isDead) return;
        UpdateAIState();
    }

    // AI״̬�������߼�
    protected abstract void UpdateAIState();

    protected override void Die()
    {
        base.Die();
        StopAllCoroutines();
    }
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