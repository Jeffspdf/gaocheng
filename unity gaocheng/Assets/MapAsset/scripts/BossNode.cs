using UnityEngine;

public class BossNode : BattleNode
{
    //ָ��ս����������
    protected override string BattleSceneName => "BossScene";

    public void StartBossBattle()
    {
        StartBattle();
    }
    public void EndBossBattle()
    {
        EndBattle();
    }

    protected override void BattleCompleted()
    {
        base.BattleCompleted();
        Debug.Log("��ϲ��������˹ص�BOSS��");
    }
}