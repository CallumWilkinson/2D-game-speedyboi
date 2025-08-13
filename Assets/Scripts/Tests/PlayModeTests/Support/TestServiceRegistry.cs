using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// A placeholder for registering or accessing test-only services in the bootstrap scene.
    /// </summary>
public class TestServiceRegistry : MonoBehaviour {


        // Example placeholders – expand as needed for your test helpers
        public GameObject MockPlayerPrefab;
        public MonoBehaviour[] GlobalMocks;

        // You can also add static access patterns if needed
        public static TestServiceRegistry Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
}
