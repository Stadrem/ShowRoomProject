using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayerBase : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlayer());

        //OnPhotonSerializeView에서 데이터 전송 빈도 수 설정하기 (per seconds)
        PhotonNetwork.SerializationRate = 30;
        //대부분의 데이터 전송 빈도 수 설정하기 (per seconds)
        PhotonNetwork.SendRate = 30;
    }

    IEnumerator SpawnPlayer()
    {
        Debug.Log("코루틴 시작");
        //룸에 입장이 완료될 때까지 기다린다.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });
        Debug.Log("입장 완료");

        // 현재 클라이언트가 로컬 플레이어를 소유하고 있는지 확인
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {

            //약간 무작위 공간에 생성
            Vector2 randomPos = Random.insideUnitCircle * 3.0f;
            Vector3 initPosition = new Vector3(randomPos.x, 0.0f, randomPos.y);
            Debug.Log("변수 생성");

            //포톤 네트워크 전용 생성기
            player = PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);

            Debug.Log("플레이어 생성완료");
        }
    }
}
