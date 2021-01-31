using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class MonoScene : MonoBehaviour
{
    public AudioSource Monos;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("skip", Skip);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        Monos.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Monos.Stop();
            SceneManager.LoadScene(sceneName: "Game");
        }

        if (!(Monos.isPlaying))
        {
            SceneManager.LoadScene(sceneName: "Game");
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
        Monos.Stop();
        SceneManager.LoadScene(sceneName: "Game");
    }
}
