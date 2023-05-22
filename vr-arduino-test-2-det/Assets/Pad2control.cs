using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad2control : MonoBehaviour
{
    
	public AudioSource myAudioSource;
	public AudioClip myAudioClip2;
	
	
	// Start is called before the first frame update
	void Start()
	{
        
	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<Renderer>().material.color = new Color(0.87f, 0.6f, 0.17f); //C sharp
	}
    
	public void updatePad2fn(int forceRecv){
		if(forceRecv > 10 && !myAudioSource.isPlaying){
			myAudioSource.PlayOneShot(myAudioClip2);
			GetComponent<Renderer>().material.color = new Color(0.9f, 0.1f, 0.1f); //C sharp
		}
	}
}
