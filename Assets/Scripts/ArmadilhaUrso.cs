using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilhaUrso : MonoBehaviour
{
    GameObject PresaA;
    GameObject PresaB;
    Animator ControladorPresaA;
    Animator ControladorPresaB;


    void Start()
    {
    PresaA = this.transform.Find("PresaA").gameObject;
    PresaB = this.transform.Find("PresaB").gameObject;
    ControladorPresaA = PresaA.GetComponent<Animator>();
    ControladorPresaB = PresaB.GetComponent<Animator>();
   
    }

    void OnTriggerEnter(Collider other){
    if(other.gameObject.name == "Jogador"){
    InvokeRepeating("AtivandoArmadilha",0.2f,0);    
    }
    }
    void Update()
    {
        
    }
    
    void AtivandoArmadilha(){
        ControladorPresaA.SetBool("PresaAtiva",true);
        ControladorPresaB.SetBool("PresaAtiva",true);
    }
}
