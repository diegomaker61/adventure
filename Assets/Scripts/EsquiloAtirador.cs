using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsquiloAtirador : MonoBehaviour
{   
    public int VidaEsquilo;
    public float VelocidadeEsquilo;
    public float VelocidadeRotacao;
    public float DistanciaEsquilo_Jogador; //Verifica distancia entre jogador e player 
    public GameObject Nozes; //Fazer um prefab disso
    public float CampoDeVisao;
    GameObject Jogador;
    public GameObject Mira;
    GameObject NovaNoz;
    public bool JogadorProximo;
    public float VariacaoTempoTiro;
    public float VelocidadeProjetil;
    public GameObject[] Triggers;
    public bool ChegouNoLocal;
    public GameObject Rotacao;
    public GameObject LerpRotacao;
    TriggersEsquiloAtirador tempTrigger1,tempTrigger2,tempTrigger3,tempTrigger4;

    [Header("Locais")]
    public GameObject[] Locais;
    public int ProximoLocal;
    float LerpAngle;
    bool LevouDano;
    public bool Visivel;

    public GameObject Degrau;
    public GameObject PoseFinalDegrau;

    /* Descrição: Codigo que faz um esquilo que atira nozes em cima do jogador , a referencia para o jogador saber onde a noz vai cair é uma mira que fica no chao.
    Essa mira so sai do lugar depois que a noz atinge ele*/

    void Start()
    {
        Jogador = GameObject.Find("Jogador");
        //A cada x Segundos 
        InvokeRepeating("Dandotiro",0,VariacaoTempoTiro);
        
        tempTrigger1 = Triggers[0].GetComponent<TriggersEsquiloAtirador>();
        tempTrigger2 = Triggers[1].GetComponent<TriggersEsquiloAtirador>();
        tempTrigger3 = Triggers[2].GetComponent<TriggersEsquiloAtirador>();
        tempTrigger4 = Triggers[3].GetComponent<TriggersEsquiloAtirador>();

    }

   
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Locais"){
            ChegouNoLocal = true;
            
        } 

        if(other.gameObject.tag == "Pedra"){
            Debug.Log("Bateu No inimigo");
            LevouDano =true;
            
        }
    }

    void OnTriggerExit (Collider other){
 
        if(other.gameObject.tag == "Locais"){
            ChegouNoLocal = false;    
            
        } 
    } 

    
    void Update(){
        if(LevouDano){
            VidaEsquilo-=1;
            LevouDano = false;
        }

        if(VidaEsquilo <= 0){
            AtivandoDegrau();
            
        }
    }
    void FixedUpdate()
    {   
        
        if(!tempTrigger1.Dentro && !tempTrigger2.Dentro && !tempTrigger3.Dentro && !tempTrigger4.Dentro){
          JogadorProximo = false; 
        }

        foreach (GameObject Trigger in Triggers){
            TriggersEsquiloAtirador tempTrigger = Trigger.GetComponent<TriggersEsquiloAtirador>();
            if(tempTrigger.Dentro){
                //O jogador esta em alguma das 4 areas
                    JogadorProximo = true;
                    AtacandoJogador(); 
                //Se a Identidade for 1
                if(tempTrigger.Trigger == 4 ){
                    ProximoLocal = 1;
                    IndoParaLocal1();    
                
                }
                if(tempTrigger.Trigger == 1){
                    ProximoLocal = 4;
                    IndoParaLocal4(); 
                
                }
                if(tempTrigger.Trigger == 2){
                    ProximoLocal = 2;
                    IndoParaLocal2();
                
                }
                if (tempTrigger.Trigger == 3){
                    ProximoLocal = 3;
                    IndoParaLocal3();
                }
            }   
        }
    }

    void AtacandoJogador(){
        
        //Quando o jogador estiver dentro da visao olhe para o jogador com a mira
        Mira.transform.LookAt(Jogador.transform);
        LerpRotacao.transform.LookAt(Jogador.transform);
           
        Vector3 JogadorSemY = new Vector3(Jogador.transform.position.x,this.transform.position.y,Jogador.transform.position.z);
        LerpAngle = Mathf.LerpAngle(Rotacao.transform.eulerAngles.y,LerpRotacao.transform.eulerAngles.y,VelocidadeRotacao*Time.deltaTime);
        Rotacao.transform.eulerAngles = new Vector3(0,LerpAngle,0);
        

        
    }

    void Patrulha(){

    }

    void Dandotiro(){
        if(JogadorProximo && ChegouNoLocal){
            NovaNoz = Instantiate(Nozes,Mira.transform.position,Quaternion.identity);
            ProjetilEsquiloAtirador temProjetilEsqAtirador = NovaNoz.GetComponent<ProjetilEsquiloAtirador>();
            temProjetilEsqAtirador.VelocidadeProjetil = VelocidadeProjetil;
            NovaNoz.transform.eulerAngles = Mira.transform.eulerAngles;
        }
    }

    void IndoParaLocal1(){   
        this.transform.position = Vector3.MoveTowards(this.transform.position,Locais[0].transform.position,VelocidadeEsquilo*Time.deltaTime);  
    }

    void IndoParaLocal2(){
        this.transform.position = Vector3.MoveTowards(this.transform.position,Locais[1].transform.position,VelocidadeEsquilo*Time.deltaTime);           
    }

    void IndoParaLocal3(){   
            this.transform.position = Vector3.MoveTowards(this.transform.position,Locais[2].transform.position,VelocidadeEsquilo*Time.deltaTime);                  
    }

    void IndoParaLocal4(){       
            this.transform.position = Vector3.MoveTowards(this.transform.position,Locais[3].transform.position,VelocidadeEsquilo*Time.deltaTime);                   
    }

    void AtivandoDegrau(){
        Degrau.transform.position = PoseFinalDegrau.transform.position; 
        GameObject.Destroy(this.gameObject); 
    }

}
