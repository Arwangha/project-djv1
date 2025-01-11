using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _level;
    public static int UpgradePoints;
    public static List<int> CompletedLevels = new List<int>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(int level)
    {
        if (!CompletedLevels.Contains(level)) return;
        SceneManager.LoadScene(level);
        _level = level;
    }
}
