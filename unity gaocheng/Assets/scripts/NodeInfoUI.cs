using UnityEngine;
using UnityEngine.UI;

public class NodeInfoUI : MonoBehaviour
{
    public Text nodeTypeText;
    public Text nodeNameText;
    public Text nodeIdText;
    public Text nodeDescriptionText;
    public Button confirmButton;
    public Button cancelButton;
    public Text nodeNidText;

    public void ShowPanel(string nodeType, string nodeName, int nodeId, int nodeNid, string nodeDescription)
    {
        // �����������
        nodeTypeText.text = $"����: {nodeType}";
        nodeNameText.text = $"����: {nodeName}";
        nodeIdText.text = $"ID: {nodeId}";
        nodeNidText.text = $"���(Nid): {nodeNid}";
        nodeDescriptionText.text = $"����: {nodeDescription}";

        // ��ʾ���
        gameObject.SetActive(true);

        // ��Ӱ�ť�¼�
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            Debug.Log("ȷ�ϰ�ť�����");
            gameObject.SetActive(false); // �������
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            Debug.Log("ȡ����ť�����");
            gameObject.SetActive(false); // �������
        });
    }
}
