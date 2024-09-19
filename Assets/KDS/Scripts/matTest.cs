using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EKeySetUp;

public class matTest : MonoBehaviour
{
    public LayerMask layerMask;  // 레이캐스트에 사용할 레이어 마스크
    public Material outlineMaterial;  // 아웃라인에 사용할 메테리얼
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();  // 각 렌더러의 원본 메테리얼 배열을 저장
    private Renderer currentRenderer;  // 현재 아웃라인이 적용된 렌더러

    void Update()
    {
        RaycastHit hit;
        // 카메라에서 전방으로 레이캐스트 수행
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30.0f, layerMask))
        {
            Renderer renderer = hit.transform.GetComponent<Renderer>();
            // 새로운 오브젝트에 레이캐스트가 닿았을 경우
            if (renderer != currentRenderer)
            {
                ResetPreviousRenderer();  // 이전 오브젝트의 아웃라인 제거
                currentRenderer = renderer;  // 현재 렌더러 업데이트
                ApplyOutlineMaterial(renderer);  // 새 오브젝트에 아웃라인 적용
            }
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if (hit.collider.gameObject.GetComponent<IEKey>() != null)
                {
                    IEKey ekey = hit.collider.gameObject.GetComponent<IEKey>();
                    ekey.UiCall();
                }
                else
                {
                    print("없는데요?");
                }
            }
        }
        else
        {
            ResetPreviousRenderer();  // 레이캐스트가 아무것도 맞지 않았을 때 이전 오브젝트의 아웃라인 제거
        }
    }

    void ApplyOutlineMaterial(Renderer renderer)
    {
        // 해당 렌더러에 아웃라인이 아직 적용되지 않았다면
        if (!originalMaterials.ContainsKey(renderer))
        {
            // 원본 메테리얼 배열을 저장
            originalMaterials[renderer] = renderer.materials;

            // 새 메테리얼 배열 생성 (기존 메테리얼 + 아웃라인 메테리얼)
            Material[] newMaterials = new Material[originalMaterials[renderer].Length + 1];
            for (int i = 0; i < originalMaterials[renderer].Length; i++)
            {
                newMaterials[i] = originalMaterials[renderer][i];
            }
            newMaterials[newMaterials.Length - 1] = outlineMaterial;  // 마지막에 아웃라인 메테리얼 추가

            // 새 메테리얼 배열 적용
            renderer.materials = newMaterials;
        }

        // 아웃라인 두께 설정 (항상 마지막 메테리얼)
        renderer.materials[renderer.materials.Length - 1].SetFloat("_OutlineThickness", 0.03f);
    }

    void ResetPreviousRenderer()
    {
        // 이전에 아웃라인이 적용된 렌더러가 있고, 그 원본 메테리얼이 저장되어 있다면
        if (currentRenderer != null && originalMaterials.ContainsKey(currentRenderer))
        {
            // 원본 메테리얼 배열로 복원
            currentRenderer.materials = originalMaterials[currentRenderer];
            originalMaterials.Remove(currentRenderer);
        }
        currentRenderer = null;  // 현재 렌더러 초기화
    }

    void OnDisable()
    {
        ResetPreviousRenderer();  // 스크립트가 비활성화될 때 마지막으로 적용된 아웃라인 제거
    }
}