using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OScript : MonoBehaviour {

    public Image[] Colors;
    private int ColorSelected;
    private bool waitActive;
    public AudioSource[] Sounds;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!waitActive)
        {
            StartCoroutine(WaitAndLight());
        }
    }

    IEnumerator WaitAndLight()
    {
        ColorSelected = Random.Range(0, Colors.Length);

        Colors[ColorSelected].color = new Color(Colors[ColorSelected].color.r,
    Colors[ColorSelected].color.g, Colors[ColorSelected].color.b, 1f);
        Sounds[ColorSelected].Play();

        waitActive = true;
        yield return new WaitForSeconds(.5f);

        Colors[ColorSelected].color = new Color(Colors[ColorSelected].color.r,
Colors[ColorSelected].color.g, Colors[ColorSelected].color.b, .3f);
        StartCoroutine(VolumeFade(Sounds[ColorSelected], 0f, .07f));

        yield return new WaitForSeconds(.07f);
        waitActive = false;
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
        _AudioSource.volume = .8f;
    }
}
