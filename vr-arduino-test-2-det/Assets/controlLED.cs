using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class controlLED : MonoBehaviour
{
	public string writeVal;
			
	public string lightLED = "";
	public int speed = 15;
	public float verticalInput;
	
	SerialPort arduinoPort = new SerialPort("COM15", 9600);
	
	
	public bool useAutomaticPort = true;
	public string serialPortId = "COM4";
	float readingPeriodSecs = 1f;
	string lastReadValue = "";

	SerialPort sp;
	float next_time;
	
	void Start()
	{
	    
		//Initiate the Serial stream
	    
		//arduinoPort.Open();
		//arduinoPort.ReadTimeout = 100;
		//arduinoPort.WriteTimeout = 100;
		
		next_time = Time.time;

		if(useAutomaticPort)
		{
			AssignAutomaticPort();
		}
        
		TryToOpenSerial();

	}

	// Update is called once per frame
	void Update()
	{
		//receivedVal = data_stream.ReadLine();
		//receivedVal = arduinoPort.ReadLine();
	    
		//string[] splitStringDataArray =  receivedVal.Split(",");
	    
		//stepSize = int.Parse(receivedVal);
		//stepSize = int.Parse(splitStringDataArray[0]);
		//int speedRecv = int.Parse(splitStringDataArray[1]);
		//speed = speedRecv/80;
		verticalInput = Input.GetAxis("Vertical");
		transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
		float pos = transform.position.z;
		//Debug.Log(pos);
		
		if(pos > -0.33){
			lightLED = "b";
			//arduinoPort.Open();
			//arduinoPort.Write(lightLED);
			
		}
		else{
			lightLED = "y";
			//arduinoPort.Open();
			//arduinoPort.Write(lightLED);
			
		}
		
		if (Time.time > next_time)
		{
			if (sp.IsOpen)
			{
				// Read values
				//ReadValuesFromSerial();
				
				sp.Write(lightLED);
				Debug.Log("Writing " + lightLED);
				//CloseSerial();
			}
			else
			{
				TryToOpenSerial();
			}

			//if (sp.IsOpen)
			//{
			//    // Write values in the port
			//    print("Writing " + ii);
			//    sp.Write((ii.ToString()));
			//}
			next_time = Time.time + readingPeriodSecs;
			//if (++ii > 9) ii = 0;
		}
		
		
		
	    
		//arduinoPort.Write(lightLED);
		//Debug.Log(stepSize);
		//transform.Translate(Vector3.right * multiplier * Time.deltaTime * speed);
	    
		//rb.AddForce(0, 0, float.Parse(datas[0])* sensitivity* Time.deltaTime, ForceMode.VelocityChange); 		
		//rb.AddForce(float.Parse(datas[1]) *sensitivity *Time.deltaTime, 0, 0, ForceMode. VelocityChange); 			
		//transform. Rotate(0, float.Parse(receivedVal), 0);

	}
	
	
	//int ii = 0;
	private void OnDisable()
	{
		CloseSerial();
	}


	private void AssignAutomaticPort()
	{
		string[] availablePorts = SerialPort.GetPortNames();
		if (availablePorts.Length == 0)
		{
			Debug.Log("COM ports were not found");
		}
		else
		{
			Debug.Log("Found COM ports:");
			foreach (string mysps in availablePorts)
			{
				print(mysps);
			}
			serialPortId = availablePorts[3];
		}
	}

	private void TryToOpenSerial()
	{
		sp = new SerialPort(serialPortId, 9600);
		if (!sp.IsOpen)
		{
			print("Opening " + serialPortId + " baud 9600");
			sp.Open();
			//sp.ReadTimeout = 100;
			//sp.Handshake = Handshake.None;
			if (sp.IsOpen) { print("Open"); }
		}
	}

	private void CloseSerial()
	{
		if (sp.IsOpen)
			sp.Close();
	}

	private void ReadValuesFromSerial()
	{
		lastReadValue = sp.ReadLine(); //Read the information
		string[] vec3 = lastReadValue.Split(','); //My arduino script returns a 3 part value (IE: 12,30,18)
		if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "") //Check if all values are recieved
		{
			Debug.Log("Received vect3: " + vec3[0] + ", " + vec3[1] + ", " + vec3[2]);
			sp.BaseStream.Flush();              //Clear the serial information so we assure we get new information.
		}
	}
}


