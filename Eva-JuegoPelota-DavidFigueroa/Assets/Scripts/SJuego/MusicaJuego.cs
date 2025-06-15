using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float volumen = PlayerPrefs.GetFloat("volumen", 1f);
        GetComponent<AudioSource>().volume = volumen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
