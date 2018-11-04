using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour {

    public SpriteRenderer[] Colors;
    private int ColorSelected;
    public AudioSource[] Sounds;

    public float LitTime;
    private float LitTimeCounter;

    public float WaitTime;
    private float WaitTimeCounter;

    private bool shouldLit;
    private bool shouldDim;
    public bool Active = false;

    public List<int> Seq;
    private int Pos;
    private int Input;

    public AudioSource Correct;
    public AudioSource Incorrect;

    public Text Score;
    public Text HighScore;
    public Text RestartBtn;
    public SpriteRenderer GoText;

    private int Lvl;

    void Start()
    {
        RestartBtn.text = "Start";
        Lvl = PlayerPrefs.GetInt("Lvl");

        LitTime = 1f - (Lvl * 0.2f);
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
        HighScore.text = "High Score - "+ PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update () {

        if (shouldLit)
        {
            LitTimeCounter -= Time.deltaTime;
            if(LitTimeCounter < 0)
            {
                Colors[Seq[Pos]].color = new Color(Colors[Seq[Pos]].color.r,
                            Colors[Seq[Pos]].color.g, Colors[Seq[Pos]].color.b, .5f);
                StartCoroutine(VolumeFade(Sounds[Seq[Pos]], 0f, .3f));


                shouldLit = false;


                shouldDim = true;
                WaitTimeCounter = WaitTime;

                Pos++;
            }
        }

        if (shouldDim)
        {
            WaitTimeCounter -= Time.deltaTime;
            
            if(Pos >= Seq.Count)
            {
                shouldDim = false;
                //Wait fo player input
                GoText.enabled = true;


                Active = true;
            }
            else
            {
                if(WaitTimeCounter < 0)
                {


                    Colors[Seq[Pos]].color = new Color(Colors[Seq[Pos]].color.r,
                        Colors[Seq[Pos]].color.g, Colors[Seq[Pos]].color.b, 1f);
                    Sounds[Seq[Pos]].Play();




                    LitTimeCounter = LitTime;
                    shouldLit = true;
                    shouldDim = false;

                }
            }
        }
	}

    public void StartGame()
    {
        RestartBtn.text = "Restart";
        Clear();

        ColorSelected = Random.Range(0, Colors.Length);

        Seq.Add(ColorSelected);

        Colors[Seq[Pos]].color = new Color(Colors[Seq[Pos]].color.r,
            Colors[Seq[Pos]].color.g, Colors[Seq[Pos]].color.b, 1f);
        Sounds[Seq[Pos]].Play();


        LitTimeCounter = LitTime;
        shouldLit = true;

        Score.text = "0";
        
    }

    public void getColorPressed(int whichButton)
    {
        if (Active)
        {
            if(Seq[Input] == whichButton)
            {
                Input++;
                if(Seq.Count <= Input)
                {

                    GoText.enabled = false;
                    StartCoroutine(NewSound());
                }
            }
            else
            {
                Incorrect.Play();
                GoText.enabled = false;

                Active = false;
            }
        }
        
    }

    void Clear()
    {
        foreach (SpriteRenderer Color in Colors)
        {
            Color.color = new Color(Color.color.r, Color.color.g, Color.color.b, .5f);
        }
        foreach (AudioSource Sound in Sounds)
        {
            StartCoroutine(VolumeFade(Sound, 0f, .4f));
        }


        Seq.Clear();

        Pos = 0;
        Input = 0;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator NewSound()
    {
        Correct.Play();
        Pos = 0;
        Input = 0;
        
        ColorSelected = Random.Range(0, Colors.Length);

        Score.text = "" + Seq.Count;
        if (Seq.Count > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", Seq.Count);
        HighScore.text = "High Score - " + PlayerPrefs.GetInt("HighScore");

        yield return new WaitForSeconds(1f);

        Seq.Add(ColorSelected);
        Colors[Seq[Pos]].color = new Color(Colors[Seq[Pos]].color.r,
            Colors[Seq[Pos]].color.g, Colors[Seq[Pos]].color.b, 1f);
        Sounds[Seq[Pos]].Play();

        if (Lvl == 3 && Seq.Count % 5 == 0 && Seq.Count > 4)
        {
            LitTime *= 0.95f;
            Debug.Log(LitTime);
        }

        LitTimeCounter = LitTime;
        shouldLit = true;
        Active = false;

    }

    IEnumerator VolumeFade(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {

        float _StartVolume = _AudioSource.volume;

        float _StartTime = Time.time;

        while (Time.time < _StartTime + _FadeLength)
        {

            _AudioSource.volume = _StartVolume + ((_EndVolume - _StartVolume) * ((Time.time - _StartTime) / _FadeLength));

            yield return null;

        }

        if (_EndVolume == 0) { _AudioSource.Stop(); }
        _AudioSource.volume = 1f;
    }
}
