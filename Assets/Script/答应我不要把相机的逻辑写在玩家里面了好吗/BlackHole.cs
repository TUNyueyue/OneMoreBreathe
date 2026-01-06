using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    [SerializeField] string loadSceneName;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<BaseCraft>() != null)
        {
            SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
        }
    }
}
