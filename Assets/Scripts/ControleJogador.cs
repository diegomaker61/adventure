using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class ControleJogador : MonoBehaviour
{   

    //Movimentacao
    public float VelocidadeJogador;
    public float VelocidadeRotacao;
    public CharacterController Controlador;
    public GameObject DirecaoCamera;
    public Vector3 DirecaoFinal;
    public float EscalaGravidade;
    public float Pulo;
    public int ContadorPulo;
    public GameObject PivorRotacao;
    public bool PuloDuplo;
    public bool PuloSimples;
    public bool NaEscada;
    public GameObject CameraPrincipal;
    


    //Estados 
    public bool Pulando;
    public bool Agachado;
    bool SofreuDano;
    bool AcabouSofrerDano;
    public bool Visivel;
    public bool Invulneravel;
    public bool Controle;
    public bool Off_Invulneravel;


    //Combate 
    MeshRenderer RenderJogador;
    public GameObject PivorTiro;
    public GameObject Pedra;
    GameObject NovaPedra;
    public GameObject InimigoMaisProximo;   
    public List <GameObject> TodosInimigos;
    public List <GameObject> DentroAgro;
    public bool CarregandoTiro;
    public Transform Mira;
    public float Agro;
    public GameObject Seletor;
    public float VelocidadePedra;
    public int VidaJogador;
    public bool GirarMachado;
    public GameObject PivorMachado;
    public bool DesligandoGirarMachado;
    public int QuantidadeDePedras;
    public TextMeshProUGUI TextoQuantidadeBalas;
    public bool DanoSimples;
    public TextMeshProUGUI TextoVidaJogador;
    public TextMeshProUGUI TextoVidaInimigo;


    //Carga 
    public bool PodeCarregar;
    public bool PegouCarga;
    public GameObject ObjetoCarregavel;
    public GameObject PivorCarga;  
    public BoxCollider ColliderCarga;
    public GameObject Selecao;
    SpriteRenderer SelecaoRender;
    SelecaoFinalCarga tempSelecao;


    //Variaveis de Edicao
    [HideInInspector]
    public int ValorAbaAtual;
    public string AbaAtual;


    void Start()
    {
        Controlador = this.GetComponent<CharacterController>();
        RenderJogador = this.GetComponent<MeshRenderer>();
        InvokeRepeating("Piscar",0,0.1f);
        
        //Adiciona todos os inimigos a lista
        foreach (GameObject Inimigo in GameObject.FindGameObjectsWithTag("Inimigo")){
        TodosInimigos.Add(Inimigo);            
        }

        CameraPrincipal = GameObject.Find("CameraPrincipal");
        SelecaoRender = Selecao.GetComponent<SpriteRenderer>();
        tempSelecao = Selecao.GetComponent<SelecaoFinalCarga>();

        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag =="Inimigo" && Invulneravel == false){
            Debug.Log("ColidiuInimigo");
            SofreuDano = true;
            Controle = false;
        }

        if(other.gameObject.tag == "Carregavel"){
            PodeCarregar = true;
            ObjetoCarregavel = other.gameObject;
            
        }

        if(other.gameObject.tag == "Pedra"){
            QuantidadeDePedras+=1;
            GameObject.Destroy(other.gameObject);
        }
        
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag=="Carregavel"){
            PodeCarregar = false;
            
        }

    }


    void Update() {

        //Atualizando Quantidade de balas na Ui
        TextoVidaJogador.text = VidaJogador.ToString() + "Vidas";
        TextoQuantidadeBalas.text = QuantidadeDePedras.ToString()+" Pedras";

        //Jogador Pulando 
        if(Controlador.isGrounded){
            ContadorPulo=0;
            Pulando = false;
        }
        if(Input.GetButtonDown("ButtonCross")&& ContadorPulo<2){
            if(Controlador.isGrounded){
                PuloSimples = true;
                Pulando = true;
                ContadorPulo+=1;
            }else{
                if(ContadorPulo == 1){
                PuloDuplo = true;
                Pulando = true;
                ContadorPulo+=1;   
                }
            }     
        }

        
    
        //Visibilidade Jogador 
        if(Visivel){
            RenderJogador.enabled = true;
        }else{
            RenderJogador.enabled = false;
        }
        

        /*********************************  ATIRANDO EM INIMIGOS  *****************************************************/
        
        //Mirando
        if(Input.GetButton("ButtonR1")){
            CarregandoTiro = true; 
        }else{
            CarregandoTiro = false;    
        }


        //Colocando inimigos dentro do agro de acordo com a distancia 
        if(TodosInimigos.Count>0){
            foreach (GameObject InimigosProximos in TodosInimigos){
                if(InimigosProximos!=null){
                float DistanciaoDentroAgro = Vector3.Distance(this.transform.position,InimigosProximos.transform.position); 
                    if(DistanciaoDentroAgro<=Agro && !DentroAgro.Contains(InimigosProximos)){
                    DentroAgro.Add(InimigosProximos);
                    }     
                }           
            }
        }

        //RemovendoInimigos do Agro Caso eles estejam longe 
        if(DentroAgro.Count>0){
            foreach (GameObject InimigosLonge in DentroAgro){
                if(InimigosLonge!=null){
                float Distancia = Vector3.Distance(InimigosLonge.transform.position,this.transform.position);
                    if(Distancia>Agro){
                    DentroAgro.Remove(InimigosLonge);
                    break;    
                    }
                }
            }
        }

        //Removendo Inimigos do Agro caso eles tenham sido derrotados 
        if(DentroAgro.Count>0){
            foreach(GameObject InimigosDentroAgro in DentroAgro ){
                if(InimigosDentroAgro == null){
                    DentroAgro.Remove(InimigosDentroAgro);
                    break;
                }
            }
        }



        if(CarregandoTiro){

            //Mostrando vida do inimigo 
            if(InimigoMaisProximo!=null){
                EsquiloAtirador tempEsquiloAtirador = InimigoMaisProximo.GetComponent<EsquiloAtirador>();
                TextoVidaInimigo.text = "Vida Inimigo: " + tempEsquiloAtirador.VidaEsquilo.ToString();
            }


            //Achando o inimigo mais proximo dentro do Agro
            float DistanciaMinima = Mathf.Infinity;  
            if(DentroAgro.Count>0){
                foreach (GameObject Inimigo in DentroAgro){
                    if(Inimigo!=null){
                    float DistanciaInimigo = Vector3.Distance(this.transform.position,Inimigo.transform.position);
                                    
                        if(DistanciaInimigo<DistanciaMinima){
                            DistanciaMinima = DistanciaInimigo;
                            InimigoMaisProximo = Inimigo;
                            if(Input.GetButtonDown("ButtonSquare")){
                                if(QuantidadeDePedras>0 && !Invulneravel){
                                        NovaPedra = Instantiate(Pedra,PivorTiro.transform.position,Quaternion.identity) as GameObject; 
                                        NovaPedra.transform.eulerAngles = PivorRotacao.transform.eulerAngles;
                                        PedraAtirada tempPedraAtirada = NovaPedra.GetComponent<PedraAtirada>();
                                        tempPedraAtirada.AtiradaPeloJogador = true;
                                        QuantidadeDePedras-=1;
                                                     
                                }
                            }   
                        }
                    }
                }   
            }else{
                CarregandoTiro = false;
                TextoVidaInimigo.text = "Sem Inimigo";
            }


            if(InimigoMaisProximo!=null){
                Inimigos tempInimigoSelecionado = InimigoMaisProximo.GetComponent<Inimigos>();
                Seletor.SetActive(true);
                Seletor.transform.position = tempInimigoSelecionado.PivorSelecao.transform.position;
            }
            
            //Atirando 
            

        }else{

            Seletor.SetActive(false);
        } 

        /*********************************  CARREGANDO OBJETOS  *****************************************************/  
        
        //Possibilida de Carregar Objeto
        if(PodeCarregar){
            if(Input.GetButtonDown("ButtonTriangle")){ 
                PegouCarga = true;
                ColliderCarga = ObjetoCarregavel.transform.GetChild(0).GetComponent<BoxCollider>();
                ColliderCarga.enabled = false;
            }  
        }

        if(PegouCarga){
            SelecaoRender.enabled = true;
                //Segurando objeto
                ObjetoCarregavel.transform.position = PivorCarga.transform.position;
                ObjetoCarregavel.transform.eulerAngles = PivorRotacao.transform.eulerAngles;
                
            
            //Soltando objeto
            if(Input.GetButtonDown("ButtonTriangle") && !PodeCarregar && tempSelecao.PodeDescarregar){
                ObjetoCarregavel.transform.position = Selecao.transform.position;
                ColliderCarga.enabled = true;
                PegouCarga = false;
            } 
            
        }else{
            SelecaoRender.enabled = false;
        }



        /*********************************  ATAQUE DE CORPO A CORPO  *****************************************************/

        if(Input.GetButtonDown("ButtonSquare") && !Input.GetButton("ButtonR1")){
            PivorMachado.SetActive(true);
            GirarMachado = true;
            DesligandoGirarMachado =true;

        }

        if(GirarMachado){
            PivorMachado.transform.Rotate(Vector3.up * 2000*Time.deltaTime);
            if(DesligandoGirarMachado){
                InvokeRepeating("PararGiroMachado",1,0);
                DesligandoGirarMachado = false;
            }
        }

        /*********************************  RECEBENDO DANO SIMPLES  *****************************************************/

        if(DanoSimples){
            Invulneravel = true;
        }


    }   

        
        

        

    void FixedUpdate(){
        
    if(!SofreuDano && Controle){    
        
        //Input do joystick esquerdo Movimentacao
        DirecaoFinal = new Vector3(Input.GetAxis("LeftJoystickHorizontal"),DirecaoFinal.y,-Input.GetAxis("LeftJoystickVertical"));

        //Caso o Jogador esteja no chao
        if(Controlador.isGrounded){
            DirecaoFinal.y = -1f;           
                         
        }

        if(PuloSimples){
           
            DirecaoFinal.y = Pulo;
            PuloSimples = false;
        }

        if(PuloDuplo){
            DirecaoFinal.y = Pulo;
            PuloDuplo = false;
        }
 

    DirecaoFinal = DirecaoCamera.transform.TransformDirection(DirecaoFinal);

    }
    
    if(SofreuDano){

        DirecaoFinal = new Vector3(0,2,-0.5f);
        DirecaoFinal = PivorRotacao.transform.TransformDirection(DirecaoFinal);
        DirecaoFinal.y = DirecaoFinal.y + (Physics.gravity.y*EscalaGravidade*Time.deltaTime);
        Controlador.Move(DirecaoFinal*VelocidadeJogador*Time.deltaTime);
        Invulneravel = true;
        InvokeRepeating("Caindo",0.1f,0);

    }

    if(!NaEscada){
        DirecaoFinal.y = DirecaoFinal.y + (Physics.gravity.y*EscalaGravidade*Time.deltaTime);
        Controlador.Move(DirecaoFinal*VelocidadeJogador*Time.deltaTime);
    }

    if(Controle){
        if(!CarregandoTiro){
            //Rotaciona o jogador para onde ele esta andando
            if(Input.GetAxis("LeftJoystickHorizontal")!=0 || Input.GetAxis("LeftJoystickVertical")!=0 ){
                Vector3 DirecaoFinalSemY = new Vector3(DirecaoFinal.x,0,DirecaoFinal.z);
                Quaternion RotacaoFinal = Quaternion.LookRotation(DirecaoFinalSemY);
                PivorRotacao.transform.rotation = Quaternion.Slerp(PivorRotacao.transform.rotation, RotacaoFinal,VelocidadeRotacao*Time.deltaTime);
            }
        }else{
            //Mira no inimigo mais proximo
            if(DentroAgro.Count>0 && InimigoMaisProximo!= null){
                Vector3 DirecaoFinalSemY = new Vector3(InimigoMaisProximo.transform.position.x,PivorRotacao.transform.position.y,InimigoMaisProximo.transform.position.z);
                Mira.transform.LookAt(DirecaoFinalSemY);
                PivorRotacao.transform.rotation = Quaternion.Slerp(PivorRotacao.transform.rotation,Mira.transform.rotation,VelocidadeRotacao*Time.deltaTime);
            }
        }   
    }
    
    
    /* 
    if(Input.GetButton("ButtonCircle")){
        Agachado =true;
    }else{
        Agachado = false;
    }
    
    if(Agachado){
        this.transform.localScale = new Vector3(1,0.5f,1);
        VelocidadeJogador = 2; 
    }else{
        this.transform.localScale = new Vector3(1,1,1); 
        VelocidadeJogador = 5;
    }
    */

    if(AcabouSofrerDano){
        if(Controlador.isGrounded){
            Controle = true;
            AcabouSofrerDano = false;
        }
    }

    if(Invulneravel){
        if(!Off_Invulneravel){
            InvokeRepeating("DesligarInvulnerabilidade",3,0);
            Off_Invulneravel = true;    
        }
        Physics.IgnoreLayerCollision(10,0,true);

        }

    }//Final Fixed Update


    void Caindo(){
        AcabouSofrerDano = true;
        SofreuDano = false;
    }

    void Piscar(){
        if(Invulneravel){ 
            Visivel = !Visivel;           
        }    
    }

    void DesligarInvulnerabilidade(){
        Invulneravel = false;
        Visivel = true;
        Physics.IgnoreLayerCollision(10,0,false);
        Off_Invulneravel = false;
        DanoSimples = false;
    }

    void PararGiroMachado(){
        GirarMachado = false;
        PivorMachado.SetActive(false);
    }

    
    

    
        
    



}
