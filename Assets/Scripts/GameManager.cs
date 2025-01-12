using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _level;
    public static int UpgradePoints;
    public static List<int> CompletedLevels;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            CompletedLevels ??= new List<int>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(int level)
    {
        if (!CompletedLevels.Contains(level - 1) && level != 1) return;
        SceneManager.LoadScene(level);
        _level = level;
    }
}
