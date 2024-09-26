using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_UIController : MonoBehaviour
{
    public GameObject p_Info;  //패널연결변수
    public GameObject p_Aitalk;
    public GameObject p_Selet;
    public GameObject UI_PreView_shh;
    public GameObject[] backcolors;

    //public GameObject[] panels;
    //public Button btn;
    public Button CloseButton;
    public Button Btn_Spec;
    public Button Btn_AIChat;
    public Button Btn_Select;

    //public Button[] btns;
    private void Start()
    {
        // btn.onClick.AddListener(Function1);
        // btns[0].onClick.AddListener(closeButton);
        p_Info.SetActive(false);
        p_Aitalk.SetActive(false);
        //짝짝짝 :-)

        //람다식
        //Btn_colorG.onClick.AddListener(() => { ColorChange(AA.A); });
        //Btn_colorB.onClick.AddListener(() => { ColorChange(AA.B); });
        //Btn_colorI.onClick.AddListener(() => { ColorChange(AA.C); });
        //Btn_colorG.onClick.AddListener(ExistedName);
        

    }
    public void closeButton()
    {
        UI_PreView_shh.SetActive(false);
    }

    void Function1() { }
    public void FunctionGo1()
    {
        p_Info.gameObject.SetActive(true);
        p_Aitalk.gameObject.SetActive(false);
        p_Selet.gameObject.SetActive(false);

        Btn_Spec.interactable = false;
        Btn_AIChat.interactable = true;
    }
    public void FunctionGo2() 
    {
        p_Info.gameObject.SetActive(false);
        p_Aitalk.gameObject.SetActive(true);
        p_Selet.gameObject.SetActive(false);

        Btn_Spec.interactable = true;
        Btn_AIChat.interactable = false;
        Btn_Select.interactable = true;
    }
    public void FunctionGo3() 
    {
        p_Selet.gameObject.SetActive(true);
        p_Info.gameObject.SetActive(false);
        p_Aitalk.gameObject.SetActive(false);


        Btn_Spec.interactable = true;
        Btn_AIChat.interactable = true;
        Btn_Select.interactable = false;
    }

    GameObject previousGo = null; //이전값 초기화
    int previousIndex = -1;
   
 
    // 버튼을 누르면
    public void ColorChange(int num)
    {
        //  if(previousIndex != -1)
        //  {
        //      // 이전에 활성화 된거(기억했던거)를 비활성화한다.
        //      backcolors[previousIndex].SetActive(false);
        //  }
        //      // 버튼을 누른게 보이게 한다. 해당 버튼의 이미지를 활성화
        //      backcolors[num].SetActive(true);
        //     
        //  // 활성화 시킨거를 기억하자.
        //      previousIndex = num;

       
        backcolors[num].SetActive(true); // 버튼을 누른게 보이게 한다. 해당 버튼의 이미지를 활성화

        if (previousGo != null)
        {
            previousGo.SetActive(false); // 이전에 활성화 된거 비활성화
        }

        previousGo = backcolors[num];  // 현재 버튼을 이전 버튼으로 설정
        
        ChangeImage(num); //num에 따라 이미지 색상을 변경하는 ChangeImage 메서드를 호출
    }

    Image image;
    Color color;
    public Button Btn_colorG;
    public Button Btn_colorB;
    public Button Btn_colorI;
    GameObject previousColor = null; //이전값 초기화
   
    
    public void ChangeImage(int gbi)
    {
        //image에 원하는 이미지 컴포넌트<현재 게임오브젝트의 자식들 중 Image 컴포넌트>를 갖고오고싶어!
        image = transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Image>();

        //색상 변경
        if (gbi == 0)
        {
            image.color = Btn_colorG.GetComponent<Image>().color;
        }

        else if(gbi == 1)
        {
            image.color = Btn_colorB.GetComponent<Image>().color;
        }

        else if(gbi == 2)
        {
            image.color = Btn_colorI.GetComponent<Image>().color;
        }

        //switch (gbi) 
        //{
        //    case 0:
        //        image.color = Btn_colorG.GetComponent<Image>().color;
        //        break;
        //    case 1:
        //        image.color = Btn_colorB.GetComponent<Image>().color;
        //        break;
        //    case 2:
        //        image.color = Btn_colorI.GetComponent<Image>().color;
        //        break;
        //    default:
        //        break;
        //}


        
    }

    


}
