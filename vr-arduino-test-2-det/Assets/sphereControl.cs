using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereControl : MonoBehaviour
{
	
	public float verticalInput;
	public int speed = 15;
	public string lightLED = "b";
	public GameObject colliderPlane;
	public int colliderCount = 10;
	int past_state=-3;

	
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
	    verticalInput = Input.GetAxis("Vertical");
	    transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
	    float posSphereZ = transform.position.z;
	    char current_picture='y';
	    
	    float current_state = posSphereZ;
	    
	    // OTHER LOGIC
	    /*if (posSphereZ>0){
	    	current_picture='b';
	    }
	    
	    if (past_picture!=current_picture){
		    FindObjectOfType<SerialKiller>().UpdateLEDArduino(current_picture);
		    past_picture=current_picture;
	    }*/
	    
	    if((past_state >0)&&(current_state<=0)){
	    	past_state=-3;
	    	lightLED = "y";
		    Debug.Log("Trying to Write to Arduino! :: " + lightLED);
		    FindObjectOfType<SerialKiller>().UpdateLEDArduino(lightLED);
	    }
	    else if((past_state<=0)&&(current_state >0)){
	    	past_state=3;
	    	lightLED = "b";
		    Debug.Log("Trying to Write to Arduino! ::" + lightLED);
		    FindObjectOfType<SerialKiller>().UpdateLEDArduino(lightLED);
	    }else{
	    	//Debug.Log("DO NOTHING");
	    }
	    
    }
	  
	private void OnTriggerEnter(Collider other){
		colliderCount++;
		Debug.Log("ColliderCount; " + colliderCount);
	}
		
    
}
