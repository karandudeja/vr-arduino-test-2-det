using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class arduino : MonoBehaviour
{
	//SerialPort data_stream = new SerialPort("COM12", 9600);
	public string receivedVal;
	//public GameObject changePositionObject;
	//public Rigidbody rb;
	public float sensitivity = 0.01f;
			
	public string[] datas;
	private int stepSize = 0;
	public int speed = 5;
	private int multiplier = 1;
	
	SerialPort arduinoPort = new SerialPort("COM15", 9600);
	
    void Start()
    {
	    
	    arduinoPort.Open();
	    arduinoPort.ReadTimeout = 100;
	    //arduinoPort.WriteTimeout = 100;

    }

    // Update is called once per frame
    void Update()
	{
		try {
			
			receivedVal = arduinoPort.ReadLine();
			   
		    string[] splitStringDataArray =  receivedVal.Split(",");
		    
		    //stepSize = int.Parse(receivedVal);
		    stepSize = int.Parse(splitStringDataArray[0]);
		    int speedRecv = int.Parse(splitStringDataArray[1]);
			speed = speedRecv/100;
		    
		    if(stepSize > 512){
		    	multiplier = -1;
		    }
		    else{
		    	multiplier = 1;
		    }
		    
		    //Debug.Log(stepSize);
		    transform.Translate(Vector3.right * multiplier * Time.deltaTime * speed);
		    
		    //rb.AddForce(0, 0, float.Parse(datas[0])* sensitivity* Time.deltaTime, ForceMode.VelocityChange); 		
		    //rb.AddForce(float.Parse(datas[1]) *sensitivity *Time.deltaTime, 0, 0, ForceMode. VelocityChange); 			
		    //transform. Rotate(0, float.Parse(receivedVal), 0);
			}
		catch (System.Exception e)
		{
			Debug.Log(e);
		}
    }
}
