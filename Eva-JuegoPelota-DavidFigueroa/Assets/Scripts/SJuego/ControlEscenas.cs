using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
