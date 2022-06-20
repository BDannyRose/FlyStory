using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private bool currentScoreAdded = false;

    private List<Transform> highscoreEntryTransformList;

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    // класс нужен, чтобы записывать List в формат Json, сам по себе список записать нельзя
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
        public Highscores()
        {
            highscoreEntryList = new List<HighscoreEntry>();
        }
    }

    private void OnEnable()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        if (!currentScoreAdded)
        {
            AddHighscoreEntry(ScoreManager.score, PlayerPrefs.GetString("playerName"));
            currentScoreAdded = true;
        }
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }


    private void AddHighscoreEntry(int score, string name)
    {
        // создаём новую запись
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        // загружаем существующую таблицу
        Highscores highscores;
        string jsonString = PlayerPrefs.GetString("highscoreTable");

        if (jsonString == "")
        {
            Debug.Log("json is empty");
            highscores = new Highscores();
        }
        else
        {
            Debug.Log("json: " + jsonString);
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        // добавляем новую запись
        highscores.highscoreEntryList.Add(highscoreEntry);

        // сохраняем таблицу с новой записью
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 35f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }
}
