using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteoScript : MonoBehaviour
{
    [SerializeField] int numero = 0;
    [SerializeField] int menorA = 10;

    private void Update()
    {
        if (numero < menorA)
        {
            Debug.Log("esto esta pasando");
        }
    }   
}
