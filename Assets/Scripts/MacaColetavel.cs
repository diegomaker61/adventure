using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaColetavel : MonoBehaviour
{   
    [Header("Objetos")]
    public GameObject Jogador;

    [Header("Velocidade")]
    public float VelocidadeRotacao;
    public float VelocidadeMovimento;
    public float Distancia;
    bool Pegou;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {
    if(other.gameObject.name =="Jogador"){
        GameObject.Destroy(this.gameObject);    
    }
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up,VelocidadeRotacao*Time.deltaTime);
        Distancia = Vector3.Distance(Jogador.transform.position,this.transform.position);

        if(Distancia<4f){
            Pegou = true; 
        }

        if(Pegou){
            this.transform.position = Vector3.Lerp(this.transform.position,Jogador.transform.position,VelocidadeMovimento*Time.deltaTime); 
        }

        

    }
}
