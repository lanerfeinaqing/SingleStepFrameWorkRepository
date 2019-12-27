using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMessageObjManager_CF : MonoBehaviour {

     Color rightAnwser=new Color(0.2196079f, 0.8745099f, 0.2784314f,1.0f);
     Color wrongAnwser=new Color(0.8705883f, 0.2313726f, 0.3058824f,1.0f) ;

     Text branchStoreText;
     Text scoreText;

    public void SetDescribeMessage(string message,int _score)
    {
        Transform thistransform = transform;
        branchStoreText = thistransform.Find("BranchStore").GetComponent<Text>();
        scoreText = thistransform.Find("Score").GetComponent<Text>();

        branchStoreText.text = message;
        scoreText.text = _score.ToString();
        if(_score.Equals(0))
        {
            branchStoreText.color = Color.white;
            scoreText.color = wrongAnwser;
        }
        else
        {
            branchStoreText.color = Color.white;
            scoreText.color = rightAnwser;
        }
    }

}
