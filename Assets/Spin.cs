using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
    }

    void OnCollisionStay( Collision collisionInfo){
        Debug.Log("OnCollisionStay");
        CharacterController target = collisionInfo.gameObject.GetComponent<CharacterController>();
        target.Move(new Vector3(0f,0f,0.01f));
    }

}
