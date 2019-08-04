using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ControleJogador))]
public class ControleJogadorEditor : Editor
{
    private ControleJogador Jogador;
    private SerializedObject SoJogador;

    
    //Variaveis de Movimentacao
    private SerializedProperty VelocidadeJogador;
    private SerializedProperty VelocidadeRotacao;
    private SerializedProperty EscalaGravidade;
    private SerializedProperty Pulo;
    private SerializedProperty ContadorPulo;
    private SerializedProperty PivorRotacao;
    private SerializedProperty DirecaoCamera;
    private SerializedProperty PuloDuplo;

    // Variaveis de Estado 

    private SerializedProperty Pulando;
    private SerializedProperty Agachado;
    private SerializedProperty Visivel;
    private SerializedProperty Invulneravel;
    private SerializedProperty Controle;
    private SerializedProperty Off_Invulneravel;
    

    //Variaveis de Combate 

    private SerializedProperty PivorTiro;
    private SerializedProperty Pedra;
    private SerializedProperty InimigoMaisProximo;
    private SerializedProperty DentroAgro;
    private SerializedProperty CarregandoTiro;
    private SerializedProperty Mira;
    private SerializedProperty Agro;
    private SerializedProperty Seletor;
    private SerializedProperty VidaJogador;
    private SerializedProperty GirarMachado;
    private SerializedProperty PivorMachado;  
    private SerializedProperty QuantidadeDePedras;
    private SerializedProperty TextoQuantidadeBalas;
    private SerializedProperty TextoVidaJogador;
    private SerializedProperty TextoVidaInimigo;
    

    



    //Variaveis de Carga

    private SerializedProperty PodeCarregar;
    private SerializedProperty PegouCarga;
    private SerializedProperty ObjetoCarregavel;
    private SerializedProperty PivorCarga;
    private SerializedProperty ColliderCarga;
    private SerializedProperty Selecao;













    void OnEnable() {
        
        Jogador = (ControleJogador)target;
        SoJogador = new SerializedObject(Jogador);

        //Pegar variaveis do script Alvo 
        //Aba 1
        VelocidadeJogador = SoJogador.FindProperty("VelocidadeJogador");
        VelocidadeRotacao = SoJogador.FindProperty("VelocidadeRotacao");
        EscalaGravidade = SoJogador.FindProperty("EscalaGravidade");
        Pulo = SoJogador.FindProperty("Pulo");
        ContadorPulo = SoJogador.FindProperty("ContadorPulo");
        PivorRotacao = SoJogador.FindProperty("PivorRotacao");
        DirecaoCamera = SoJogador.FindProperty("DirecaoCamera");
        PuloDuplo = SoJogador.FindProperty("PuloDuplo");

        //Aba2 
        Pulando = SoJogador.FindProperty("Pulando");
        Agachado = SoJogador.FindProperty("Agachado");
        Visivel = SoJogador.FindProperty("Visivel");
        Invulneravel = SoJogador.FindProperty("Invulneravel");
        Controle = SoJogador.FindProperty("Controle");
        Off_Invulneravel = SoJogador.FindProperty("Off_Invulneravel");

        //Aba3 
        PivorTiro = SoJogador.FindProperty("PivorTiro");
        Pedra = SoJogador.FindProperty("Pedra");
        InimigoMaisProximo = SoJogador.FindProperty("InimigoMaisProximo");
        DentroAgro = SoJogador.FindProperty("DentroAgro");
        CarregandoTiro = SoJogador.FindProperty("CarregandoTiro");
        Mira  = SoJogador.FindProperty("Mira");
        Agro = SoJogador.FindProperty("Agro");
        Seletor = SoJogador.FindProperty("Seletor"); 
        VidaJogador = SoJogador.FindProperty("VidaJogador");
        GirarMachado =SoJogador.FindProperty("GirarMachado");
        PivorMachado = SoJogador.FindProperty("PivorMachado");
        QuantidadeDePedras = SoJogador.FindProperty("QuantidadeDePedras");
        TextoQuantidadeBalas = SoJogador.FindProperty("TextoQuantidadeBalas");
        TextoVidaJogador = SoJogador.FindProperty("TextoVidaJogador");
        TextoVidaInimigo = SoJogador.FindProperty("TextoVidaInimigo");
        
        

        //Aba4 
        PodeCarregar = SoJogador.FindProperty("PodeCarregar");
        PegouCarga = SoJogador.FindProperty("PegouCarga");
        ObjetoCarregavel = SoJogador.FindProperty("ObjetoCarregavel");
        PivorCarga = SoJogador.FindProperty("PivorCarga");
        ColliderCarga = SoJogador.FindProperty("ColliderCarga");
        Selecao = SoJogador.FindProperty("Selecao");

        
        
        

 

    }
    public override void OnInspectorGUI(){
    
    DrawDefaultInspector();

    SoJogador.Update();
    EditorGUI.BeginChangeCheck();

        Jogador.ValorAbaAtual = GUILayout.Toolbar(Jogador.ValorAbaAtual,new string []{"Movimento","Estados","Combate","Carga"});

        switch(Jogador.ValorAbaAtual){
            case 0:
                Jogador.AbaAtual = "Movimento";
                break;
            case 1:
                Jogador.AbaAtual = "Estados";
                break;
            case 2:
                Jogador.AbaAtual = "Combate";
                break;
            case 3:
                Jogador.AbaAtual = "Carga";
                break;

        }

    if(EditorGUI.EndChangeCheck()){
        SoJogador.ApplyModifiedProperties();
        GUI.FocusControl(null);
    }
        
    EditorGUI.BeginChangeCheck();

        switch(Jogador.AbaAtual){
            case "Movimento":

                
                EditorGUILayout.LabelField("Variaveis de Movimentação ",EditorStyles.boldLabel);
                EditorGUILayout.Space();
                
                //Variaveis de Movimentacao
                EditorGUILayout.PropertyField(VelocidadeJogador);
                EditorGUILayout.PropertyField(VelocidadeRotacao);
                EditorGUILayout.PropertyField(EscalaGravidade);
                EditorGUILayout.PropertyField(Pulo);
                EditorGUILayout.PropertyField(ContadorPulo);
                EditorGUILayout.PropertyField(PuloDuplo);
                
                EditorGUILayout.Space();

                //Objectos relacionados
                EditorGUILayout.LabelField("Objetos Relacionados",EditorStyles.boldLabel);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(PivorRotacao);
                EditorGUILayout.PropertyField(DirecaoCamera);

                break;

            case "Estados":

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(Pulando);
                EditorGUILayout.PropertyField(Agachado);
                EditorGUILayout.PropertyField(Visivel);
                EditorGUILayout.PropertyField(Invulneravel);
                EditorGUILayout.PropertyField(Controle);
                EditorGUILayout.PropertyField(Off_Invulneravel);


                break;

            case "Combate":
                
                EditorGUILayout.LabelField("Variaveis de Combate",EditorStyles.boldLabel);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(VidaJogador);
                EditorGUILayout.PropertyField(Agro);
                EditorGUILayout.PropertyField(CarregandoTiro);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Inimigos",EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(InimigoMaisProximo);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Objetos Relacionados",EditorStyles.boldLabel);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(TextoVidaInimigo);
                EditorGUILayout.PropertyField(TextoVidaJogador);
                EditorGUILayout.PropertyField(TextoQuantidadeBalas);
                EditorGUILayout.PropertyField(QuantidadeDePedras);
                EditorGUILayout.PropertyField(PivorTiro);
                EditorGUILayout.PropertyField(Pedra);
                EditorGUILayout.PropertyField(Seletor);
                EditorGUILayout.PropertyField(Mira);
                EditorGUILayout.PropertyField(GirarMachado);
                EditorGUILayout.PropertyField(PivorMachado);

                break;
            
            case "Carga":
                EditorGUILayout.LabelField("Variaveis de Carga",EditorStyles.boldLabel);
                
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(PodeCarregar);
                EditorGUILayout.PropertyField(PegouCarga);
                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("Objetos Relacionados",EditorStyles.boldLabel);
                
                EditorGUILayout.PropertyField(ObjetoCarregavel);
                EditorGUILayout.PropertyField(PivorCarga);
                EditorGUILayout.PropertyField(ColliderCarga);
                EditorGUILayout.PropertyField(Selecao);


                break;

        }
        
    if(EditorGUI.EndChangeCheck()){
        SoJogador.ApplyModifiedProperties();        
    }

    }

    void OnSceneGUI() {

        //Desenha Agro do jogador 
        Handles.color = Color.green;
        Handles.DrawWireDisc(Jogador.transform.position,new Vector3(0,2,0),Jogador.Agro);
    }

}
