using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public HealthBar healthBar;
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public static HighscoreTable highscoreTable;

    public SongManager songManager;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro Multiplier;

    public GameObject resultsLossScreen;

    public GameObject ShadowPlayer;

    public GameObject resultsWonScreen;

    public GameObject hitPlaceText;

    public GameObject hitPlace;
    public AudioSource audioSource;
    public GameObject Jogo;
    public TMPro.TextMeshPro percentHitText,
        normalsText,
        goodsText,
        perfectsText,
        missesText,
        rankText,
        finalScoreText,
        percentHitTextWon,
        normalsTextWon,
        goodsTextWon,
        perfectsTextWon,
        missesTextWon,
        rankTextWon,
        finalScoreTextWon,
        countdownDisplay;

    static int comboScore;
    static int currentScore;
    static int scorePerNote = 100;
    static int scorePerGoodNote = 200;
    public int maxHealth = 10;
    public int currentHealth;
    public bool isCounting;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float totalNotes;
    public float missedHits;
    static int scorePerPerfectNote = 300;
    public int countdownTime;
    static int currentMultiplier = 1;
    static int multiplierTracker;
    static int[] multiplierThresholds = { 4, 8, 16 };

    void Start()
    {
        AudioListener.pause = true;
        StartCoroutine(countdownToStart());
        AudioListener.pause = true;
        Instance = this;
        comboScore = 0;
        currentMultiplier = 1;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Invoke(nameof(End), audioSource.clip.length);
        AudioListener.pause = true;
        normalHits = 0;
        goodHits = 0;
        perfectHits = 0;
        totalNotes = 0;
        missedHits = 0;
        comboScore = 0;
        currentScore = 0;
        isCounting = false;
    }

    public static void Hit()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            if (currentMultiplier - 1 < multiplierThresholds.Length)
            {
                multiplierTracker++;

                if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    currentMultiplier++;
                }
            }

            //currentScore += scorePerNote * currentMultiplier;
            comboScore += 1;
            Instance.hitSFX.Play();
        }
    }

    public static void NormalHit()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            currentScore += scorePerNote * currentMultiplier;
            Hit();
            Instance.normalHits++;
            Instance.totalNotes++;
        }
    }

    public static void GoodHit()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            currentScore += scorePerGoodNote * currentMultiplier;
            Hit();
            Instance.goodHits++;
            Instance.totalNotes++;
        }
    }

    public static void PerfectHit()
    {
        if (PauseMenu.gameIsPaused == false)
        {
            currentScore += scorePerPerfectNote * currentMultiplier;
            Hit();
            Instance.perfectHits++;
            Instance.totalNotes++;
        }
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public static void Miss()
    {
        comboScore = 0;
        Instance.missSFX.Play();
        currentMultiplier = 1;
        multiplierTracker = 0;
        Instance.missedHits++;
        Instance.totalNotes++;
        Instance.takeDamage(1);
    }

    public void End()
    {
        if (currentHealth > 0)
        {
            isCounting = true;
            resultsWonScreen.SetActive(true);
            Jogo.SetActive(false);
            ShadowPlayer.SetActive(false);
            hitPlace.SetActive(false);
            normalsTextWon.text = normalHits.ToString();
            goodsTextWon.text = goodHits.ToString();
            perfectsTextWon.text = perfectHits.ToString();
            missesTextWon.text = missedHits.ToString();

            float totalHit = normalHits + perfectHits + goodHits;
            float percentHit = (totalHit / totalNotes) * 100f;

            percentHitTextWon.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";

            if (percentHit > 40)
            {
                rankVal = "F";
                if (percentHit > 55)
                {
                    rankVal = "C";
                    if (percentHit > 70)
                    {
                        rankVal = "B";
                        if (percentHit > 85)
                        {
                            rankVal = "A";
                            if (percentHit > 95)
                            {
                                rankVal = "S";
                            }
                        }
                    }
                }
            }

            rankTextWon.text = rankVal;

            finalScoreTextWon.text = currentScore.ToString();
            int scorezinho = currentScore;
            HighscoreTable.AddHighscoreEntry(scorezinho, rankVal);
        }
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            isCounting = true;
            hitPlace.SetActive(false);
            Jogo.SetActive(false);
            ShadowPlayer.SetActive(false);
            resultsLossScreen.SetActive(true);
            normalsText.text = normalHits.ToString();
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = missedHits.ToString();

            float totalHit = normalHits + perfectHits + goodHits;
            float percentHit = (totalHit / totalNotes) * 100f;

            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";

            if (percentHit > 40)
            {
                rankVal = "F";
                if (percentHit > 55)
                {
                    rankVal = "C";
                    if (percentHit > 70)
                    {
                        rankVal = "B";
                        if (percentHit > 85)
                        {
                            rankVal = "A";
                            if (percentHit > 95)
                            {
                                rankVal = "S";
                            }
                        }
                    }
                }
            }

            rankText.text = rankVal;

            finalScoreText.text = currentScore.ToString();
        }

        scoreText.text = currentMultiplier.ToString();
        Multiplier.text = currentScore.ToString();
    }

    IEnumerator countdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            isCounting = true;
            AudioListener.pause = true;
            countdownTime--;
        }
        countdownDisplay.text = "GO!";
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isCounting = false;
        hitPlace.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        countdownDisplay.gameObject.SetActive(false);
        hitPlaceText.SetActive(false);
        PauseMenu.gameIsPaused = false;
    }
}
