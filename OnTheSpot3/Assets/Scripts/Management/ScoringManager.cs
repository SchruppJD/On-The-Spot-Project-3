using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    public GameObject canvas;

    private List<Text> scores;
    List<Material> playerColors;

    private bool setup = false;

    // Start is called before the first frame update
    void Start()
    {
        scores = new List<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // only update after text has been setup
        if (setup)
        {
            updateScoreDisplay();
        }
    }

    public void updateScoreDisplay()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].text = "Player " + (i + 1) + " : " + FindObjectOfType<roomManager>().playerPoints[i];
        }
    }

    public void CreateScoreDisplay()
    {
        playerColors = new List<Material>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerColors.Add(go.GetComponent<Renderer>().material);
        }
        for (int i = 0; i < playerColors.Count; i++)
        {
            GameObject textHolder = new GameObject("scoreDisplay " + (i + 1));
            textHolder.transform.SetParent(canvas.transform);

            Text scoreText = textHolder.AddComponent<Text>();
            scoreText.color = playerColors[i].GetColor("_Color");
            scoreText.text = "Player " + (i + 1) + " : 0";
            scoreText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            scoreText.fontSize = 22;
            scoreText.alignment = TextAnchor.UpperLeft;
            scoreText.rectTransform.anchoredPosition = new Vector3(200, -(i * 35) - 50, 0);
            scoreText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);
            scoreText.rectTransform.anchorMin = new Vector2(0, 1);
            scoreText.rectTransform.anchorMax = new Vector2(0, 1);
            scores.Add(scoreText);
        }

        setup = true;
    }

    public void VictoryText(int index)
    {
        GameObject textHolder = new GameObject("victoryText");
        textHolder.transform.SetParent(canvas.transform);

        Text victoryText = textHolder.AddComponent<Text>();
        victoryText.color = playerColors[index].GetColor("_Color");
        victoryText.alignment = TextAnchor.MiddleCenter;
        victoryText.rectTransform.anchoredPosition = new Vector3(0, 200);
        victoryText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        victoryText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        victoryText.text = "Player " + (index + 1) + " wins!";
        victoryText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        victoryText.fontSize = 40;
        victoryText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);

        GameObject subTextHolder = new GameObject("victoryText");
        subTextHolder.transform.SetParent(canvas.transform);

        Text restartText = subTextHolder.AddComponent<Text>();
        restartText.text = "Press 'T' to restart";
        restartText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        restartText.fontSize = 30;
        restartText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
        restartText.alignment = TextAnchor.MiddleCenter;
        restartText.rectTransform.anchoredPosition = new Vector3(0, 150);
        restartText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        restartText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }
}
