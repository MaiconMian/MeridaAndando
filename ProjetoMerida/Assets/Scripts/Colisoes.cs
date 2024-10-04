using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisoes : MonoBehaviour
{



    private Rigidbody _rb;
    // Start is called before the first frame update


    private void Awake(){
        _rb = GetComponent<Rigidbody>();
    }
    void OnCollisonEnter(Collision collision){

        switch(collision.gameObject.tag){
            case "Cenario":
            _rb.AddForce(Vector3.back * 10);
            break;

        }

    }
}
