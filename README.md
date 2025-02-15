# VR Roguelike

> **무한 던전 형식의 로그라이크 VR 게임**

## 🏰 게임 개요
VR 환경에서 진행되는 로그라이크 스타일의 던전 탐험 게임입니다. 
플레이어는 다양한 방을 탐색하며 몬스터와 싸우고, 함정을 피하고, 보물을 획득하며 점점 강해집니다.

---

## 🏠 던전 구조
던전은 총 6가지 종류의 방으로 구성됩니다:
- **Main Room** (중앙 허브 역할)
- **Regular Room** (기본 전투 방)
- **Recovery Room** (체력 회복 가능)
- **Lintel Room** (스탯 강화 가능)
- **Trap Room** (함정이 존재)
- **Treasure Room** (보상 획득 가능)

### 🗺️ 전체 던전 맵
![AllRoom](https://github.com/HaruNine/VR_Roguelike/assets/149753122/85828ce4-2f76-49e1-8f37-9b900043384e)

### **Main & Regular Room**
- **Main Room**: 던전의 중심이 되는 공간
- **Regular Room**: 일반 몬스터와 전투가 벌어지는 곳

![MainRoom](https://github.com/HaruNine/VR_Roguelike/assets/149753122/c9d66387-2937-4036-a8de-9242b88efd36)
![RegularRoom](https://github.com/HaruNine/VR_Roguelike/assets/149753122/c30effcf-aba0-4095-b505-b2c597860d41)

### **Recovery & Lintel Room**
- **Recovery Room**: 플레이어의 체력을 회복할 수 있는 방
- **Lintel Room**: 재회를 사용한 스탯 강화가 가능한 방

![RecoveryAndLintelRoom](https://github.com/HaruNine/VR_Roguelike/assets/149753122/0b742669-ff5a-4672-a3ff-cb0e59729ac9)

### **Trap & Treasure Room**
- **Trap Room**: 체력 감소, Soul 감소, 랜덤 이벤트 등 함정이 존재
- **Treasure Room**: 아이템 및 Soul을 획득할 수 있는 방

![BoxRoom](https://github.com/HaruNine/VR_Roguelike/assets/149753122/31bea517-6210-438e-8269-c6ac4e17310c)

---

## ⚔️ 게임 요소
### **UI, 무기, 사망 시스템**
![UI](https://github.com/HaruNine/VR_Roguelike/assets/149753122/696729e0-df32-461f-b5c2-6b87ad04b2dc)
![Sword](https://github.com/HaruNine/VR_Roguelike/assets/149753122/7a0db96f-043a-42bf-ad10-bf03eecc2837)
![Player_die](https://github.com/HaruNine/VR_Roguelike/assets/149753122/275de351-91b5-469e-ba79-851949b9dea6)

---

## 🛠 기타 기능
### **함정 (HP & Soul 감소, 랜덤 박스)**
![Trap_downHP](https://github.com/HaruNine/VR_Roguelike/assets/149753122/e52bde6c-a4f4-41e0-af5d-4486ae631c5e)
![Trap_downSoul](https://github.com/HaruNine/VR_Roguelike/assets/149753122/f1b78d32-2e29-4286-9fe9-15a991b98654)
![Trap_lucky](https://github.com/HaruNine/VR_Roguelike/assets/149753122/4fbd4429-cdbd-4e31-9fd9-1e42811f92dd)

### **보물 (Soul 획득)**
![Treasure](https://github.com/HaruNine/VR_Roguelike/assets/149753122/cbaa3f66-6911-4f9a-a061-22487080e0b5)

### **회복 (체력 회복 기능)**
![Recovery](https://github.com/HaruNine/VR_Roguelike/assets/149753122/e087c531-3cff-4b89-b3a1-0f2b71e15580)

### **스탯 업그레이드 (HP & 공격력 증가)**
![Up_HP_success](https://github.com/HaruNine/VR_Roguelike/assets/149753122/00625a70-f917-4ca0-9c2d-7249e8f4c1d8)
![Up_DM_success](https://github.com/HaruNine/VR_Roguelike/assets/149753122/934da2a8-41d8-451e-9939-6542fc0ef045)

---

## 👾 몬스터 종류 (4종류)
게임에는 총 4종류의 적이 등장합니다.

| Skeleton | Cthulhu | Flying Head | Ghost |
|:--------:|:-------:|:-----------:|:------:|
| ![Skeleton](https://github.com/HaruNine/VR_Roguelike/assets/149753122/2fce5235-0681-4556-8004-1f75940668a9) | ![Cthulhu](https://github.com/HaruNine/VR_Roguelike/assets/149753122/ffe14dfb-8a62-4236-a705-60f2c9b40610) | ![FlyingHead](https://github.com/HaruNine/VR_Roguelike/assets/149753122/ab68de58-e92d-4bc1-8cd5-0920b4792b0c) | ![Ghost](https://github.com/HaruNine/VR_Roguelike/assets/149753122/e5f639fd-bf8d-4b18-a2d2-7b3878183cf9) |

---

## 🎥 [구동 영상](https://github.com/HaruNine/VR_Roguelike/tree/main/video)

## 🎮 사용 Asset
1. [석상](https://assetstore.unity.com/packages/3d/environments/fantasy/angel-statue-27594)
2. [이펙트](https://assetstore.unity.com/packages/vfx/particles/spells/status-effects-free-238904)
3. [던전1](https://assetstore.unity.com/packages/3d/environments/dungeons/the-red-prison-40198)
4. [던전2](https://assetstore.unity.com/packages/3d/environments/dungeons/blue-dungeon-106912)
5. [검](https://assetstore.unity.com/packages/3d/props/medieval-long-sword-229366)
