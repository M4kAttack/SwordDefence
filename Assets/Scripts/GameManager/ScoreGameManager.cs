using TMPro;
using UnityEngine;

public class ScoreGameManager : MonoBehaviour
{
    private TextMeshPro scoreText;
    private TextMeshPro headShootComboText; 
    private TextMeshPro staticHeadShootComboText;
    private GameObject billboard;
    public int Score { get; set; } = 0;
    private float originalScoreFontSize;
    private float originalComboFontSize;
    public int headShootCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (billboard == null)
        {
            billboard = GameObject.FindGameObjectWithTag("BillBoard");
            NullCheck.CheckIfNull(billboard, typeof(GameObject), this, "BillBoard");
        }    
       if (scoreText == null)
        {
            scoreText = billboard.transform.Find("ScoreText").GetComponent<TextMeshPro>();
             NullCheck.CheckIfNull(scoreText, typeof(TextMeshPro), this, "ScoreText");
        }
       if(headShootComboText == null)
        {
            headShootComboText = billboard.transform.Find("ComboText").GetComponent<TextMeshPro>();
            NullCheck.CheckIfNull(headShootComboText, typeof(TextMeshPro), this, "ComboText");
        }
       if(staticHeadShootComboText == null)
        {
            staticHeadShootComboText = billboard.transform.Find("StaticComboText").GetComponent<TextMeshPro>();
            NullCheck.CheckIfNull(staticHeadShootComboText, typeof(TextMeshPro), this, "StaticComboText");
        }
        staticHeadShootComboText.enabled = false;
        headShootComboText.enabled = false;
        originalScoreFontSize = scoreText.fontSize;
        originalComboFontSize = headShootComboText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.ToString();

        ReturnFontSize();
    }

    private void ReturnFontSize()
    {
        if (scoreText.fontSize > originalScoreFontSize)
        {
            scoreText.fontSize -= 0.2f;
        }
        if (headShootComboText.fontSize > originalComboFontSize)
        {
            headShootComboText.fontSize -= 0.4f;
        }
    }

    public void UpdateScore(int score, bool isHeadShot)
    {
 
         
        if (isHeadShot)
        {
            headShootCounter++;
            score = score * headShootCounter;
            staticHeadShootComboText.enabled = true;
            headShootComboText.enabled = true;
            headShootComboText.fontSize = originalComboFontSize * 1.5f;
            headShootComboText.text = "X" + headShootCounter.ToString();
        } else
        {
            staticHeadShootComboText.enabled = false;
            headShootComboText.enabled = false;
            headShootCounter = 0;
        }
        Score += score;
        scoreText.fontSize = originalScoreFontSize * 1.5f;
    }

}
