using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class QuizManager : MonoBehaviour
{
    enum Estados {rellenar_pantalla, esperar_input_opcion, revisar_pregunta_correcta_o_no, feedback, revisar_si_el_juego_termina_o_no }
    Estados estadoJuego;
    [SerializeField] TextMeshProUGUI valorPregunta;
    [SerializeField] TextMeshProUGUI[] opcionesBotones;
    [SerializeField] TextMeshProUGUI puntacion;

    [Space(30)]
    [SerializeField] PreguntasJuegoKirby[] preguntas;
    //botones gameobject
    public GameObject[] opciones;
    //huh?? huh?? huh??
    Color verde;
    Color rojo;
    //contador de las preguntas y las preguntas maximas
    int contadorPreguntasKirby;
    int preguntasMaximas = 10;
    //puntaje del quiz de kirby actual y final
    int puntajeQuizFinal;
    int puntajeActual;
    int puntajePorPreguntaCorrecta = 100;
    //informacion de preguntas
    int botonPresionado;
    PreguntasJuegoKirby preguntaTiempoPresente;




    //Este metodo busca la info y la rellena en la pantalla
    void BuscarPreguntasAleatoriasYMostrarlas()
    {
        do
        {
            int lengthQuestions = preguntas.Length;
            //Debug.Log(lengthQuestions);
            int index = Random.Range(0, lengthQuestions);
            //Debug.Log(index);
            //Debug.Break();
            PreguntasJuegoKirby preguntasJuegoKirby = preguntas[index];
            preguntaTiempoPresente = preguntasJuegoKirby;
        }
        while (preguntaTiempoPresente.fueLaPreguntaYaRealizada);

        preguntaTiempoPresente.fueLaPreguntaYaRealizada = true;

        for(int i = 0; i < opcionesBotones.Length; i++)
        {
            opcionesBotones[i].text = preguntaTiempoPresente.opciones[i];
        }


        valorPregunta.text = preguntaTiempoPresente.preguntasKirby;

    }

    //esta funcion sucede al presionar el boton(evento onclick)
    public void AlPresionarBoton(int botonPresionado)
    {
        if (estadoJuego == Estados.esperar_input_opcion)
        {
            this.botonPresionado = botonPresionado;
            estadoJuego = Estados.revisar_pregunta_correcta_o_no;
        }
    }


    //Esta funcion revisa la pregunta, y dependiendo que si este correcta o no, lo pone en verde(correcta) y suma el puntaje, o lo pone en rojo(falso) y no suma nada.
    void RevisarSiPreguntaEsCorrecta()
    {
        //si es correcta, poner la opcion en verde: check
        //si es incorrecta, poner la opcion en rojo y no se suma nada(en codigo no se hace nada): check

        if (preguntaTiempoPresente.opcionesCorrecta[botonPresionado] == true)
        {
            puntajeActual = puntajeActual + puntajePorPreguntaCorrecta;
            puntacion.text = puntajeActual.ToString();
            //aqui pongo mi script para cambiar boton a verde(se hace con un arreglo = [])
            //funciona pero solo pone el primer boton en verde, hay progreso.
            verde = opciones[0].GetComponent<Image>().color = Color.green;
            verde = opciones[1].GetComponent<Image>().color = Color.green;
            verde = opciones[2].GetComponent<Image>().color = Color.green;
            verde = opciones[3].GetComponent<Image>().color = Color.green;
            



        }
        else
        {
            //aqui pongo mi script para cambiar boton a rojo(se hace con un arreglo = [])
            rojo = opciones[0].GetComponent<Image>().color = Color.red;
            rojo = opciones[1].GetComponent<Image>().color = Color.red;
            rojo = opciones[2].GetComponent<Image>().color = Color.red;
            rojo = opciones[3].GetComponent<Image>().color = Color.red;
            
        }

        estadoJuego = Estados.feedback;
        StartCoroutine(ejemploCoroutine());

    }

   


    //este metodo suma el contador de preguntas
    bool SumaDeContadorPreguntas()
    {
        contadorPreguntasKirby++;

        return contadorPreguntasKirby < preguntasMaximas;
    }

    
    /*
    void PreguntaColorYPuntos()
    {
        if ( == true)
        {
            puntajeQuiz++;
            OpcionA.GetComponent<Image>().color = Color.green;
            OpcionB.GetComponent<Image>().color = Color.green;
            OpcionC.GetComponent<Image>().color = Color.green;
            OpcionD.GetComponent<Image>().color = Color.green;
        }
        else
        {
            OpcionA.GetComponent<Image>().color = Color.red;
            OpcionB.GetComponent<Image>().color = Color.red;
            OpcionC.GetComponent<Image>().color = Color.red;
            OpcionD.GetComponent<Image>().color = Color.red;
        }
    


    }
    */

    //Este metodo revisa si el juego se termina o no
    void JuegoTerminaONo()
    {
        if (contadorPreguntasKirby < preguntasMaximas)
        {
            estadoJuego = Estados.rellenar_pantalla;
        }
        else
        {
            puntajeQuizFinal = puntajeActual;
            PlayerPrefs.SetInt("puntajefinal", puntajeQuizFinal);
            PlayerPrefs.Save();
            SceneManager.LoadScene("final_juego_kirby");

            
        }
    }

    void OpcionElegida(string caracteresDelBoton)
    {
        //botonPresionado = ;
        //alternativaCorrecta = preguntaTiempoPresente.opciones[];
        //valorPregunta.text = alternativaCorrecta.ToString();
    }


    /*
    public void CambiarEscenaKirby()
    {
        SceneManager.LoadScene("juego_kirby_quiz_escena");
    }
    */

    IEnumerator ejemploCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        estadoJuego = Estados.revisar_si_el_juego_termina_o_no;
    }

    private void Update()
    {
        switch (estadoJuego)
        {
            case Estados.rellenar_pantalla:
                //conseguir pregunta aleatoria: falla encontrada
                //mostrarla: falla encontrada
                //añadimos 1 al contador de preguntas: check
                //se pasa al siguiente estado: check
                BuscarPreguntasAleatoriasYMostrarlas();
                SumaDeContadorPreguntas();
                estadoJuego = Estados.esperar_input_opcion;
                break;
            case Estados.esperar_input_opcion:
                //guardar la opcion del jugador: check
                break;
            case Estados.revisar_pregunta_correcta_o_no:
                RevisarSiPreguntaEsCorrecta();
                break;
            case Estados.feedback:
                //esperar 0.5 segundos antes del proximo estado: falta hacer
                break;
            case Estados.revisar_si_el_juego_termina_o_no:
                //si quedan preguntas, nos devolvemos al primer estado: check
                //si no quedan preguntas, pasar a la escena de fin de juego: check
                //mostrar puntaje final en fin de juego: check
                JuegoTerminaONo();
                break;
        }
    }

}
[System.Serializable]
    public class PreguntasJuegoKirby
    {

        public string preguntasKirby;
        public string[] opciones;
        public bool[] opcionesCorrecta;
        [HideInInspector] public bool fueLaPreguntaYaRealizada;
    }
