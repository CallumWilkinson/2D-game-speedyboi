using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Support
{
    /// <summary>
    /// Base fixture that loads a tiny bootstrap scene before each test and
    /// returns to a clean state after each test.
    /// </summary>
    public abstract class PlayModeBootstrap
    {
        protected const string BootstrapSceneName = "TestBootstrap";

        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            yield return SceneManager.LoadSceneAsync(BootstrapSceneName, LoadSceneMode.Single);
            yield return null; // Let Awake/Start finish
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            {
                if (go != null && go.scene.name != "DontDestroyOnLoad")
                {
                    Object.DestroyImmediate(go);
                }
            }
        }

        [UnityTearDown]
        public IEnumerator UnityTearDown()
        {
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().path, LoadSceneMode.Single);
            yield return null;
        }
    }
}
