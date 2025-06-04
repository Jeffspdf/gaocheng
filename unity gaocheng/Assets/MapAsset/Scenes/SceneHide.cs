using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHider
{
    public static void SetSceneActive(string sceneName, bool active)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (!scene.isLoaded)
        {
            Debug.LogError($"���� {sceneName} δ����");
            return;
        }

        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (var rootObj in rootObjects)
        {
            rootObj.SetActive(active);
        }
    }
}
