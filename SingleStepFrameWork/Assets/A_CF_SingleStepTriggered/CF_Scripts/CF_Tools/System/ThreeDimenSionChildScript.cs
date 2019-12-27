using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDimenSionChildScript : ThreeDimesionTrigger {

	// Use this for initialization
	void Start () {
		
	}

    public override void OpenInterface()
    {
        base.OpenInterface();
        gameObject.SetActive(true);
    }

    public override void CloseInterface()
    {
        base.CloseInterface();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.GetComponent<PlayerMoveMentScript>()!=null)
        //{
        //    scriptEventIn.Invoke();
        //    CloseInterface();
        //}
       
    }

    




}
