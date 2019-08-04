using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersEsquiloAtirador : MonoBehaviour
{
public int Trigger;
public bool Dentro;


void OnTriggerEnter(Collider other) {
if(other.gameObject.name == "Jogador"){
Dentro = true;
}    
}

void OnTriggerExit(Collider other) {
if(other.gameObject.name == "Jogador"){
Dentro = false;
}      
}





}
