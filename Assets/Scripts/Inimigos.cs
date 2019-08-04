using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public float Distancia;
    GameObject Jogador;
    public GameObject PivorSelecao;
    
    void Awake() {
    Jogador = GameObject.Find("Jogador");
    PivorSelecao = this.transform.GetChild (0).gameObject;
    }
    void Start()
    {
        
    }

    
    
    void Update()
    {
        Distancia = Vector2.Distance(Jogador.transform.position,this.transform.position);

    }
}
