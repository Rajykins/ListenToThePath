using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public AudioSource step1, step2, rotate, rightWind, leftWind, bump;
    public AudioSource goalSound;
    public AudioSource Inst;
    public Game game;
    int togg = 1;
    public float oldt;

    private Dictionary<int, string[]> rotations = new Dictionary<int, string[]>();
    private int[] rotation = { 0, 1, 2, 3 };
    int currentRotation = 0;

    int nx, ny, iterated = 0;

    //higher for more iterations of GradualMove
    int max = 500;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        rotations.Add(0, new string[] { "down", "up" });
        rotations.Add(1, new string[] { "left", "right" });
        rotations.Add(2, new string[] { "up", "down" });
        rotations.Add(3, new string[] { "right", "left" });

        actions.Add("forward", Forward);
        actions.Add("left", Left);
        actions.Add("right", Right);
        actions.Add("behind", Behind);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inst.Play();
            oldt = Time.time;
        }
        if(Input.anyKey&&Time.time-oldt>2)
        {
            Inst.Stop();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Forward();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Behind();
        }

        GradualMove(nx, ny);
        updateWind();
    }

    //speech
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        nx = Mathf.RoundToInt(transform.position.x);
        ny = Mathf.RoundToInt(transform.position.y);

        animator.SetInteger("dir", rotation[currentRotation]);
        animator.SetBool("Moving", true);

        MovementAction(rotations[currentRotation][0]);
    }

    private void Left()
    {
        NextRotation(-1);
        animator.SetInteger("dir", rotation[currentRotation]);
        adjustSpread(currentRotation);
        rotate.Play();
    }

    private void Right()
    {
        NextRotation(1);
        animator.SetInteger("dir", rotation[currentRotation]);
        adjustSpread(currentRotation);
        rotate.Play();
    }

    private void Behind()
    {
        nx = Mathf.RoundToInt(transform.position.x);
        ny = Mathf.RoundToInt(transform.position.y);

        animator.SetInteger("dir", rotation[currentRotation]);
        animator.SetBool("Moving", true);

        MovementAction(rotations[currentRotation][1]);
    }

    void MovementAction(string dir)
    {
        if (dir == "left" && !(game.hwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)]))
        {
            nx--;
            if (togg == 1) 
            {
                step1.Play(); 
            }
            else 
            { 
                step2.Play(); 
            }
            togg = togg * -1;   
        }
        else if (dir == "left")
        {
            bump.Play();
        }

        if (dir == "right" && !(game.hwalls[Mathf.RoundToInt(transform.position.x) + 1, Mathf.RoundToInt(transform.position.y)]))
        {
            nx++;
            if (togg == 1) 
            {
                step1.Play(); 
            }
            else 
            { 
                step2.Play();
            }
            togg = togg * -1;
        }
        else if (dir == "right")
        {
            bump.Play();
        }
        if (dir == "up" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) + 1]))
        {
            ny++;
            if (togg == 1) 
            { 
                step1.Play(); 
            }
            else 
            { 
                step2.Play(); 
            }
            togg = togg * -1;
        }
        else if (dir == "up")
        {
            bump.Play();
        }

        if (dir == "down" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)]))
        {
            ny--;
            if (togg == 1) 
            { 
                step1.Play(); 
            }
            else 
            { 
                step2.Play(); 
            }
            togg = togg * -1;
        }
        else if (dir == "down")
        {
            bump.Play();
        }
    }

    void GradualMove(int x, int y)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(nx, ny), Time.deltaTime * 12);
        iterated++;
        if(iterated >= max)
        {
            transform.position = new Vector3(nx, ny);
            animator.SetBool("Moving", false);
            iterated = 0;
        }
    }

    //i = -1 to turn left and 1 to turn right
    void NextRotation(int i)
    {
        if (currentRotation + i > 3)
        {
            currentRotation += i;
            currentRotation -= 4;
        }
        else if (currentRotation + i < 0)
        {
            currentRotation += i;
            currentRotation += 4;
        }
        else
        {
            currentRotation += i;
        }
    }

    //default 360 at the start
    void adjustSpread(int i)
    {
        if(i == 0)
        {
            goalSound.spread = 360;
        }
        else if(i == 1)
        {
            goalSound.spread = 180;
        }
        else if (i == 2)
        {
            goalSound.spread = 0;
        }
        else
        {
            goalSound.spread = 180;
        }
    }

    void updateWind()
    {
        //facing down
        if(rotations[currentRotation][0] == "down" && !(game.hwalls[Mathf.RoundToInt(transform.position.x) + 1, Mathf.RoundToInt(transform.position.y)])) //left
        {
            leftWind.volume = 0.85f;
        }
        else if(rotations[currentRotation][0] == "down")
        {
            leftWind.volume = 0;
        }
        
        if (rotations[currentRotation][0] == "down" && !(game.hwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)])) //right
        {
            rightWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "down")
        {
            rightWind.volume = 0;
        }

        //facing left
        if (rotations[currentRotation][0] == "left" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) + 1])) //up
        {
            rightWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "left")
        {
            rightWind.volume = 0;
        }

        if (rotations[currentRotation][0] == "left" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)])) //down
        {
            leftWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "left")
        {
            leftWind.volume = 0;
        }

        //facing up
        if (rotations[currentRotation][0] == "up" && !(game.hwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)]))
        {
            leftWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "up")
        {
            leftWind.volume = 0;
        }

        if (rotations[currentRotation][0] == "up" && !(game.hwalls[Mathf.RoundToInt(transform.position.x) + 1, Mathf.RoundToInt(transform.position.y)]))
        {
            rightWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "up")
        {
            rightWind.volume = 0;
        }

        //facing right
        if (rotations[currentRotation][0] == "right" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) + 1]))
        {
            leftWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "right")
        {
            leftWind.volume = 0;
        }

        if (rotations[currentRotation][0] == "right" && !(game.vwalls[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)]))
        {
            rightWind.volume = 0.85f;
        }
        else if (rotations[currentRotation][0] == "right")
        {
            rightWind.volume = 0;
        }
    }
}
