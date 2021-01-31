using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class SceneTransition : MonoBehaviour
{
    public AudioSource Instructions;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("skip", Skip);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        Instructions.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            Instructions.Stop();
            SceneManager.LoadScene(sceneName: "Mono");
        }

        if (!(Instructions.isPlaying))
        {
            SceneManager.LoadScene(sceneName: "Mono");
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
        Instructions.Stop();
        SceneManager.LoadScene(sceneName: "Mono");
    }
}
