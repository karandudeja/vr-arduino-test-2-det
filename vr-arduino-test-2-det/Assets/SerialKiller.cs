﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialKiller : MonoBehaviour
{
	public string writeVal;
	
	public cubeControl cubeControl1;
	public Pad1control pad1Controller;
	public Pad2control pad2Controller;
	public Pad3control pad3Controller;
	public Pad4control pad4Controller;
			
	public string lightLEDSend = "b";
	public float angleSend2Cylinder = 0.0f;
	public int speedSend2Cube = 0;
	public int tapValueForCube = 0;
	public int potValRecv = 500;
	public int multiplierSend2Cube = 1;
	public float verticalInput;
	public string recvFromArduino;
	
	public bool useAutomaticPort = true;
	public string serialPortId = "COM4";
	float readingPeriodSecs = 1f;
	string lastReadValue = "";
	
	public GameObject sphere;
	public GameObject cylinder;

	SerialPort sp;
	float next_time;
	
	int piezo1valRecv = 0;
	int piezo2valRecv = 0;
	int piezo3valRecv = 0;
	int piezo4valRecv = 0;
	
	[SerializeField] int directionFromArduino = 0;
	[SerializeField] int forceVal1FromArduino = 0;
	[SerializeField] int forceVal2FromArduino = 0;
	[SerializeField] int forceVal3FromArduino = 0;
	[SerializeField] int forceVal4FromArduino = 0;
	
	void Start()
	{
	    
		
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
		
		try {
					
					if (sp.IsOpen)
					{
						// Read values
						
						recvFromArduino = sp.ReadLine();
						print(recvFromArduino);
				   
						string[] splitStringDataArray =  recvFromArduino.Split(",");
			    
						potValRecv = int.Parse(splitStringDataArray[0]);
						
						piezo1valRecv = int.Parse(splitStringDataArray[0]);
						//speedSend2Cube = speedRecv; // older was divided by 100
						forceVal1FromArduino = piezo1valRecv; //sent to pad 1
						
						piezo2valRecv = int.Parse(splitStringDataArray[1]);
						//tapValueForCube = forceRecv/100;
						forceVal2FromArduino = piezo2valRecv; // sent to pad2
						
						piezo3valRecv = int.Parse(splitStringDataArray[2]);
						forceVal3FromArduino = piezo3valRecv; // sent to pad3
						
						piezo4valRecv = int.Parse(splitStringDataArray[3]);
						forceVal4FromArduino = piezo4valRecv; // sent to pad4
			    		
						if(potValRecv > 512){
							multiplierSend2Cube = -1;
							directionFromArduino = multiplierSend2Cube;
						}
						else{
							multiplierSend2Cube = 1;
							directionFromArduino = multiplierSend2Cube;
						}
						
						// Send values of Direction to Cube Script as received from Arduino
						//FindObjectOfType<cubeControl>().UpdateCubeLocation(directionFromArduino);
						
						// Send values of Speed to Cube Script as received from Arduino
						//FindObjectOfType<cubeControl>().UpdateCubeSpeed(forceValFromArduino, forceValue2FromArduino);
						
						//maurice code
						//cubeControl1.UpdateCubeSpeed(forceValFromArduino, forceValue2FromArduino);
						
						
						//pad1Controller.updatePad1fn(forceVal1FromArduino);
						//pad2Controller.updatePad2fn(forceVal2FromArduino);
						//pad3Controller.updatePad3fn(forceVal3FromArduino);
						//pad4Controller.updatePad4fn(forceVal4FromArduino);
						
						if(forceVal1FromArduino != 0){
							pad1Controller.updatePad1fn(forceVal1FromArduino);
							//forceVal1FromArduino = 0;
						}
						if(forceVal2FromArduino != 0){
							pad2Controller.updatePad2fn(forceVal2FromArduino);
						}
						if(forceVal3FromArduino != 0){
							pad3Controller.updatePad3fn(forceVal3FromArduino);
						}
						
						if(forceVal4FromArduino != 0){
							pad4Controller.updatePad4fn(forceVal4FromArduino);
						}
						
						
					}
					
				if(Input.GetKeyDown(KeyCode.Q)){
					sp.Close();
					Debug.Log("Serial Port Closed !!!!");
				}
					
			}
			
		catch (System.Exception e)
			{
				Debug.Log(e);
			}

			
			next_time = Time.time + readingPeriodSecs;
			

	} //end of Update Function
	
	public void UpdateLEDArduino(string lightValRecv){
		
		lightLEDSend = lightValRecv;
		if (sp.IsOpen)
		{
			sp.Write(lightLEDSend);
		}
	}
	
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
			Debug.Log(availablePorts);
			foreach (string mysps in availablePorts)
			{
				print(mysps);
			}
			serialPortId = availablePorts[1];
		}
	}

	private void TryToOpenSerial()
	{
		sp = new SerialPort(serialPortId, 9600);
		if (!sp.IsOpen)
		{
			print("Opening " + serialPortId + " baud 9600");
			sp.Open();
			
			sp.ReadTimeout = 100;
			//sp.Handshake = Handshake.None;
			if (sp.IsOpen)
			{ 
				print("Open");
			}
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



