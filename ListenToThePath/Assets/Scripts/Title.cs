using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class Title : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("skip", Skip);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();


    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(sceneName: "Introduction");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    void Skip()
    {

        SceneManager.LoadScene(sceneName: "Introduction");
    }
}
