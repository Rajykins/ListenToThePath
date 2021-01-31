using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;



public class Game : MonoBehaviour
{
    public float holep;
    public int w, h, x, y;
    public bool[,] hwalls, vwalls;
    public Transform Level, Player, Goal;
    public GameObject Floor, Wall;
    public CinemachineVirtualCamera cam;
    public AudioSource Collect;
    public Light2D starLight;
    public Light2D torch;
    float scale = 2;
    int tog = 1;
    public AudioSource  lvl2, lvl3, lvl4, lvl5, lvl6;
    int level = 0;

    void Start()
    {
        GeneterateMaze();
        if(starLight.intensity > 0)
        {
            starLight.intensity = starLight.intensity - 0.5f;
        }

        if (starLight.pointLightOuterRadius > 1)
        {
            starLight.pointLightOuterRadius = starLight.pointLightOuterRadius - 1f;
        }

        if (scale > 0.5)
        {
            scale = scale - 0.25f;
        }

        if (torch.pointLightOuterRadius > 1)
        {
            torch.pointLightOuterRadius = torch.pointLightOuterRadius - 0.5f;
        }
        level++;

        if (level == 2) 
        {
            lvl2.Play();
        }
        if (level == 3) 
        {
            lvl2.Stop();
            lvl3.Play();
        }
        if (level == 4) 
        {
            lvl3.Stop();
            lvl4.Play();
        }
        if (level == 5) 
        {
            lvl4.Stop();
            lvl5.Play();
        }
        if (level == 6) 
        {
            lvl5.Stop();
            lvl6.Play();
        }
        if (level == 15)
        {
            SceneManager.LoadScene(sceneName: "Ending");
        }
    }

    public int Rand(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }

    public float frand()
    {
        return UnityEngine.Random.value;
    }

    void GeneterateMaze()
    {
        foreach (Transform child in Level)
            Destroy(child.gameObject);

        hwalls = new bool[w + 1, h];
        vwalls = new bool[w, h + 1];
        var st = new int[w, h];

        void dfs(int x, int y)
        {
            st[x, y] = 1;
            Instantiate(Floor, new Vector3(x, y), Quaternion.identity, Level);

            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W),
            };
            foreach (var (nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
                if (!(0 <= nx && nx < w && 0 <= ny && ny < h) || (st[nx, ny] == 2 && Random.value > holep))
                {
                    wall[wx, wy] = true;
                    Instantiate(Wall, new Vector3(wx, wy) - sh / 2, Quaternion.Euler(0, 0, ang), Level);
                }
                else if (st[nx, ny] == 0)
                {
                    dfs(nx, ny);
                }

            st[x, y] = 2;
        }
        dfs(0, 0);

        x = Random.Range(0, w);
        y = Random.Range(0, h);
        Player.position = new Vector3(x, y);

        do
        {
            Goal.position = new Vector3(Random.Range(0, w), Random.Range(0, h));
        }
        while (Vector3.Distance(Player.position, Goal.position) < (w + h) / 4);

        cam.m_Lens.OrthographicSize = (Mathf.Pow(w / 3 + h / 2, 0.7f) + 1) / 1.5f;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tog = tog * -1;
        }

        if (tog == 1) {
            torch.intensity = scale;
        }
        else { torch.intensity = 0; }

        if (Vector3.Distance(Player.position, Goal.position) < 0.5f)
        {
            Collect.Play();
            if (Random.Range(0, 5) < 3) w++;
            else h++;
            Start();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}