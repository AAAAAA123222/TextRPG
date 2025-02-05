using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG
{
    internal class Program
    {

        //일단 구현해야 하는 것
        //캐릭터 리스트
        //이름, 레벨, 직업, 공격력, 방어력, 체력, Gold
        // 어떻게?
        //그 상태창을 보여주는 창 만들기
        //0번을 입력하면 나가기

        //인벤토리
        //아이템
        // 보유 중인 아이템을 표시하는 인터페이스
        // 착용 중인 아이템은 착용 중이라는 표시를 추가하기
        // 1번을 입력하면 장착하기로, 0번을 입력하면 그냥 나가기
        // 하지만 어떻게?
        // 장착 관리
        // 
        // 아이템을 장착했을 때 능력치를 변경하기
        // 변경된 상태를 상태창에서도 똑같이 반영하기

        //상점
        //보유 골드와 정보, 가격을 열거하기
        //  이미 구매했다면 '구매완료' 를 표시하기
        //1번을 누르면 구매 창으로, 2번을 누르면 그냥 나가기
        //아이템 구매
        //  이미 구매했다면 '이미 구매했습니다' 출력하고 다시 출력
        // 구매가 가능하다면 '구매했습니다' 출력 이후 재화 감소, 아이템을 인벤토리에 추가, 상점에 해당 물건의 구매완료 표시
        // 금액이 부족하다면 '골드가 부족합니다' 출력 이후 돌아오기
        // 예시에서 이탈한 값이라면 '잘못된 입력입니다' 출력 이후 돌아오기

        //어떻게 구현하는가
        //  일단 캐릭터부터
        // 이름, 레벨, 직업, 공격력, 방어력, 체력, Gold를 안에 먼저 넣기

        //장착 개선
        //어떻게 해야 하지?
        //메인캐릭터 클래스에 string List를 추가하고, 장비마다 장비에 맞는 string(armor,weapon)을 추가하고 EquipToggle를 할때 그 스트링이 메인캐릭터 클래스 안에 있는지 확인하기
        //막상 이러니까 뭘 착용하고 있는지에 대한 값이 없었음
        //List<아이템 클래스> 의 값을 추가한 뒤 착용할때마다 넣고 해제할때마다 리스트에서 삭제하는 식으로 진행하기
        //근데 결국 비교하려면 아이템 클래스에 아이템타입이 필요하다보니 그걸 넣고 판단해보니까 List string이 필요가 없어보여서 지우고 List<아이템 클래스> 만 남겨둠
        //장착한 아이템을 저장해두는 리스트(List<CommonEquipment> Wearing) 를 만들어두고 Player.Wearing과 착용할 아이템을 비교하는 조건문과 반복문을 EquipToggle()에 추가하는 식으로 구현
        //EquipToggle로 착용하게 될 경우, 착용한 아이템을 Player.Wearing 안에 넣고, 해제하게 될 경우 해제한 아이템을 Player.Wearing 안에서 제외하기

        //레벨업 기능 추가
        //계획
        //던전을 클리어할 수록 레벨이 오르는 시스템
        //던전 클리어 경험치를 캐릭터 안에 넣고 
        //클리어할때마다 그 값에 레벨업에 필요한 만큼의 경험치를 추가한 뒤
        //일정 경험치가 오를 때마다 레벨업 함수 실행
        //오른 레벨마다 공격력 0.5, 방어력 1 증가
        //소수점이 표기가 안 되는 자료형 int를 float로 변경(소수점이 표기되도록)
        //가능하면 경험치도 상태창에 추가하기
        //레벨이 오를때마다 필요 경험치가 올라가게끔 할 수 있을 것 같지만 명시되어 있지 않아 보류(int reqExp 변수를 추가하고, 매 레벨업 함수가 실행될때마다 exp가 초기화된 다음 reqExp의 값을 올리기)

        //던전 추가
        //DungeonStart() 메서드를 추가하고 그 메서드 안의 매개변수로 힘, 방어력, 골드의 양, 경험치의 양을 설정
        //힘에 따른 랜덤 보상 변수를 선언한 뒤 Random 함수로 힘, 그리고 이미 만들어놨던 힘 변수(x2)를 기입하는걸로 보상 보너스는 해결
        //마찬가지로, 방어력이 낮은데도 시도할 경우의 가능성을 Rand.Next(1,10)으로 잡아둔 뒤 3보다 클때 성공, 작을때 실패하는 걸로 확률 구현
        //성공했을 경우 매개변수로 지정해되어있는 골드에 1+보상 보너스만큼을 곱한 뒤 EarnGold 함수 안에 매개변수로 넣기
        //그리고 매개변수로 지정되어있는 경험치만큼 expUP 함수 안 매개변수로 넣기
        //나가기 전까지 반복문 실행, 나가겠다 선언되면 반복문 탈출
        //체력이 줄어드는 것까진 구현했는데 체력이 0 이하가 되었을 때 게임이 종료되는 기능은 명시되어 있지 않아서 보류(체력이 줄어드는 걸 메서드로 구현한 뒤 줄어들고 나서 체력이 0이 되었을 때 isAlive 불리언을 false로 바꾸는 등)

        public class MainCharacter
        {
            // 뭐가 생길것이라 선언
            public string Name { get; set; }
            public string Job { get; set; }
            public int Gold { get; set; }
            public float Strength { get; set; }
            public float Defence { get; set; }
            public int Health { get; set; }
            public int Level { get; set; }
            public float ogStrength { get; set; }
            public float ogDefence { get; set; }
            public List<CommonEquipment> Wearing { get; set; }
            public int exp { get; set; }


            // 생성자
            public MainCharacter(string name, string job)
            {
                //입력받을 변수
                Name = name;
                Job = job;
                //초기 스탯
                Gold = 1500;
                Strength = 10;
                Defence = 5;
                Health = 100;
                Level = 1;
                ogStrength = Strength;
                ogDefence = Defence;
                exp = 0;
                Wearing = new List<CommonEquipment>();

            }

            //상태창 메서드
            public void ShowStatus()
            {
                Console.WriteLine($"Lv. {Level}");
                Console.WriteLine($"이름: {Name}");
                Console.WriteLine($"직업: {Job}");
                Console.WriteLine($"공격력: {Strength} (+{Strength - ogStrength})"); //오른 힘과 스탯으로 오른 힘을 강조하기
                Console.WriteLine($"방어력: {Defence} (+{Defence - ogDefence})"); //오른 방어력과 스탯으로 오른 방어력을 강조하기
                Console.WriteLine($"체력: {Health}");
                Console.WriteLine($"골드: {Gold}");
                //상태창 표시
            }

            //골드 메서드, 획득 시
            public void EarnGold(int amount)
            {
                Gold += amount;
                Console.WriteLine($"{amount} 골드를 얻었습니다! 현재 골드: {Gold}");
            }

            //골드 메서드, 소모 및 소모 여부 반환(구매할 수 있도록)
            public bool SpendGold(int amount)
            {
                if (Gold >= amount)
                {
                    Gold -= amount;
                    Console.WriteLine($"{amount} 골드를 사용했습니다. 남은 골드: {Gold}");
                    return true;
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다!");
                    return false;
                }
            }
            //경험치, 그리고 경험치에 따른 레벨업 함수
            public void ExpUp(int _Exp)
            {
                exp += _Exp; //경험치 추가
                if (exp > 0 && exp % 5 == 0) //exp가 0보다 크면서, exp가 5의 배수일 경우
                {
                    Console.WriteLine($"축하합니다! 레벨이 올랐습니다. 현재 레벨:{Level}. 공격력이 0.5, 방어력이 1 성장했습니다.");
                    LevelUP(); //레벨업 함수 실행
                }
            }
            //레벨업에 따른 능력치 조정 함수
            public void LevelUP()
            {
                //레벨 1 증가
                Level += 1;
                //힘 증가폭 조정
                float StrModifier = 0.5f;
                float DefModifier = 1f;
                //증가 먼저 계산
                Strength += StrModifier;
                Defence += DefModifier;
                ogStrength += StrModifier;
                ogDefence += DefModifier;
            }
        }



        //상점의 아이템을 클래스로 구현
        //public int reqGold(구매에 필요한 골드), public int statAmount(착용 시 바뀔 스탯), public bool isEquipped(착용된 상태인지 확인하는 값), public bool isBought(구매된 상태인지 확인하는 값) 
        //
        //

        public class CommonEquipment
        {
            public string ItemName { get; set; }
            public string Description { get; set; }
            public int ReqGold { get; set; }
            public int Strength { get; set; }
            public int Defence { get; set; }
            public bool IsEquipped { get; set; }
            public bool IsBought { get; set; }

            public string ItemType { get; set; }

            //생성자 작성하기
            public CommonEquipment(string itemName, string description, int reqGold, int strength, int defence, string itemType)
            {
                //입력받을 변수
                ItemName = itemName;
                Description = description;
                ReqGold = reqGold;
                Strength = strength;
                Defence = defence;
                ItemType = itemType;
                //기본 상태
                IsEquipped = false;
                IsBought = false;
            }

            //장비 착용 토글하기
            public bool EquipToggle(MainCharacter Player)
            {
                if (!IsEquipped) //착용하지 않았을 경우
                {
                    CommonEquipment Equippeditem = null; //겹치는 아이템을 변수로 저장할 공간을 선언
                    for (int i = 0; i < Player.Wearing.Count; i++) //반복문을 플레이어가 입고 있는 장비의 수만큼 계속하기
                    {
                        if (Player.Wearing[i].ItemType == this.ItemType) //플레이어가 입고있는 물건의 아이템타입과 이 물건의 아이템타입이 같을 경우
                        {
                            Equippeditem = Player.Wearing[i]; //중복된 아이템 변수로 넣고
                        }
                    }

                    if (Equippeditem != null) //장비의 아이템타입이 Player의 ItemSlot 안에 있을 경우
                    {
                        Console.WriteLine($"이미 {Equippeditem.ItemType} 칸에 무언가 착용되어 있습니다. {Equippeditem.ItemName}을 해제합니다.");
                        Equippeditem.EquipToggle(Player); //플레이어에게 장착한 아이템을 해제하라는 메서드를 실행하라고 명령
                    }

                    StatUp(Player); //StatUp 메서드를 호출해 착용한 장비만큼 플레이어 스탯 상승
                    IsEquipped = true; //착용 상태 변경 이후
                    Player.Wearing.Add(this); //입고 있는 장비에 추가하기
                    Console.WriteLine($"{Player.Name}가 {ItemName}을(를) 착용했습니다.");
                    Console.WriteLine($"효과: 공격력 +{Strength}, 방어력 +{Defence}");
                }
                else //착용했을 경우
                {
                    StatDown(Player); //StatDown 메서드를 호출해 해제한 장비만큼 플레이어 스탯 하락
                    IsEquipped = false; //착용 상태 변경 이후
                    Player.Wearing.Remove(this); //입고 있는 장비에서 제거하기
                    Console.WriteLine($"{Player.Name}가 {ItemName}을(를) 해제했습니다.");
                    Console.WriteLine($"효과: 공격력 -{Strength}, 방어력 -{Defence}");
                }
                return IsEquipped; // 변경된 상태를 반환하기
            }
            //스탯 상승
            public void StatUp(MainCharacter Player)
            {
                Player.Strength += this.Strength;
                Player.Defence += this.Defence;
            }
            //스탯 감소
            public void StatDown(MainCharacter Player)
            {
                Player.Strength -= this.Strength;
                Player.Defence -= this.Defence;
            }

        }





        //게임 매니저
        public class GameManager
        {
            //필요한 것들 선언하기
            private MainCharacter Player;               // 플레이어 캐릭터
            private List<CommonEquipment> inventory; //그 캐릭터의 인벤토리
            private bool isRunning; //이 게임이 실행중인지
            private List<CommonEquipment> shopList; // 상점의 판매 품목들

            // 생성자: 초기 설정
            public GameManager(string inputname, string inputClass)
            {
                Player = new MainCharacter(inputname, inputClass); //입력된 이름과 직업이 포함된 캐릭터 생성
                inventory = new List<CommonEquipment>(); //빈 리스트의 인벤토리를 생성
                isRunning = true; //게임 시작이 됐다고 선언
                //시작 조건 설정
                CommonEquipment Tutorialsword = new CommonEquipment("훈련용 목검", "아주 무딘 검입니다.", 0, 1, 0, "무기"); //목검 생성
                CommonEquipment Tutorialarmor = new CommonEquipment("훈련용 갑옷", "많이 낡은 갑옷입니다.", 0, 1, 0, "갑옷"); //목검 생성
                inventory.Add(Tutorialsword); //목검 인벤토리에 추가
                inventory.Add(Tutorialarmor); //목검 인벤토리에 추가
                shopList = new List<CommonEquipment>
                {
                    new CommonEquipment("구리 검", "그럭저럭 쓸만한 것 같습니다.", 500, 3, 0, "무기"),
                    new CommonEquipment("양철 검", "적당한 품질의 검처럼 보입니다.", 1000, 5, 0,"무기"),
                    new CommonEquipment("강철 검", "예리한 칼날로 무엇이든 벨 수 있을것만 같은 검입니다.", 2500, 10, 0,"무기"),
                    new CommonEquipment("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 500, 0, 3,"갑옷"),
                    new CommonEquipment("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 1000, 0,5,"갑옷"),
                    new CommonEquipment("강철 갑옷", "어떤 공격이라도 막아낼 것 같은 갑옷입니다.", 2500, 0, 10,"갑옷"),
                };

                if (inputClass == "전사")
                {
                    Player.Defence += 5;
                }
                else if (inputClass == "마법사")
                {
                    Player.Strength += 5;
                }
                //직업에 따른 능력치 조정
            }


            // 메인 메뉴 표시
            // 메뉴 자체로는 하는 게 없으니 행동을 따로 입력해야 한다
            private void ShowMainMenu()
            {
                Console.WriteLine("1. 내 상태 보기");
                Console.WriteLine("2. 내 인벤토리");
                Console.WriteLine("3. 상점 들어가기");
                Console.WriteLine("4. 던전 탐험");
                Console.WriteLine("5. 휴식하기");
                Console.Write("원하시는 행동을 입력해주세요. ");
            }
            // 입력받고 수행하기

            private void HandleInput()
            {
                string input = Console.ReadLine(); //입력 받고나서
                Console.Clear(); //한번 치우고
                switch (input) //받은 값에 따라 결정
                {
                    case "1":
                        ShowStatus();
                        break;
                    case "2":
                        OpenInventory();
                        break;
                    case "3":
                        EnterShop();
                        break;
                    case "4":
                        EnterDungeon();
                        break;
                    case "5":
                        TakeABreak(500);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다."); //예외처리
                        break;
                }
            }
            //상태창
            private void ShowStatus()
            {
                bool iswronganswer = true;
                while (iswronganswer) // 맞는 답을 적기 전까지 반복문 시작
                {
                    int i = 0;//착용중인 변수를 확인하기 위한 변수
                    Console.WriteLine("=== 캐릭터 상태 ===");
                    Player.ShowStatus(); //플레이어 클래스 안에 있는 스테이터스 보여주기 함수를 사용
                    Console.WriteLine("=== 착용중인 아이템 ===");
                    foreach (var item in inventory)
                    {
                        if (item.IsEquipped) //만약 아이템이 착용중이라면
                        {
                            i++; //착용했단 변수를 증가
                            Console.WriteLine($"[E]{item.ItemName} (공격력 +{item.Strength}, 방어력 +{item.Defence})");
                        }
                    }
                    if (i == 0)//만약 착용중인 게 단 하나도 없다면
                    {
                        Console.WriteLine("착용중인 장비가 없습니다."); //없다고 출력
                    }
                    Console.WriteLine("=======================");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("원하는 행동을 입력하세요.");
                    string check = Console.ReadLine();
                    Console.Clear();
                    switch (check)
                    {
                        case "0":
                            iswronganswer = false;
                            break;
                        default:
                            Console.WriteLine("틀린 숫자입니다.");
                            break;
                    }
                }
            }

            //인벤토리열기

            private void OpenInventory()
            {
                bool iswronganswer = true;
                while (iswronganswer) // 맞는 답을 적기 전까지 반복문 시작
                {
                    Console.WriteLine($"현재 공격력: {Player.Strength}, 현재 방어력: {Player.Defence}");
                    Console.WriteLine("=== 인벤토리 ===");

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        string isequipped; //착용했는지 안 착용했는지 판정할 변수 선언하기

                        if (inventory[i].IsEquipped) //만약 아이템이 착용중이라면
                        {
                            isequipped = "[E]"; //[E](착용중) 이라는 문자열을 출력하기
                        }
                        else
                        {
                            isequipped = " - "; // 그냥 - 출력하기
                        }
                        Console.WriteLine($"{isequipped}{inventory[i].ItemName} (공격력 +{inventory[i].Strength}, 방어력 +{inventory[i].Defence})");
                    }
                    Console.WriteLine("=======================");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("1. 장착 관리");
                    Console.WriteLine("원하는 행동을 입력하세요.");
                    string check = Console.ReadLine(); //입력 받고
                    Console.Clear(); //일단 치우기
                    switch (check)
                    {
                        case "0":
                            iswronganswer = false; //반복문 탈출하기
                            break;
                        case "1":
                            EquipmentManager(); //장착 관리자 메서드 호출
                            break;
                        default:
                            Console.WriteLine("틀린 숫자입니다.");
                            break;
                    }


                }
            }

            //장착 관리자
            private void EquipmentManager()
            {
                while (true) // 나간다고 입력하기 전까지 반복문 시작
                {
                    Console.WriteLine($"현재 공격력: {Player.Strength}, 현재 방어력: {Player.Defence}");
                    Console.WriteLine("=== 장착/해제할 아이템을 선택해 주세요. ===");
                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("장착할 아이템이 없습니다.");
                    }

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        string isequipped; //착용했는지 안 착용했는지 판정할 변수 선언하기

                        if (inventory[i].IsEquipped) //만약 아이템이 착용중이라면
                        {
                            isequipped = "[E]"; //[E](착용중) 이라는 문자열을 출력하기
                        }
                        else
                        {
                            isequipped = " - "; // 그냥 - 출력하기
                        }
                        Console.WriteLine($"{i + 1}. {isequipped}{inventory[i].ItemName} (공격력 +{inventory[i].Strength}, 방어력 +{inventory[i].Defence})");
                    }

                    Console.WriteLine("=== 아이템 번호를 입력하면 장착/해제할 수 있습니다. (0: 돌아가기) ===");

                    string input = Console.ReadLine();
                    Console.Clear(); //일단 밀기
                    bool isNum = int.TryParse(input, out int choice);

                    if (isNum && choice > 0 && choice <= inventory.Count) //적합한 숫자를 입력했다면(0보다 크면서 inventory만큼의 수 안의 숫자를 입력했다면)
                    {
                        inventory[choice - 1].EquipToggle(Player); //플레이어에게 inventory.EquipToggle 함수를 사용
                        if (inventory[choice - 1].IsEquipped) // 만약 장착 함수를 사용했는데 결과가 참이라면(장착을 했다면)
                        {
                            Console.WriteLine($"{inventory[choice - 1].ItemName}의 장착을 완료했습니다."); //장착했다고 알려주기
                        }
                        else //해제를 하게 됐다면
                        {
                            Console.WriteLine($"{inventory[choice - 1].ItemName}의 장착을 완료했습니다."); //해제했다고 알려주기
                        }
                    }
                    else if (input == "0") //나가기를 희망한다면
                    {
                        break; //반복문 탈출, 첫 화면으로 이동
                    }
                    else
                    {
                        Console.WriteLine("맞는 숫자를 입력해주세요.");
                    }
                }
            }

            private void EnterShop()
            {
                bool iswronganswer = true;
                while (iswronganswer) // 맞는 답을 적기 전까지 반복문 시작
                {
                    Console.WriteLine($"현재 공격력: {Player.Strength}, 현재 방어력: {Player.Defence}");
                    Console.WriteLine("=== 상점 ===");

                    for (int i = 0; i < shopList.Count; i++)
                    {
                        string Price; //착용했는지 안 착용했는지 판정할 변수 선언하기

                        if (shopList[i].IsBought) //만약 아이템을 구매했다면   
                        {
                            Price = "구매완료"; // 가격에 "구매완료" 라는 문자열을 출력하기
                        }
                        else //구매하지 않았다면
                        {
                            Price = ($"{shopList[i].ReqGold.ToString()} gold"); // 대신, (구매에 필요한 골드) gold를 출력
                        }
                        Console.WriteLine($"- {shopList[i].ItemName} (공격력 +{shopList[i].Strength}, 방어력 +{shopList[i].Defence}) | {Price}");
                    }
                    Console.WriteLine("=======================");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    Console.WriteLine("원하는 행동을 입력하세요.");
                    string check = Console.ReadLine(); //입력 받고
                    Console.Clear(); //일단 치우기
                    switch (check)
                    {
                        case "0":
                            iswronganswer = false; //반복문 탈출하기
                            break;
                        case "1":
                            ShopBuy(); //구매 관리자 메서드 호출
                            break;
                        case "2":
                            ShopSell(); //판매 관리자 메서드 호출
                            break;
                        default:
                            Console.WriteLine("틀린 숫자입니다.");
                            break;
                    }
                }
            }
            private void ShopBuy()
            {
                while (true) // 맞는 답을 적기 전까지 반복문 시작
                {
                    Console.WriteLine($"현재 공격력: {Player.Strength}, 현재 방어력: {Player.Defence}, 현재 골드: {Player.Gold} gold");
                    Console.WriteLine("=== 아이템 구매하기 ===");

                    int i = 0;
                    while (i < shopList.Count)
                    {
                        string Price; //구매 여부를 판정할 변수 선언하기

                        if (shopList[i].IsBought) //만약 아이템을 구매했다면   
                        {
                            Price = "구매완료"; // 구매완료라는 문자열을 출력하기
                        }
                        else //구매하지 않았다면
                        {
                            Price = ($"{shopList[i].ReqGold.ToString()} gold"); // 그냥 {가격} gold 출력하기
                        }
                        Console.WriteLine($"- {i + 1} {shopList[i].ItemName} (공격력 +{shopList[i].Strength}, 방어력 +{shopList[i].Defence}) | {Price}");
                        i++;
                    }
                    Console.WriteLine("=======================");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("아이템 번호를 입력하면 아이템을 구매할 수 있습니다.");
                    Console.WriteLine("원하는 행동을 입력하세요.");

                    string input = Console.ReadLine();
                    Console.Clear(); //일단 밀기
                    bool isNum = int.TryParse(input, out int choice);

                    if (isNum && choice > 0 && choice <= shopList.Count) //숫자이면서 입력값이 0보다 크고 shopList보다 작거나 같다면
                    {
                        if (shopList[choice - 1].IsBought) // 이미 산 물건이라면
                        {
                            Console.WriteLine("이미 구매한 아이템입니다."); //이미 샀다고 말한 뒤 종료
                        }
                        else //아니라면 구매 시작
                        {
                            bool isbought = Player.SpendGold(shopList[choice - 1].ReqGold); //아이템의 가격만큼 플레이어에게 SpendGold 함수 발동, 이후 반환값을 변수에 저장
                            if (isbought)
                            {
                                Console.WriteLine($"아이템 {shopList[choice - 1].ItemName}를 구매했습니다."); //해당 아이템을 구매했다는 알림 출력
                                shopList[choice - 1].IsBought = true;
                                inventory.Add(shopList[choice - 1]); //이후 아이템을 인벤토리에 추가
                            }
                        }
                    }
                    else if (input == "0") //입력값이 문자열 0이라면
                    {
                        break; //반복문 탈출
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }
            private void ShopSell()
            {
                while (true) // 맞는 답을 적기 전까지 반복문 시작
                {
                    Console.WriteLine($"현재 공격력: {Player.Strength}, 현재 방어력: {Player.Defence}, 현재 골드: {Player.Gold} gold");
                    Console.WriteLine("=== 아이템 판매하기 ===");

                    int i = 0;
                    while (i < inventory.Count) //플레이어의 인벤토리만큼 반복하기
                    {
                        float fPrice = inventory[i].ReqGold * (float)0.85f;
                        int Price = (int)fPrice;
                        Console.WriteLine($"- {i + 1} {inventory[i].ItemName} (공격력 +{inventory[i].Strength}, 방어력 +{inventory[i].Defence}) | {Price}");
                        i++;
                    }
                    Console.WriteLine("=======================");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("아이템 번호를 입력하면 아이템을 판매할 수 있습니다.");
                    Console.WriteLine("원하는 행동을 입력하세요.");

                    string input = Console.ReadLine();
                    Console.Clear(); //일단 밀기
                    bool isNum = int.TryParse(input, out int choice);

                    if (isNum && choice > 0 && choice <= inventory.Count) //숫자이면서 입력값이 0보다 크고 inventory보다 작거나 같다면
                    {
                        string itemName = inventory[choice - 1].ItemName;
                        int itemPrice = (int)(inventory[choice - 1].ReqGold * 0.85);

                        while (true) //판매 확정을 위한 반복문 시작
                        {
                            Console.WriteLine($"{itemName}을/를 정말 판매하시겠습니까? y/n");
                            Console.WriteLine($"판매시 받게 되는 골드:{itemPrice}");
                            string confirmation = Console.ReadLine(); //일단 입력 받고
                            Console.Clear(); //일단 밀기
                            if (confirmation == "y")
                            {
                                //판매 시작
                                Player.EarnGold(itemPrice); //아이템의 가격만큼 플레이어에게 SpendGold 함수 발동, 이후 반환값을 변수에 저장
                                Console.WriteLine($"아이템 {itemName}을/를 판매했습니다."); //해당 아이템을 판매했다는 알림 출력
                                inventory[choice - 1].EquipToggle(Player);//착용 해제
                                inventory.Remove(inventory[choice - 1]); //아이템을 인벤토리에서 제거
                                //그 다음, 반복문 시작
                                for (int j = 0; j < shopList.Count; j++) //상점의 아이템 수만큼 반복
                                {
                                    if (itemName == shopList[j].ItemName && shopList[j].IsBought) //판매할 아이템의 이름과 상점에 진열된 아이템의 품목의 이름이 완전히 같으면서, 이미 구매했던 적이 있다면
                                    {
                                        shopList[j].IsBought = false;//구매했다는 표시를 제거하기(다시 구매할 수 있도록)
                                    }
                                }
                                break; //이후 반복문 탈출
                            }
                            else if (confirmation == "n")
                            {
                                break; //아무것도 하지 않고 반복문 탈출
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                    }
                    else if (input == "0") //입력값이 문자열 0이라면
                    {
                        break; //반복문 탈출
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }

            private void EnterDungeon()
            {
                while (true)
                {
                    Console.WriteLine("=== 던전 입장 ===");
                    Console.WriteLine("입장할 던전을 선택해 주세요.");
                    Console.WriteLine("1. 쉬운 던전, 방어력 10 이상 권장");
                    Console.WriteLine("2. 일반 던전, 방어력 15 이상 권장");
                    Console.WriteLine("3. 어려운 던전, 방어력 20 이상 권장");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("원하는 행동을 입력하세요.");

                    string input = Console.ReadLine();
                    Console.Clear();

                    int choice;
                    bool isnum = int.TryParse(input, out choice);

                    if (isnum && choice > 0 && choice < 4)
                    {
                        switch (choice)
                        {
                            case 1:
                                DungeonStart(Player.Strength, Player.Defence, 10, 1000);
                                break;
                            case 2:
                                DungeonStart(Player.Strength, Player.Defence, 15, 1500);
                                break;
                            case 3:
                                DungeonStart(Player.Strength, Player.Defence, 20, 2500);
                                break;
                        }
                    }
                    else if (input == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }

            private void DungeonStart(float Str, float Def, int amountOfExp, int amountOfGold)
            {
                Random Rand = new Random();
                float str2 = Str;
                int goldModifier = Rand.Next((int)Str, (int)str2);

                Console.WriteLine("던전에 진입했습니다.");
                if (Player.Defence < Def) //플레이어의 방어력이 던전 요구치보다 낮을 때
                {
                    int isClear = Rand.Next(1, 10);//40%의 확률로
                    if (isClear > 3)//성공
                    {
                        Player.Health -= Rand.Next(20, 35);//20~35중 무작위 값을 차감
                        Player.ExpUp(amountOfExp);//던전에 할당된 경험치만큼 플레이어의 경험치 증가
                        Console.WriteLine($"클리어에 성공했습니다. 축하합니다! 현재 체력:{Player.Health}");
                        Player.EarnGold((int)(amountOfGold * (1 + goldModifier * 0.01)));
                    }
                    else//실패
                    {
                        Player.Health /= 2;
                        Console.WriteLine($"클리어에 실패했습니다. 체력이 절반으로 줄어들었습니다. 현재 체력:{Player.Health}");
                    }

                }
                else//높거나 같다면
                {
                    //무조건 성공
                    Player.Health -= Rand.Next(20, 35);//20~35중 무작위 값을 차감
                    Player.ExpUp(amountOfExp);//던전에 할당된 경험치만큼 플레이어의 경험치 증가
                    Console.WriteLine($"클리어에 성공했습니다. 축하합니다! 현재 체력:{Player.Health}");
                    Player.EarnGold((int)(amountOfGold * (1 + goldModifier * 0.01)));
                }

            }

            private void TakeABreak(int _reqGoldtoBreak)
            {
                int reqGoldtoBreak = _reqGoldtoBreak;
                while (true)//일단 반복문 선언
                {
                    Console.WriteLine("휴식하기");
                    Console.WriteLine($"{reqGoldtoBreak} G 를 내면 체력을 회복할 수 있습니다. (보유 골드:{Player.Gold} G)");
                    Console.WriteLine("1. 휴식하기");
                    Console.WriteLine("0. 나가기");

                    string input = Console.ReadLine(); //입력 받고
                    Console.Clear(); //일단 지우기
                    int.TryParse(input, out int choice);
                    if (choice == 1) //입력한 값이 정수 1이라면
                    {
                        bool isbought = Player.SpendGold(reqGoldtoBreak); //휴식에 필요한 비용만큼 플레이어에게 SpendGold함수 발동. 이후 반환값 저장
                        if (isbought) //샀다면
                        {
                            Player.Health = 100; //체력을 100으로 고정
                            Console.WriteLine($"휴식에 성공했습니다. 현재 체력:{Player.Health}");
                        }
                        else //못 샀다면
                        {
                            Console.WriteLine("휴식에 실패했습니다.");
                        }
                    }
                    else if (input == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("틀린 입력입니다.");
                    }
                }
            }


            public void StartGame()
            {
                Console.WriteLine("=== 텍스트 RPG에 오신 것을 환영합니다! ===");
                Console.WriteLine($"플레이어: {Player.Name}");

                // 게임 진행을 위한 반복문 시작
                while (isRunning)
                {
                    ShowMainMenu(); // 메인 메뉴 표시
                    HandleInput();  // 플레이어 입력 처리
                }
            }
        }












        static void Main(string[] args)
        {
            string Pinput1, Pinput2 = "";
            Console.WriteLine("이름을 입력해주세요.");
            Pinput1 = Console.ReadLine();


            bool iswronganswer = true;
            while (iswronganswer)
            {
                Console.WriteLine("직업을 입력해주세요. 1:전사(방어력에 +5), 2:마법사(공격력에 +5).");
                string check = Console.ReadLine();
                Console.Clear();
                switch (check)
                {
                    case "1":
                        Pinput2 = "전사";
                        iswronganswer = false;
                        break;
                    case "2":
                        Pinput2 = "마법사";
                        iswronganswer = false;
                        break;
                    default:
                        Console.WriteLine("틀린 숫자입니다.");
                        break;

                }

            }
            Console.Clear();
            GameManager gameManager = new GameManager(Pinput1, Pinput2);
            gameManager.StartGame();

        }





















    }
}
