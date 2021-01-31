using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class Ends : MonoBehaviour
{
    public AudioSource voi;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("skip", Skip);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        voi.Play();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            voi.Stop();
            SceneManager.LoadScene(sceneName: "TitleCard");
        }

        if (!(voi.isPlaying))
        {
            SceneManager.LoadScene(sceneName: "TitleCard");
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
        voi.Stop();
        SceneManager.LoadScene(sceneName: "TitleCard");
    }
}
