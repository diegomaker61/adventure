using UnityEngine;

public class SelecaoFinalCarga : MonoBehaviour
{
    SpriteRenderer Renderer; 
    public bool PodeDescarregar;
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider other) {
        
        PodeDescarregar = false;
    }

    void OnTriggerExit(Collider other) {
        
        PodeDescarregar = true;
    }


    void Update()
    {
        if(PodeDescarregar){
            Renderer.color = new Color32(0,255,0,130);
        }else{
            Renderer.color = new Color32(255,0,0,130);
        }



    }
}
