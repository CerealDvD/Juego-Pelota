using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;
    private AudioSource audioSource;

    public TextMeshProUGUI textoVolumen;
    private float volumenActual;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        float volumen = PlayerPrefs.GetFloat("volumen", 1f);
        audioSource.volume = volumen;
    }

    public void CambiarVolumen(float nuevoVolumen)
    {
        if (instancia == null) return;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("❌ AudioSource no disponible al cambiar volumen.");
                return;
            }
        }

        volumenActual = nuevoVolumen;
        audioSource.volume = nuevoVolumen;
        PlayerPrefs.SetFloat("volumen", nuevoVolumen);
        ActualizarTextoVolumen(nuevoVolumen);

        Debug.Log($"🎚 Volumen actualizado: {nuevoVolumen * 100f}% en {gameObject.name}");
    }

    private void ActualizarTextoVolumen(float valor)
    {
        if (textoVolumen != null)
        {
            int porcentaje = Mathf.RoundToInt(valor * 100f);
            textoVolumen.text = "Volumen: " + porcentaje + "%";
        }
    }
}
