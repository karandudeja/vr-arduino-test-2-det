using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad4control : MonoBehaviour
{
	public AudioSource myAudioSource;
	public AudioClip myAudioClip;
	
	
	// Start is called before the first frame update
	void Start()
	{
        
	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<Renderer>().material.color = new Color(0.87f, 0.6f, 0.17f); //C sharp
	}
    
	public void updatePad4fn(int forceRecv){
		//older
		/*if(forceRecv > 1 && !myAudioSource.isPlaying){
		myAudioSource.PlayOneShot(myAudioClip1);
		if(myAudioSource.isPlaying){
		GetComponent<Renderer>().material.color = new Color(0.72f, 0.47f, 0.066f); //C sharp
		}
		}*/
		
		
		// for light sensor
		if(forceRecv > 30 && !myAudioSource.isPlaying){
			myAudioSource.PlayOneShot(myAudioClip);
			if(myAudioSource.isPlaying){
				GetComponent<Renderer>().material.color = new Color(0.9f, 0.1f, 0.1f); //C sharp
			}
		}
	}
}
