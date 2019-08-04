using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletorAlvo : MonoBehaviour
{
    public float VelocidadeRotacao ;
    GameObject Jogador;
    void Start()
    {
    Jogador = GameObject.Find("Jogador");
    }

    
    void Update()
    {
        Vector3 CameraSemY = new Vector3(Camera.main.transform.position.x,this.transform.position.y,Camera.main.transform.position.z);
        this.transform.LookAt(CameraSemY);
    }

    
}
