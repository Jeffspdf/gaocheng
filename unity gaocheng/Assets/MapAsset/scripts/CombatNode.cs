using UnityEngine;
public class CombatNode : BattleNode
{

    // ָ��ս����������
    protected override string BattleSceneName => "BattleScene";
    public void StartCombat()
    {
        StartBattle();
    }
    public void EndCombat()
    {
        EndBattle();
    }
}
