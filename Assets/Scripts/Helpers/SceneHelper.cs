using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public static int MainSceneIndex() => _getIndex("main");

    public static int MenuSceneIndex() => _getIndex("menu");

    private static int _getIndex(string sceneName) => SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/{sceneName}.unity");
}
