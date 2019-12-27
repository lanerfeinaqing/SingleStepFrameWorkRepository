using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepBigImageScript : MonoBehaviour {

     Text stepText;
     Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        stepText = transform.GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start () {
		
	}

    public void PlayAnimation(string s)
    {
        anim.Play("defaultStepImage");
        anim.Update(0);
        stepText.text = s;
        anim.SetTrigger("anim");
    }

    public void StopAnimation()
    {
        anim.Play("defaultStepImage");
        anim.Update(0);
    }


	
	
}
