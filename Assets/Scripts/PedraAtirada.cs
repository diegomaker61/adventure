using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraAtirada : MonoBehaviour
{
    GameObject Alvo;
    ControleJogador tempControleJogador;
    public float VelocidadePedra;
    public bool AtiradaPeloJogador;

    void Start()
    {   
        tempControleJogador = GameObject.Find("Jogador").GetComponent<ControleJogador>();    
    }

    void OnTriggerEnter(Collider other) {
    if(other.gameObject.tag == "Inimigo"){
        GameObject.Destroy(this.gameObject);    
    }

    }
    
    void Update()
    {   
        
        if(tempControleJogador.InimigoMaisProximo!=null && AtiradaPeloJogador){
            //Alvo = tempControleJogador.InimigoMaisProximo;
            
            this.transform.Translate(0,0,VelocidadePedra*Time.deltaTime);
        }       
    }
}
