using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escadas : MonoBehaviour
{   
    public bool PosSubirEscada;
    public bool SubirEscada;
    GameObject Jogador;
    ControleJogador tempControleJogador;
    CharacterController Controlador;
    public bool SubindoEscada;
    public bool DescendoEscada;
    GameObject PesJogador;
    public float DistJogador_Chao;
    public GameObject PivorSuperior;
    public GameObject PivorEnt_Sai_Superior;
    public bool SaindoEscadaPorCima;
    public GameObject PivorInferior;   
    public bool SaindoEscadaPorBaixo;
    public GameObject PivorEnt_Sai_Inferior;
    GameObject PivorRotacao;


    void Start()
    {
    Jogador = GameObject.Find("Jogador");
    tempControleJogador = Jogador.GetComponent<ControleJogador>();
    Controlador = Jogador.GetComponent<CharacterController>();
    PesJogador = GameObject.Find("Pes");
    PivorRotacao = GameObject.Find("PivorVisao");

    }

    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Jogador"){
            PosSubirEscada = true;
        }
        
        
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Jogador"){
        PosSubirEscada = false;   
        }    
    }
    
    void Update()
    {


    if(PosSubirEscada){
        
        if(Input.GetButtonDown("ButtonTriangle")){
            //EmBaixo na escada
            if(PivorSuperior.transform.position.y >= PesJogador.transform.position.y){
                SubirEscada = true;
                tempControleJogador.Controle = false;
                tempControleJogador.NaEscada = true;
                tempControleJogador.DirecaoFinal = Vector3.zero;
                Jogador.transform.position = PivorEnt_Sai_Inferior.transform.position;
                PivorRotacao.transform.eulerAngles = this.transform.eulerAngles;

            
            }else{
            //Encima na escada
                SubirEscada = true;
                tempControleJogador.Controle = false;
                tempControleJogador.NaEscada = true;
                tempControleJogador.DirecaoFinal = Vector3.zero;
                Jogador.transform.position = PivorEnt_Sai_Superior.transform.position;
                PivorRotacao.transform.eulerAngles = this.transform.eulerAngles;
            }
        }

       

        
    }

    if(SubirEscada){

        InvokeRepeating("SaindoEscada",0.1f,0);

        if(Input.GetAxis("LeftJoystickVertical")<0){
            SubindoEscada = true;
            DescendoEscada = false;    
        }
        if(Input.GetAxis("LeftJoystickVertical")>0){
            DescendoEscada = true;
            SubindoEscada = false;

        }
        if(Input.GetAxis("LeftJoystickVertical")==0){
            SubindoEscada =false;
            DescendoEscada =false;    
        }


        if(PivorSuperior.transform.position.y < PesJogador.transform.position.y){
           SaindoEscadaPorCima = true; 
        }

        if(SaindoEscadaPorCima){
            SubirEscada = false;
            tempControleJogador.Controle = true;
            tempControleJogador.NaEscada = false;
            SubindoEscada = false;
            SaindoEscadaPorCima = false;
        }

        if(PivorInferior.transform.position.y > PesJogador.transform.position.y){
            SaindoEscadaPorBaixo=true;
        }

        if(SaindoEscadaPorBaixo){
            SubirEscada = false;
            tempControleJogador.Controle = true;
            tempControleJogador.NaEscada = false;
            DescendoEscada = false;
            SaindoEscadaPorBaixo = false;
        }

    }

        
    }

    void FixedUpdate(){
        
        if(SubindoEscada){
            Controlador.Move(this.transform.up*4*Time.deltaTime);
        }
        if(DescendoEscada){
            Controlador.Move(-this.transform.up*4*Time.deltaTime);
        }

    }

    void SaindoEscada(){
        
        if(DistJogador_Chao == 0){
            if(Input.GetButtonDown("ButtonTriangle")){
                SubirEscada = false;
                tempControleJogador.Controle = true;
                tempControleJogador.NaEscada = false;
            }
        }
    }
}
