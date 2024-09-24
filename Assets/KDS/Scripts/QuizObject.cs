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
}
