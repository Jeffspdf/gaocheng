using System.Collections;
using UnityEngine;

public class BossEnemy : Enemy
{
    private Animator animator;
    [Header("�׶�����")]
    [SerializeField] private float phaseTransitionThreshold = 0.5f;  // 50%Ѫ��ת���׶�
    [SerializeField] private float phaseTransitionTime = 0.11f;

    [Header("ͨ���ƶ�����")]
    [SerializeField] private float patrolMinX = -3f;
    [SerializeField] private float patrolMaxX = 3f;
    [SerializeField] private float patrolSpeed = 2f;
    private bool movingRight = true;
    [Header("��һ�׶Σ���״��Ļ")]
    [SerializeField] private int columnProjectileCount = 5;   // ��Ļ����
    [SerializeField] private float columnSpacing = 0.5f;      // �ӵ����
    [SerializeField] private float columnAttackCooldown = 3f;
    private float columnAttackTimer = 0f;

    [Header("�ڶ��׶Σ����ε�Ļ")]
    [SerializeField] private int fanProjectileCount = 7;
    [SerializeField] private float fanSpreadAngle = 60f;
    private float phase2AttackTimer = 0f;
    private float phase2AttackCooldown = 2.5f; // ��������ε�Ļ������ȴʱ��
    private bool useLaserNext = true;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserSpawnPoint;
    private bool isPhase2 = false;
    private bool isTransitioning = false;
    [Header("�ܻ�����")]
    [SerializeField] private Color hurtColor = Color.red;
    [SerializeField] private float hurtDuration = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    protected override void Start()
    {
        base.Start();
        LoadFromData(data); 
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected override void UpdateAIState()
    {
        if (isTransitioning) return;

        if (!isPhase2 && currentHP <= maxHP * phaseTransitionThreshold)
        {
            StartCoroutine(TransitionToNextPhase());
            return;
        }

        HorizontalPatrol();

        if (isPhase2)
        {
            Phase2Behavior();
        }
        else
        {
            Phase1Behavior();
        }
        Debug.Log($"��ǰ�׶�: {(isPhase2 ? "Phase2" : "Phase1")} Ѫ��: {currentHP}/{maxHP}");
    }


    private IEnumerator TransitionToNextPhase()
    {
        isTransitioning = true;
        animator.SetTrigger("DoTransforming");
        yield return new WaitForSeconds(phaseTransitionTime);
        isPhase2 = true;
        isTransitioning = false;
    }

    private void HorizontalPatrol()
    {
        Vector3 pos = transform.position;
        pos.x += (movingRight ? 1f : -1f) * patrolSpeed * Time.deltaTime;

        if (pos.x >= patrolMaxX)
        {
            pos.x = patrolMaxX;
            movingRight = false;
        }
        else if (pos.x <= patrolMinX)
        {
            pos.x = patrolMinX;
            movingRight = true;
        }

        transform.position = pos;
    }

    private void Phase1Behavior()
    {
        // ȫ����״��Ļ����
        columnAttackTimer += Time.deltaTime;
        if (columnAttackTimer >= columnAttackCooldown)
        {
            columnAttackTimer = 0f;
            FireColumn();
        }
    }

    private void Phase2Behavior()
    {
        phase2AttackTimer += Time.deltaTime;

        if (phase2AttackTimer >= phase2AttackCooldown)
        {
            phase2AttackTimer = 0f;

            if (useLaserNext)
            {
                FireLaser();
            }
            else
            {
                FireFan();
            }

            useLaserNext = !useLaserNext; // ÿ���л���������
        }
    }

    private void FireColumn()
    {
        // ������ʼλ��
        Vector2 startPos = transform.position; // �� Boss ͬһˮƽ��


        for (int i = 0; i < columnProjectileCount; i++)
        {
            Vector2 spawnPos = startPos + Vector2.right * (i * columnSpacing - columnSpacing * (columnProjectileCount - 1) / 2f);

            GameObject bulletObj = BulletManager.Instance.GetBullet(
                BulletType.Enemy,
                spawnPos,
                Quaternion.Euler(0, 0, 270f)
            );
            if (bulletObj.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(transform, data.attackDamage, Vector2.down);
            }
            else
            {
                Debug.LogError("�ӵ�Ԥ������û�� Projectile �����");
            }
        }
    }

    private void FireFan()
    {
        if (animator != null)
            animator.SetTrigger("DoAttack1");

        float startAngle = 270f - fanSpreadAngle / 2f;
        float delta = fanSpreadAngle / (fanProjectileCount - 1);
        for (int i = 0; i < fanProjectileCount; i++)
        {
            float angle = startAngle + delta * i;
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            ShootProjectile(dir);
        }
    }

    private void FireLaser()
    {
        if (animator != null)
            animator.SetTrigger("DoAttack2");

        if (laserPrefab != null)
        {
            Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        }
    }


    private void OnDrawGizmosSelected()//����ʹ��
    {
        // �����ƶ���Χ
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            new Vector3(patrolMinX, transform.position.y - 1f),
            new Vector3(patrolMaxX, transform.position.y - 1f)
        );

        // �������ε�Ļ�������򣨵�һ�׶Σ�
        if (!isPhase2)
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * 2f, fanSpreadAngle);
        }

        // ������״��Ļ�������򣨵ڶ��׶Σ�
        if (isPhase2)
        {
            Gizmos.DrawWireCube(
                transform.position + Vector3.up * 2f,
                new Vector3(columnSpacing * 4, 0.5f)
            );
        }
    }
    protected override void Die()
    {
        base.Die();
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        
        Destroy(gameObject, 3f); // �ȴ����������������
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HurtEffect());
    }
    IEnumerator HurtEffect()
    {
        spriteRenderer.color = hurtColor;
        yield return new WaitForSeconds(hurtDuration);
        spriteRenderer.color = originalColor;
    }
}

