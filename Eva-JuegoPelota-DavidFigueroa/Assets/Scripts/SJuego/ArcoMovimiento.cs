using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcoMovimiento : MonoBehaviour
{
    public float amplitud = 2f;
    public float velocidad = 2f;
    private Vector3 posicionInicial;

    private bool mover = false;

    void Start()
    {
        posicionInicial = transform.position;

        // Leer si el jugador activó el toggle en el menú
        mover = PlayerPrefs.GetInt("moverArco", 0) == 1;
    }

    void Update()
    {
        if (mover)
        {
            float offset = Mathf.Sin(Time.time * velocidad) * amplitud;
            transform.position = posicionInicial + new Vector3(offset, 0, 0);
        }
    }
}
