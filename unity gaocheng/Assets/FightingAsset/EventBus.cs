using System;
using System.Collections.Generic;

public static class EventBus
{
    // �¼��������ֵ䣨�¼����� -> �¼��������б�
    private static Dictionary<Type, List<Action<object>>> eventListeners = new Dictionary<Type, List<Action<object>>>();

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="eventData">�¼�����</param>
    public static void Publish(object eventData)
    {
        Type eventType = eventData.GetType();
        if (eventListeners.ContainsKey(eventType))
        {
            // �������еļ���������������
            foreach (var listener in eventListeners[eventType])
            {
                listener.Invoke(eventData);
            }
        }
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="eventListener">�¼�������</param>
    public static void Subscribe<T>(Action<T> eventListener)
    {
        Type eventType = typeof(T);
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Action<object>>();
        }

        // ��Ӽ�����
        eventListeners[eventType].Add((eventData) => eventListener.Invoke((T)eventData));
    }

    /// <summary>
    /// ȡ�������¼�
    /// </summary>
    /// <param name="eventListener">�¼�������</param>
    public static void Unsubscribe<T>(Action<T> eventListener)
    {
        Type eventType = typeof(T);
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove((eventData) => eventListener.Invoke((T)eventData));
        }
    }
}
