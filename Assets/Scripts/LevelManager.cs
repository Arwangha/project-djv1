using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private UIHealthBar remainingTime;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float roundDuration;
    private float _timer;

    public bool canAct;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        _timer = roundDuration;
        StartCoroutine(LevelBegin());
    }

    IEnumerator LevelBegin()
    {
        remainingTime.gameObject.SetActive(false);
        scoreText.text = "";
        infoText.text = "Prêt ?";
        Debug.Log("here");
        backgroundImage.enabled = true;
        canAct = false;
        yield return new WaitForSeconds(1f);
        infoText.text = "3";
        yield return new WaitForSeconds(1f);
        infoText.text = "2";
        yield return new WaitForSeconds(1f);
        infoText.text = "1";
        yield return new WaitForSeconds(1f);
        infoText.text = "C'est parti !";
        yield return new WaitForSeconds(1f);
        infoText.text = "";
        backgroundImage.enabled = false;
        canAct = true;
        scoreText.text = "Score : 0";
        remainingTime.gameObject.SetActive(true);
    }
    

    IEnumerator LevelEnd()
    {
        remainingTime.gameObject.SetActive(false);
        backgroundImage.enabled = true;
        canAct = false;
        infoText.text = "Temps écoulé ! \n" + scoreText.text;
        scoreText.text = "";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (canAct)
        {
            _timer -= Time.deltaTime;
            float remainingTimeFraction = _timer / roundDuration;
            remainingTime.SetVal(remainingTimeFraction);
            if (_timer <= 0)
            {
                StartCoroutine(LevelEnd());
            }
        }
    }
}
