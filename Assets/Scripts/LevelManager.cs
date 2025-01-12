using System.Collections;
using System.Linq;
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
    public int releasedFraudeurs;
    public int caughtFraudeurs;
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
        scoreText.text = "Score : 0 " + "\nPoints d'amélioration : " + GameManager.UpgradePoints.ToString();
        remainingTime.gameObject.SetActive(true);
    }
    

    IEnumerator LevelEnd()
    {
        remainingTime.gameObject.SetActive(false);
        backgroundImage.enabled = true;
        canAct = false;
        float fractionResult = 100f;
        if(releasedFraudeurs > 0) fractionResult = caughtFraudeurs / (float)releasedFraudeurs * 100f;
        infoText.text = "Temps écoulé ! \n" + scoreText.text + "\n Soit : " + fractionResult + "%";
        if (fractionResult > 70)
        {
            GameManager.UpgradePoints++;
            yield return null;
            yield return null;
            infoText.text = "Temps écoulé ! \n" + scoreText.text + "\n Soit : " + fractionResult + "%" + "\n Bien joué ! + 1 point d'amélioration";
        }
        scoreText.text = "";
        yield return new WaitForSeconds(3f);
        GameManager.CompletedLevels.Add(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log(GameManager.CompletedLevels.Count);
        //Debug.Log(GameManager.CompletedLevels.ElementAt(1));
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
