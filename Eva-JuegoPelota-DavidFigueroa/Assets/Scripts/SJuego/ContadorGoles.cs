using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContadorGoles : MonoBehaviour
{
    public int goles = 0;
    public TextMeshProUGUI textoGoles;
    public GameObject panelFinal;
    public TextMeshProUGUI textoFinal;

    public GameObject pelota;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Rigidbody rbPelota;

    public ParticleSystem fuegosArtificiales;

    public AudioClip sonidoGol;
    public AudioSource audioSource;

    void Start()
    {
        if (pelota != null)
        {
            posicionInicial = pelota.transform.position;
            rotacionInicial = pelota.transform.rotation;
            rbPelota = pelota.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            if (sonidoGol != null && audioSource != null)
            {
                float volumen = PlayerPrefs.GetFloat("volumen", 1f);
                audioSource.PlayOneShot(sonidoGol, volumen);
            }

            goles++;

            if (textoGoles != null)
                textoGoles.text = "Goles: " + goles;

            if (goles >= 3)
            {
                if (fuegosArtificiales != null)
                    fuegosArtificiales.Play();

                if (panelFinal != null)
                    panelFinal.SetActive(true);

                if (textoFinal != null)
                {
                    string nombreJugador = PlayerPrefs.GetString("nombreJugador", "");
                    if (string.IsNullOrWhiteSpace(nombreJugador))
                        nombreJugador = "Player";

                    textoFinal.text = "¡Ganaste, " + nombreJugador + "!";
                }

                // Desactiva control de disparo
                if (pelota != null)
                {
                    DisparoPelota disparo = pelota.GetComponent<DisparoPelota>();
                    if (disparo != null)
                        disparo.DesactivarControl();
                }

                return; // no reiniciar pelota si ya ganó
            }

            if (pelota != null)
            {
                rbPelota.velocity = Vector3.zero;
                rbPelota.angularVelocity = Vector3.zero;
                pelota.transform.position = posicionInicial;
                pelota.transform.rotation = rotacionInicial;
            }
        }
    }
}
