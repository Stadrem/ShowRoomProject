using UnityEngine;

public class FirstPerson_PlayerMove : MonoBehaviour
{
    public float sensitivity = 5f;
    public float moveSpeed = 5f;
    public float minY = -40f; // 마우스 시야 최소값
    public float maxY = 40f;  // 마우스 시야 최대값

    private Camera _camera;
    private Rigidbody _rb;

    private Vector3 _positionInput;
    private Vector3 _rotationInput;

    private void Awake()
    {
        // 각 컴포넌트를 찾아서 가져옴
        _camera = GetComponentInChildren<Camera>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 마우스 커서를 사라지게 함
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    
    {
        
        Turn();
        Move();
        GetComponent<Rigidbody>().velocity =Vector3.zero;
    }

    private void Turn()
    {
        /// 1. 마우스 델타값 업데이트
        // 마우스 좌우 이동 델타값을 얻어옴
        var mouseX = Input.GetAxisRaw("Mouse X");
        // 마우스 상하 이동 델타값을 얻어옴
        var mouseY = Input.GetAxisRaw("Mouse Y");
        // _rotationInput x값에 mouseY값을 빼고,
        // y값에 mouseX 값을 더함
        // z는 무시
        //mouseX를 더하는 이유 : 마우스 오른쪽으로 돌리면 y축 양의방향 회전하기 위해
        //mouseY를 빼는 이유 : 마우스 위로 올리면 x축 음의방향 회전하기 위해
        _rotationInput.Set(_rotationInput.x - mouseY, _rotationInput.y + mouseX, _rotationInput.z);

        /// 2. 카메라 x축 회전
        // 카메라가 널이 아니면 = 카메라가 있으면
        if (_camera != null)
        {
            // _rotationInput.x값을 minY와 maxY 사이로 제한
            _rotationInput.x = Mathf.Clamp(_rotationInput.x, minY / sensitivity, maxY / sensitivity);

            // _rotationInput.x값에 sensitivity를 곱한 값을 x축 방향으로 쿼터니언 계산해서
            // 카메라의 localRotation값에 할당
            _camera.transform.localRotation = Quaternion.Euler(_rotationInput.x * sensitivity, 0f, 0f);
        }

        /// 3. 플레이어 y축 회전
        // _rotationInput.y값에 sensitivity를 곱한 값을 y축 방향으로 쿼터니언 계산해서
        // 플레이어의 rotation값에 할당
        transform.rotation = Quaternion.Euler(0f, _rotationInput.y * sensitivity, 0f);
    }

    private void Move()
    {
        /// 4. 방향키 정보 얻어옴
        // 좌우 방향키 입력 정보를 얻어옴
        var h = Input.GetAxisRaw("Horizontal");
        // 상하 방향키 입력 정보를 얻어옴
        var v = Input.GetAxisRaw("Vertical");
        // _positionInput에 상하 입력정보를 z값으로
        // 좌우 입력 정보를 x값으로 저장
        _positionInput.Set(h, 0f, v);


        /// 5. 플레이어의 y방향을 기준으로 방향키 입력 방향으로 이동
        // 플레이어의 y방향 값을 얻어옴
        var y = transform.rotation.eulerAngles.y;
        // y만큼 회전된 쿼터니언을 생성
        var q = Quaternion.Euler(0f, y, 0f);
        // q쿼터니언과 방향키 입력 정보를 곱해서
        // q만큼 회전된 방향 정보를 계산
        var dir = q * _positionInput;
        // 노멀라이즈해서 단위 벡터로 만듦
        dir.Normalize();
        // rigidbody의 MovePosition을 이용해
        // rigidbody의 위치 + 이동속도 + 시간 + 방향
        // 값을 현재 위치로 설정
        //transform이 아니라 rigidbody로 해야 흔들림방지됨
        _rb.MovePosition(_rb.position + moveSpeed * Time.deltaTime * dir);
    }
}