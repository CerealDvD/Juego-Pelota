using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public Toggle toggleDificultad;
    public Slider volumenSlider;

    void Start()
    {
        float volumenGuardado = PlayerPrefs.GetFloat("volumen", 1f);

        // Establece el valor visual sin disparar evento
        volumenSlider.SetValueWithoutNotify(volumenGuardado);

        // Llama a CambiarVolumen manualmente
        if (AudioManager.instancia != null)
        {
            AudioManager.instancia.CambiarVolumen(volumenGuardado);
        }

        // 🔧 AHORA conecta el evento del slider
        volumenSlider.onValueChanged.AddListener((v) =>
        {
            if (AudioManager.instancia != null)
                AudioManager.instancia.CambiarVolumen(v);
        });
    }


    public void Jugar()
    {
        string nombre = nombreInput.text;
        if (string.IsNullOrWhiteSpace(nombre))
            nombre = "Player";

        PlayerPrefs.SetString("nombreJugador", nombre);
        PlayerPrefs.SetInt("moverArco", toggleDificultad.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("volumen", volumenSlider.value);

        SceneManager.LoadScene("Juego");
    }
}
