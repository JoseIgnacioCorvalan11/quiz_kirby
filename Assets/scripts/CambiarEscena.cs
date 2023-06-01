using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarEscenaKirby()
    {
        SceneManager.LoadScene("juego_kirby_quiz_escena");
    }
}
