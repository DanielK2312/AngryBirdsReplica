using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1; // Look up why this is static
    private Enemy[] _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                return; // If any enemies are alive, we want to go back and restart 
            }
        }

        Debug.Log("You Killed all Enemies");

        _nextLevelIndex++; // One all enemies are killed, next level index increases by one 
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
