# Goyang.io
청강대 게임네트워크프로그래밍 과제물입니다.

![image](https://user-images.githubusercontent.com/90246317/171873267-4ea10aba-649b-4725-9923-ea7b82d3872c.png)
[플레이 영상](https://youtu.be/Whasv4rUL2A)

## 게임 소개
- 좌클릭으로 이동하고 Space 키로 점프합니다.
- 우클릭으로 화살을 발사해 다른 고양이들을 공격합니다.
- HP가 모두 소모되면 사망하며, 다시 입장할 수 있습니다.
- 다른 고양이를 처치하면 HP를 얻습니다.
- 이래봬도 MMO 단일 전장 데스매치입니다.

## 네트워크 설계
- Server - Client 구조
- Server: C# (Dedi Server)
- Client: Unity
- Protocol: TCP
- 결정론적 락스텝 (일정 Turn을 두고 Action을 모음 -> 서버에서 주기를 맞춰 전송)
