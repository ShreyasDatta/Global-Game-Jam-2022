using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBeatScroller;
    public BeatScroll2 theSecondBeatScroller;
    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public Text scoreText;
    public Text multiText;
    public Text multiText2;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultScreen;
    public Text percentHitText, normalText, goodText, perfectText, missedText, rankText, finalScoreText;


    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThreshold;

    public int currentMultiplier2;
    public int multiplierTracker2;
    public int[] multiplierThreshold2;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: " + 0;
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying){
            if(Input.anyKeyDown){
                startPlaying = true;
                theBeatScroller.hasStarted = true;
                resultScreen.SetActive(false);
                theMusic.Play();
                Timer.instance.Start();
                theSecondBeatScroller.hasStarted = true;
            }
            else if(!theMusic.isPlaying && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);

                    normalText.text = "" + normalHits;
                    goodText.text = goodHits.ToString();
                    perfectText.text = perfectHits.ToString();
                    missedText.text = "" + missedHits;

                    float totalHit = normalHits + goodHits + perfectHits;
                    float percentHit = (totalHit/totalNotes) * 100f;

                    percentHitText.text = percentHit.ToString("F1") + "%"; 
                }
            
        }

    }
    public void NoteHit()
    {
        Debug.Log("Hit a Note");
        if(currentMultiplier - 1 < multiplierThreshold.Length)
        {
            multiplierTracker++;
            if(multiplierThreshold[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        if(currentMultiplier2 - 1 < multiplierThreshold2.Length)
        {
            multiplierTracker2++;
            if(multiplierThreshold2[currentMultiplier - 1] <= multiplierTracker2)
            {
                multiplierTracker2 = 0;
                currentMultiplier2++;
            }
        }
        multiText2.text = "Multiplier: x" + currentMultiplier2;

        /*currentScore += scorePerNote * currentMultiplier;*/
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * (currentMultiplier + currentMultiplier2);
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote* (currentMultiplier + currentMultiplier2);
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * (currentMultiplier + currentMultiplier2);
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed(){
        Debug.Log("Missed Note");
        
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        currentMultiplier2 = 1;
        multiplierTracker2 = 0;
        
        multiText2.text = "Multiplier: x" + currentMultiplier2;

        missedHits++;
    }

    
}
