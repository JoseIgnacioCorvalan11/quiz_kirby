using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinDeJuegoScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultadoFinal;
    void Start()
    {
        SetPuntaje();
    }

    void SetPuntaje()
    {
        resultadoFinal.text = PlayerPrefs.GetInt("puntajefinal").ToString();

    }

}


