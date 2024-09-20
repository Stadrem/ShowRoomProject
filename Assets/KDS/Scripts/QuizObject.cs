using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EKeySetUp;

public class QuizObject : MonoBehaviour, IEKey
{
    public void UiCall()
    {
        GameUiCanvas.instance.QuizStartTriggerEnter();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
