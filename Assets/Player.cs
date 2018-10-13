using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Text score;
    public AudioClip aud;
    private int pScore = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("jewel"))
        {
            AudioSource.PlayClipAtPoint(aud, transform.position);
            pScore++;
            score.text = pScore.ToString();
            Destroy(other.gameObject);
        }
    }
}
