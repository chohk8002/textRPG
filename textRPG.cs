using System;

class Program
{
    static string playerName = "chad";//캐릭터이름
    static string playerClass = "전사";//직업
    static int playerLevel = 1;//렙
    static int baseAttackPower = 10;//기본 공격력(무기를 장착하지 않았을때)
    static int baseDefensePower = 5;//기본 방어력(방어구를 장착하지 않았을때)
    static int health = 100;//기본채력
    static int gold = 1500;//기본금화

    static Weapon equippedWeapon = null;//장착장비
    static Armor equippedArmor = null;//장착방어구
    static List<Item> inventory = new List<Item>//인벤토리아이템
    {
        new Item("무쇠갑옷", "방어력 +5", "무쇠로 만들어져 튼튼한 갑옷입니다."),
        new Item("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검입니다.")
    };

    class Item
    { 
        public string Name { get; }//아이템 이름
        public string Stats { get; }//능력치
        public string Description { get; }//설명
        public int AttackIncrease { get; protected set; }//공격력증가량
        public int Defenseincrease { get; protected set; }//방어력증가량
        

            public Item(string name, string stats, string description)
            {
                Name = name;
                Stats = stats;
                Description = description;
            }
    }

    class Weapon : Item//무기를 장착했을때 능력치가 올라가게 해주기
    { 
        public int AttackIncrease { get; }

        public Weapon(string name, int attackIncrease)
            : base(name, "공격력 + { attackIncrease}", "무기입니다.")
        {
            AttackIncrease = attackIncrease;
        }
    }

    class Armor : Item//방어구를 장착했을때 능력치가 올라가게 해주기
    {
        public Armor(string name, int defenseIncrease)
            : base(name, "방어력 + { defenseIncrease}", "방어구입니다.")
        {
            DefenseIncrease = defenseIncrease;
        }

        public int DefenseIncrease { get; }
    }

   
    static void Main()//시작화면
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동할수있습니다.");

        ShowMainMenu();
    }
    
    static void ShowMainMenu()//메인메뉴를 보여주고 선택할수있도록 해주는곳
    {
        Console.WriteLine("1.상태보기");//1번 상태보기
        Console.WriteLine("2.인벤토리");//2번 인벤토리
        Console.WriteLine("3. 상점");//3번 상점
        Console.Write("원하시는 행동을 입력해주세요.");// 원하는 숫자 입력하기

        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int choice) && (choice >= 1 && choice <= 2))
        {
            ProcessUserInput(choice.ToString());
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");//1~3번까지만 선택이 되고 나머지는 잘못입력하였다고 나오게 하기
        }

    }
    static void ProcessUserInput(string userInput)//1~3번중 번호를 눌렀을때 해당메뉴로 넘어가게 해줌
    {
        switch (userInput)
        {
            case "1":
                ShowCharacterStatus();
                break;
            case "2":
                ShowInventory();
                break;
            case "3":
                ShowShopMenu();
                break;
            default:
                Console.WriteLine("잘못된 입력입니다. 다시입력해주세요.");
                break;

        }

        ShowMainMenu();
    }

    static void ShowCharacterStatus() //캐릭터의 능력치를 보여준다
    {
        Console.WriteLine("캐릭터의 상태를 보여줍니다.");

        int totalAttackPower = baseAttackPower + (equippedWeapon != null ? equippedWeapon.AttackIncrease : 0);
        int totalDefensePower = baseDefensePower + (equippedArmor != null ? equippedArmor.DefenseIncrease : 0);

        Console.WriteLine("Lv. {playerLevel}");
        Console.WriteLine("{playerClass}. {playerClass}");
        Console.WriteLine("공격력. {attackPower}");
        Console.WriteLine("방어력. {defensePower}");
        Console.WriteLine("health. {health}");
        Console.WriteLine("gold. {gold} G");

        Console.WriteLine("0. 나가기");
        
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        string userInput = Console.ReadLine();

        if (userInput == "0")
        {
            return;
        }

        else
        {
            ShowCharacterStatus();
        }
    }

    static void ShowInventory()//캐릭터의 장비와 인벤토리를 보여준다
    {
        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine("- {i + 1} [{inventory[i].Name.Substring(0, 1)}]{inventory[i].Name} | {inventory[i].Stats} | {inventory[i].Description}");
        }

        Console.WriteLine("장착된 아이템");
        if (equippedWeapon != null)
        {
            Console.WriteLine("무기: {equippedWeapon.Name} | {equippedWeapon.Stats} | {equippedWeapon.Description}");
        }
        else
        {
            Console.WriteLine("무기: (없음)");
        }

        if (equippedArmor != null)
        {
            Console.WriteLine("방어구: {equippedArmor.Name} | {equippedArmor.Stats} | {equippedArmor.Description}");
        }
        else
        { 
        Console.WriteLine("방어구: (없음)");
        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 아이템 사용");
        Console.WriteLine("2. 아이템 장착");
        Console.WriteLine("3. 아이템 판매");


        Console.Write("원하시는 행동을 입력해주세요. ");
        string userInput = Console.ReadLine();

        if (userInput == "0")
        {
            return;
        }

        else
        {
            ShowInventory();
        }
    }

    static void EquipItem(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            Item selectedItem = inventory[index];

            if (selectedItem is Weapon weapon)
            {
                equippedWeapon = weapon;
                Console.WriteLine("{equippedWeapon.Name}이(가) 장착되었습니다.");
            }
            else if (selectedItem is Armor armor)
            {
                equippedArmor = armor;
                Console.WriteLine("{equippedArmor.Name}이(가) 장착되었습니다.");
            }
            else
            {
                Console.WriteLine("이 아이템은 장착할 수 없습니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 인덱스입니다.");
        }
    }

    static void ShowShopMenu()//상점메뉴
    {
        Console.WriteLine("상점에 오신 것을 환영합니다!");
        Console.WriteLine("[상품 목록]");
        Console.WriteLine("1. 롱소드 (공격력 +10) - 500 G");
        Console.WriteLine("2. 사각방패 (방어력 +8) - 400 G");
        Console.WriteLine("3. 스피어 (공격력 +20) - 800 G");
        Console.WriteLine("4. 풀플레이트 갑옷 (방어력 +20) - 1000 G");
        Console.WriteLine("0. 나가기");

        Console.Write("구매하고 싶은 상품의 번호를 입력해주세요. ");

        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int choice))
        {
            BuyItem(choice);
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    static void BuyItem(int choice)//아이템 구매
    {
        int cost = 0;

        switch (choice)
        {
            case 1:
                cost = 500;
                BuyWeapon("롱소드", 10, cost);
                break;
            case 2:
                cost = 400;
                BuyArmor("사각방패", 8, cost);
                break;
            case 3:
                cost = 800;
                BuyWeapon("스피어", 20, cost);
                break;
            case 4:
                cost = 1000;
                BuyArmor("풀플레이트 갑옷", 20, cost);
                break;
            case 0:
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    static void BuyWeapon(string name, int attackIncrease, int cost)//장비를 구매시 골드가있으면 지불하고 없으면 부족하다 알림이 나오게 설정하기
    {
        if (gold >= cost)
        {
            gold -= cost;
            inventory.Add(new Weapon(name, attackIncrease));
            Console.WriteLine("{name}을(를) {cost} G에 구매하였습니다!");
        }
        else
        {
            Console.WriteLine("골드가 부족합니다.");
        }
    }

    static void BuyArmor(string name, int defenseIncrease, int cost)//방어구 구매시 골드가있으면 구매되고 없으면 부족하다 알림나오게 설정
    {
        if (gold >= cost)
        {
            gold -= cost;
            inventory.Add(new Armor(name, defenseIncrease));
            Console.WriteLine($"{name}을(를) {cost} G에 구매하였습니다!");
        }
        else
        {
            Console.WriteLine("골드가 부족합니다.");
        }
    }
}


