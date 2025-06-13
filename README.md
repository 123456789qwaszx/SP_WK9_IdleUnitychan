# SP_WK9_IdleUnitychan
스파르타 코딩 9주차, 개인 3d심화과제입니다.
미완의 작업물을 보여드리게 되어 부끄럽습니다...
스스로 뭘 아는지 모르는지도 모릅니다. 무슨 말씀이든 감사히 듣겠습니다. 언제나 감사합니다.

- 플레이어 조작   
  -이동 및 회전 : 마우스 좌클릭 후, 드래그 // (기존엔 HierArchy창의 'UI_MouseDragStick'으로 마우스 움직임을 표시했으나, 인벤토리 클릭 안되는 문제로 꺼둔상태입니다.)    
  -점프 : 스페이스바    
  -공격 : 마우스 우클릭 // (맨손일 땐, 대미지 0, 아이템을 장착 해야지 몬스터 Kill 가능)    

  -인벤토리 : Tap    
  -아이템 장착 : 인벤토리 내 Item을 선택 후 EquipButton 클릭


- 기능_1. 무한맵, 맵생성시 오브젝트 랜덤배치    
  -담당 클래스 :    
  (1) "01.Scripts/Map/MeshGenerator"    
  (2)"01.Scripts/Map/PlaneGeneration"    
    
  
디폴트맵 생성 :    
if(startPos == Vector3.zero) => 맵 생성  (생성된 타일 좌표를 Key로 저장(GameManager의 Dictionary)    
맵크기 = radius * 2    
맵프리팹 = MeshGenerator사용    

이 후 맵생성 :    
1_플레이어이동거리 계산법 = PlayerMove => (int)(CharacterManager.Instance.Player.transform.position.x - startPos);    
2_플레이어이동거리 기반 플레이어위치지정 = PlayerLocation => (int)Mathf.Floor(CharacterManager.Instance.Player.transform.position / planeOffset) * planeOffset;    
3_플레이어 이동시 맵생성 = Vector3 (PlayerLocation + 맵크기)가 타일좌표를 보관한 딕셔너리에 없다면 그 위치에 맵생성,     생성된 맵좌표 List와 Dictionary로 저장    

맵에 오브젝트 추가 :    
맵생성시 저장된 List<Vector3>를 토대로 랜덤 생성    
    
    
- 기능_2. 대미지 시스템    
  -담당 클래스 :    
  (1)"01.Scripts/GameContents/Damageable"    
  (2)"01.Scripts/Item/Axe"    
  (3)"04.Data/EnemyData/EnemyData_200_Bear"    

충돌감지 :    
Axe.OnTriggerEnter (Collider ==  몬스터) => CheckDamage(Collider)    
->    
데미지 계수 설정 :    
Collider.GetComponent<Damageable>()    
Damageable.DamageMessage data;    
data.amount = (Axe._damage);    
data.damager = Axe this;    
collider.ApplyDamage(data);    
->    
데미지 적용 :    
currentHitPoints -= data.amount;    
->    
타격 판정 or 사망 판정    
(currentHitPoints <= 0) => OnDeath.Invoke // 미구현    
else OnReceiveDamage.Invoke() // 미구현    
->    
타격 판정 시 무적처리    
(isInvulnerable) => OnBecomeVulnerable.Invoke();    
사망 판정    
PoolManager.Instance.Push(gameObject);    
SpawningPool.Instance._bearCount -= 1;    


    
- 그외
1) 스포닝풀(유니티 풀 사용, 맵 내 유지되는 몬스터 관리(종류, 숫자, 스폰타임, 생성거리, 생성위치 등)    
2) 인벤토리 및 UI    
3) 스틱이동 및 Cursor변경    
4) 자연스러운 애니메이션 모션    

미완 (키보드 T입력시 Idle모드 전환)    
1) 자동사냥    
 - PlayerBehavior.FindEnemy()    
 - EnemyScanner로 특정 거리내 적을 감지 // 구현    
 - 감지된 적을 GameManager에서 딕셔너리로 저장 // 구현    
 - (딕셔너리.Count>0) => 플레이어와 가장 가까운 Value를 가진 Key 선택 // 미구현
 - 적 LockOn (타겟 고정 및 방향고정) // 구현
 - 적에게 이동 // 구현(임시)
 - 거리가 가까울 시 공격 // 구현
 - 적 사망 시 콜백 받아서 Dictionary.Clear() // 미구현
 - PlayerBehavior.FindEnemy()
 - 없으면 다음 장소 이동 // 미구현
