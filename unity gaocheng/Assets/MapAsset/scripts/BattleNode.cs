using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BattleNode : Node
{
    [Header("ս����������")]
    public bool isBattleActive = false;
    public Enemy enemy;

    protected abstract string BattleSceneName { get; }
    private GameObject playerInstance;

    public virtual void StartBattle()
    {
        isBattleActive = true;

        // �����������
        playerInstance = FindObjectOfType<Player>()?.gameObject;

        // ������ң��������٣���������״̬
        if (playerInstance != null)
        {
            playerInstance.SetActive(false);
        }

        SceneManager.LoadScene(BattleSceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnBattleSceneLoaded;
        SceneHider.SetSceneActive("MapScene", false);
        Debug.Log($"{BattleSceneName}ս����ʼ��");
        Invoke("EndBattle", 10f); // �����ã�10������ս��
    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == BattleSceneName)
        {
            SceneManager.sceneLoaded -= OnBattleSceneLoaded;
            SceneManager.SetActiveScene(scene);

            // �ض�λ������ң�����У�
            BattleSceneController controller = FindObjectOfType<BattleSceneController>();
            Player player = FindObjectOfType<Player>();

            if (player != null && controller != null)
            {
                player.transform.position = controller.playerSpawn.position;
            }
        }
    }

    public virtual void EndBattle()
    {
        isBattleActive = false;
        if (BulletManager.Instance != null)
        {
            BulletManager.Instance.ResetAllBullets();
        }
        // �ָ���ң����֮ǰ�����ˣ�
        if (playerInstance != null)
        {
            playerInstance.SetActive(true);
            playerInstance = null;
        }

        // ж��ս������
        SceneManager.UnloadSceneAsync(BattleSceneName);

        // ȷ����ͼ���������¼���
        Scene mapScene = SceneManager.GetSceneByName("MapScene");
        if (mapScene.isLoaded)
        {
            SceneManager.SetActiveScene(mapScene);
        }
        else
        {
            Debug.LogWarning("��ͼ����δ����");
        }

        SceneHider.SetSceneActive("MapScene", true);
        Debug.Log($"{BattleSceneName}ս������");

        // ս��ʤ������߼�
        BattleCompleted();
    }

    protected virtual void BattleCompleted()
    {
        Debug.Log($"ս��ʤ����");
    }
}