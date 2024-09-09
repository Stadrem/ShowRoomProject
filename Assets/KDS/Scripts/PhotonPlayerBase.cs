using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonPlayerBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer()
    {
        //룸에 입장이 완료될 때까지 기다린다.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });

        //약간 무작위 공간에 생성
        Vector2 randomPos = Random.insideUnitCircle * 2.0f;
        Vector3 initPosition = new Vector3(randomPos.x, 0.0f, randomPos.y);

        //포톤 네트워크 전용 생성기
        GameObject player = PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);
    }
}
