using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public enum LoadType { ByName, ByIndex }
    
    [Header("Scene Loading Settings")]
    public LoadType loadType = LoadType.ByName;

    [Tooltip("Numele scenei (dacă ai ales 'ByName')")]
    public string sceneName;

    [Tooltip("Indexul scenei (dacă ai ales 'ByIndex')")]
    public int sceneIndex;

    public void LoadScene()
    {
        switch (loadType)
        {
            case LoadType.ByName:
                if (!string.IsNullOrEmpty(sceneName))
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Debug.LogWarning("Scene name is empty!");
                }
                break;
            case LoadType.ByIndex:
                SceneManager.LoadScene(sceneIndex);
                break;
        }
    }
}
