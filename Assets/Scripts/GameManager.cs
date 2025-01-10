using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
