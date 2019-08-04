using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilEsquiloAtirador : MonoBehaviour
{
    GameObject Jogador;
    ControleJogador tempControleJogador;
    public float VelocidadeProjetil;
    void Start()
    {   
        Jogador =GameObject.Find("Jogador");
        tempControleJogador = Jogador.GetComponent<ControleJogador>();
    }

    void OnTriggerEnter(Collider other) {
    //Caso colida com qualquer superficie
    GameObject.Destroy(this.gameObject); 

    if(other.gameObject.name == "Jogador" && !tempControleJogador.Invulneravel){
        tempControleJogador.DanoSimples = true;
        tempControleJogador.VidaJogador -=1;
    }   
        
    }

    
    void FixedUpdate()
    {
        transform.Translate(0,0,VelocidadeProjetil*Time.deltaTime);
    }

    
}
