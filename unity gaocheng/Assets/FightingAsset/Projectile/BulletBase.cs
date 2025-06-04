using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public BulletType type;
    private Vector2 direction;
    private float speed;
    private float lifetime;

    // �������õ�״̬
    private Vector2 originalDirection;
    private float originalSpeed;
    private float originalLifetime;
    private Vector3 originalScale;
    private Color originalColor;

    void Start()
    {
        // �����ʼ״̬
        originalDirection = direction;
        originalSpeed = speed;
        originalLifetime = lifetime;
        originalScale = transform.localScale;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) originalColor = renderer.color;
    }

    // �ⲿ���������ӵ�����
    public void Initialize(Vector2 dir, float spd, float life)
    {
        direction = dir;
        speed = spd;
        lifetime = life;
    }

    // �����ӵ�����ʼ״̬
    public void ResetBullet()
    {
        direction = originalDirection;
        speed = originalSpeed;
        lifetime = originalLifetime;
        transform.localScale = originalScale;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.color = originalColor;

        // ���ö�ʱ��
        CancelInvoke(nameof(Deactivate));
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        BulletManager.Instance.ReturnToPool(gameObject, type);
    }
}