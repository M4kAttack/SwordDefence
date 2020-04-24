using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGameManager : MonoBehaviour
{
    private TextMeshPro scoreText;
    private TextMeshPro headShootComboText; 
    private TextMeshPro staticHeadShootComboText;
    public int Score { get; set; } = 0;
    private float originalScoreFontSize;
    private float originalComboFontSize;
    public int headShootCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        var BillBoard = GameObject.FindGameObjectWithTag("BillBoard");
        scoreText = BillBoard.transform.Find("ScoreText").GetComponent<TextMeshPro>();
        headShootComboText = BillBoard.transform.Find("ComboText").GetComponent<TextMeshPro>(); 
        staticHeadShootComboText = BillBoard.transform.Find("StaticComboText").GetComponent<TextMeshPro>();
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
