using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPelota : MonoBehaviour
{
    public float fuerzaMaxima = 1000f;
    private float tiempoCarga = 0f;
    private float tiempoMaximo = 3f;
    private bool cargando = false;
    private bool puedeDisparar = true;
    private bool controlBloqueado = false;

    private Rigidbody rb;

    private bool disparada = false;
    public float tiempoReinicio = 3f;
    private float cronometro = 0f;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    public AudioClip sonidoDisparo;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;

        audioSource = GetComponent<AudioSource>();
        if (sonidoDisparo != null && audioSource != null)
        {
            float volumen = PlayerPrefs.GetFloat("volumen", 1f);
            audioSource.PlayOneShot(sonidoDisparo, volumen);
        }
    }

    void Update()
    {
        if (controlBloqueado) return; // ✅ bloqueo total después del juego

        if (puedeDisparar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cargando = true;
                tiempoCarga = 0f;
            }

            if (Input.GetKey(KeyCode.Space) && cargando)
            {
                tiempoCarga += Time.deltaTime;
                tiempoCarga = Mathf.Clamp(tiempoCarga, 0f, tiempoMaximo);
            }

            if (Input.GetKeyUp(KeyCode.Space) && cargando)
            {
                float fuerza = tiempoCarga / tiempoMaximo * fuerzaMaxima;

                Vector3 direccionDisparo = transform.forward * 0.8f + transform.up * 0.6f;
                rb.AddForce(direccionDisparo.normalized * fuerza);

                Debug.DrawRay(transform.position, direccionDisparo * 5f, Color.red, 1f);

                if (sonidoDisparo != null && audioSource != null)
                {
                    float volumen = PlayerPrefs.GetFloat("volumen", 1f);
                    audioSource.PlayOneShot(sonidoDisparo, volumen);
                }

                cargando = false;
                disparada = true;
                puedeDisparar = false;
                cronometro = 0f;
            }
        }

        if (disparada)
        {
            cronometro += Time.deltaTime;

            if (cronometro >= tiempoReinicio)
            {
                ReiniciarPelota();
            }
        }
    }

    public void ReiniciarPelota()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        disparada = false;

        if (!controlBloqueado)
            puedeDisparar = true;
    }

    public void DesactivarControl()
    {
        puedeDisparar = false;
        cargando = false;
        controlBloqueado = true;
    }
}
