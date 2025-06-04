using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("��������")]
    public float baseHP = 100f;
    public float moveSpeed = 2f;
    public float attackDamage = 10f;

    [Header("AI��Ϊ����")]
    public float patrolSpeed = 1f;     // Ѳ���ٶȻ���ٶ�
    public float attackInterval = 1f;  // �������
}
