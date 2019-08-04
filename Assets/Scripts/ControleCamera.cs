
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControleCamera : MonoBehaviour
{   
    [HideInInspector]public GameObject LookCamera;
    [HideInInspector]public GameObject CameraDirecao;
    [HideInInspector]public GameObject MovimentoCamera;
    [HideInInspector]public GameObject CameraLerp; 
    public GameObject Jogador;
    public float DistanciaHorizontal;
    public float DistanciaVertical;
    public float DistanciaCamera_JogadorColisao;

    [Header("Velocidades")]
    public float VelocidadeAcompanhamentoPivor;//Velocidade em que o MovimentoCamera Acompanha o jogador
    public float VelocidadeMovimentoCamera;//Velocidade(Lerp)em que a camera Principal acompanha o movimento camera
    public float VelocidadeRotacaoCameraAutomatica;//Velocidade(Lerp) em que a Rotacao da camera acompanha a rotacao da CameraLerp

    //Raycast
    [Header("Colisao Camera Automatica")]
    RaycastHit Raio;
    public LayerMask LayerRaycast;
    ControleJogador tempControleJogador;
    public bool CameraColisao;
    public bool ForaColisaoInicial;
    public bool CameraAutomatica;
    

    //Camera Manual 
    [Header("Camera Manual")]
    public float VelocidadeLateral;
    public float MovimentoHorizontal;
    public float VelocidadeRotacaoCameraManual;
    public GameObject PivorPeJogador;
    public GameObject PeJogador;
    public GameObject PivorCameraManual;
    public TextMeshProUGUI StatusCamera;


    void Start(){
        tempControleJogador = Jogador.GetComponent<ControleJogador>();
        
    }

    void Update(){

        if(Input.GetButtonDown("ButtonL1")){
           CameraAutomatica =!CameraAutomatica; 
        }

        
    }
    
    void FixedUpdate(){


    if(!tempControleJogador.Pulando){
    PivorPeJogador.transform.position = PeJogador.transform.position;
    }else{
    PivorPeJogador.transform.position = new Vector3(PeJogador.transform.position.x,PivorPeJogador.transform.position.y,PeJogador.transform.position.z);    
    }
        

    
    if(CameraAutomatica){

    //Mudando Status da camera na UI
    StatusCamera.text = "Camera Automatica";
    

    /****************************************  CAMERA AUTOMATICA ************************************************ */ 

    this.transform.parent = null;
    MovimentoCamera.transform.parent = null;
    CameraDirecao.transform.parent = null;

    //Distancia Vertical entre Movimento Camera e LookCamera
    Vector3 PontoMovCameraY = new Vector3(MovimentoCamera.transform.position.x,MovimentoCamera.transform.position.y,LookCamera.transform.position.z);
    DistanciaVertical = Vector3.Distance(LookCamera.transform.position,PontoMovCameraY);
    

    //Distancia entre MovimentoCamera e LookCamera
    Vector3 PontoMovCameraX = new Vector3(MovimentoCamera.transform.position.x,LookCamera.transform.position.y,MovimentoCamera.transform.position.z);
    DistanciaHorizontal = Vector3.Distance(PontoMovCameraX,LookCamera.transform.position);
    
    

    if(Physics.Linecast(LookCamera.transform.position,MovimentoCamera.transform.position,out Raio,LayerRaycast) && CameraColisao){
        
        //Variavel de transporte do MovimentoCamera
        ForaColisaoInicial = true;

        //So ajusta a altura se nao tiver pulando
        if(!tempControleJogador.Pulando ){
            Vector3 VerticalLerp = new Vector3(MovimentoCamera.transform.position.x,LookCamera.transform.position.y +1.6f,MovimentoCamera.transform.position.z);
            MovimentoCamera.transform.position = VerticalLerp;
        }

        Vector3 camerasemy = new Vector3(this.transform.position.x,LookCamera.transform.position.y,this.transform.position.z);
        float DistanciaCameraJog = Vector3.Distance(camerasemy,LookCamera.transform.position);
        
        //Desativa condicao de acordo com a distancia
        if(DistanciaCameraJog>6){
        CameraColisao = false;
        }

        this.transform.position = Raio.point + this.transform.forward *0.2f;
        Debug.DrawLine(MovimentoCamera.transform.position,LookCamera.transform.position,Color.yellow);

        }
    else{

        //Ajeita o MOVIMENTOCAMERA para o local da propria camera
        CameraColisao=true;
        if(ForaColisaoInicial){
            MovimentoCamera.transform.position = this.transform.position;
            ForaColisaoInicial=false;
        }
        
        //Movimentacao do "MovimentoCamera" HORIZONTAL
        if(DistanciaHorizontal>6f){
        MovimentoCamera.transform.Translate(0,0,VelocidadeAcompanhamentoPivor*Time.deltaTime);   
        }
        if(DistanciaHorizontal<5){
        MovimentoCamera.transform.Translate(0,0,-VelocidadeAcompanhamentoPivor*Time.deltaTime);    
        }

        
        //Altura da camera em relacao ao jogador 
        if(!tempControleJogador.Pulando ){
            Vector3 VerticalLerp = new Vector3(MovimentoCamera.transform.position.x,LookCamera.transform.position.y +1.6f,MovimentoCamera.transform.position.z);
            MovimentoCamera.transform.position = Vector3.Lerp(MovimentoCamera.transform.position,VerticalLerp,VelocidadeMovimentoCamera*Time.deltaTime);
        }
       
        //Movimento Camera Olha para LookCamera
        Vector3 LookCameraSemY = new Vector3(LookCamera.transform.position.x,MovimentoCamera.transform.position.y,LookCamera.transform.position.z);
        MovimentoCamera.transform.LookAt(LookCameraSemY);
        
        //Lerp De movimento Camera
        this.transform.position = Vector3.Lerp(this.transform.position,MovimentoCamera.transform.position,VelocidadeMovimentoCamera*Time.deltaTime);
        
    }
    
    //Lerp de Rotacao Camera
    CameraLerp.transform.LookAt(LookCamera.transform);
    float LerpAnguloCamera = Mathf.LerpAngle(this.transform.eulerAngles.y,CameraLerp.transform.eulerAngles.y,VelocidadeRotacaoCameraAutomatica*Time.deltaTime);
    this.transform.eulerAngles = new Vector3(CameraLerp.transform.eulerAngles.x,LerpAnguloCamera,0);

    //Direcao da Camera 
    CameraDirecao.transform.position = this.transform.position;
    CameraDirecao.transform.eulerAngles = new Vector3(0,CameraLerp.transform.eulerAngles.y,0);

    //Evita que quanto troca de camera ela teleporte ou ande de mais 
    PivorCameraManual.transform.position = MovimentoCamera.transform.position;
    
    }
    
    else{

    //Mudando status da camera na UI
    StatusCamera.text = "Camera Manual";    
    
    /****************************************  CAMERA MANUAL  *************************************************/  
        

        //this.transform.parent = PivorCameraManual.transform;
        MovimentoCamera.transform.parent = PivorCameraManual.transform;
        CameraDirecao.transform.parent = PivorCameraManual.transform;

        //Movimento camera e camera direcao estaco sempre na posicao de pivor camera manual 
        MovimentoCamera.transform.position = PivorCameraManual.transform.position;
        CameraDirecao.transform.position = PivorCameraManual.transform.position;

        //Rotacao de camera 
        MovimentoHorizontal = Input.GetAxis("RightJoystickHorizontal")*-VelocidadeRotacaoCameraManual*Time.deltaTime;
        PivorPeJogador.transform.Rotate(Vector3.up*MovimentoHorizontal);

        //Direcao final do personagem sem interferancia da rotacao da camera que olha pra o jogador
        CameraDirecao.transform.eulerAngles = new Vector3(0,this.transform.eulerAngles.y,0);
        
        
        //Lerp de Rotacao Camera
        Vector3 LookCameraSemX = new Vector3(this.transform.position.x,LookCamera.transform.position.y,LookCamera.transform.position.z);
        this.transform.LookAt(LookCamera.transform);
            
        //Quando o jogador pula a camera deixa de ser filha do pivor 
        // E ela vai de lerp para cima do mesmo pivor , evitar que a camera teleporte
        
        
        //Colisao de camera manual 

        if(Physics.Linecast(LookCamera.transform.position,PivorCameraManual.transform.position,out Raio,LayerRaycast)){
            this.transform.position = Raio.point + this.transform.forward *0.3f;
            
        }else{
            if(tempControleJogador.Pulando){
                this.transform.parent = null;
                this.transform.position = Vector3.Lerp(this.transform.position,PivorCameraManual.transform.position,VelocidadeLateral*Time.deltaTime);   
            }else{
                this.transform.position = Vector3.Lerp(this.transform.position,PivorCameraManual.transform.position,VelocidadeLateral*Time.deltaTime);   
                this.transform.parent = PivorCameraManual.transform;
            }

        }

    }
    }
}
