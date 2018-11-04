using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

    private SpriteRenderer theSprite;

    public int thisNumber;

    private GameManeger theGM;

    private AudioSource theSound;

	// Use this for initialization
	void Start () {
        theSprite = GetComponent<SpriteRenderer>();
        theGM = FindObjectOfType<GameManeger>();
        theSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown ()
    {
        if (theGM.Active)
        { 
            theSprite.color = new Color(theSprite.color.r,
            theSprite.color.g, theSprite.color.b, 1f);
            theSound.Play();
        }
    }

    void OnMouseUp()
    {
        if (theGM.Active)
        {
            theSprite.color = new Color(theSprite.color.r,
                theSprite.color.g, theSprite.color.b, .5f);
            StartCoroutine(VolumeFade(theSound, 0f, .1f));
            theGM.getColorPressed(thisNumber);
        }
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
