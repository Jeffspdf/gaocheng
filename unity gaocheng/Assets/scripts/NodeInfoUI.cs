using UnityEngine;
using UnityEngine.UI;

public class NodeInfoUI : MonoBehaviour
{
    public Text nodeTypeText;
    public Text nodeNameText;
    public Text nodeIdText;
    public Text nodeNidText;
    public Text nodeDescriptionText;
    public Button confirmButton;
    public Button cancelButton;

    private Pointer pointer; // ����ָ�����

    void Start()
    {
        // �������
        gameObject.SetActive(false);

        // ��ȡ Pointer �ű�
        pointer = FindObjectOfType<Pointer>();
    }

    public void ShowPanel(Node targetNode)
    {
        // �����������
        nodeTypeText.text = $"����: {targetNode.nodeName}";
        nodeNameText.text = $"����: {targetNode.nodeName}";
        nodeIdText.text = $"ID: {targetNode.Id}";
        nodeNidText.text = $"���(Nid): {targetNode.Nid}";
        nodeDescriptionText.text = $"����: {targetNode.nodeDescription}";

        // ����Ƿ���ָ�뵱ǰָ��Ľڵ�����
        Node currentNode = pointer.GetCurrentNode();
        bool isConnected = currentNode != null && currentNode.IsNeighbor(targetNode);

        // ��ʾ������ȷ�ϰ�ť
        confirmButton.gameObject.SetActive(isConnected);

        // ��ʾ���
        gameObject.SetActive(true);

        // ��Ӱ�ť�¼�
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            Debug.Log("ȷ�ϰ�ť�����");
            pointer.MoveTo(targetNode); // �ƶ�ָ�뵽Ŀ��ڵ�
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
