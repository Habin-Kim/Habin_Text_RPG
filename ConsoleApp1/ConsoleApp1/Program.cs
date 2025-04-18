using System;

public class Program
{
    static Inventory inventory = new Inventory();
    static Shop shop = new Shop(inventory);
    static bool isGameRunning = true;

    public static void Main()
    {
        shop.AddItemShop("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000);
        shop.AddItemShop("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 1900);
        shop.AddItemShop("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500);
        shop.AddItemShop("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 600);
        shop.AddItemShop("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500);
        shop.AddItemShop("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 2100);

        while (isGameRunning)
        {
            Sparta();
        }
    }

    public static void Sparta()
    {

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

        Console.WriteLine("0. 게임 종료\n1. 상태 보기\n2. 인벤토리\n3. 상점\n");

        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해 주세요.");
            Console.Write(">> ");

            string? input = Console.ReadLine();

            if ( input == "0"|| input == "2" || input == "3" || input == "1" )
            {
                switch (input)
                {
                    case "0":

                        isGameRunning = false;
                        break;

                    case "1":
                        // 상태창!
                        Console.Clear();
                        Status.CharacterInfo();
                        break;

                    case "2":
                        // 인벤토리!
                        Console.Clear();
                        inventory.InventoryMenu();
                        break;

                    case "3":
                        // 상점!
                        Console.Clear();
                        shop.ShopMenu();
                        break;
                }
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}


public static class Status
{
    public static int level = 1;
    public static int attack = 10;
    public static int armor = 5;
    public static int hp = 100;
    public static int gold = 10000;
    public static int equippedAttack = 0;
    public static int equippedArmor = 0;

    public static void CharacterInfo()
    {
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
        Console.WriteLine($"Lv. {level}");
        Console.WriteLine("Chad ( 전사 )");
        Console.WriteLine($"공격력: {attack} (+{equippedAttack})");
        Console.WriteLine($"방어력: {armor} (+{equippedArmor})");
        Console.WriteLine($"체 력 : {hp}");
        Console.WriteLine($"Gold : {gold} G");

        while (true)
        {
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string? input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n잘못된 입력입니다.\n");
            }
        }
    }


}


public class Inventory
{
    public List<Item> myItem = new List<Item>();

    public void InventoryMenu()
    {
        while (true)
        {
            Console.WriteLine("인벤토리\n");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            ShowItems();

            Console.WriteLine("\n1. 장착 관리\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string? input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                break;
            }

            else if (input == "1")
            {
                Console.Clear();
                Equipped();
            }

            else
            {
                Console.Clear();
                Console.WriteLine("\n잘못된 입력입니다.\n");
            }
        }
    }

    public void Equipped()
    {
        Console.WriteLine("장착 관리\n");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

        while (true)
        {
            ShowItems();

            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string? input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                break;
            }

            else
            {
                int itemNum;
                bool isInt = int.TryParse(input, out itemNum);
                itemNum -= 1; // 배열 0부터 시작 입력숫자 -1

                if (isInt && itemNum >= 0 && itemNum < myItem.Count)
                {
                    if (myItem[itemNum].isEquipped)
                    {
                        Console.Clear();
                        myItem[itemNum].isEquipped = false;
                        Status.armor -= myItem[itemNum].armor;
                        Status.equippedArmor -= myItem[itemNum].armor;
                        Status.attack -= myItem[itemNum].attack;
                        Status.equippedAttack -= myItem[itemNum].attack;
                        Console.WriteLine($"\"{myItem[itemNum].name}\" 장착 해제했습니다.\n");
                    }
                    else
                    {
                        Console.Clear();
                        myItem[itemNum].isEquipped = true;
                        Status.armor += myItem[itemNum].armor;
                        Status.equippedArmor += myItem[itemNum].armor;
                        Status.attack += myItem[itemNum].attack;
                        Status.equippedAttack += myItem[itemNum].attack;
                        Console.WriteLine($"\"{myItem[itemNum].name}\" 장착했습니다.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                }
            }
        }
    }

    public void ShowItems()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < myItem.Count; i++)
        {
            Item item = myItem[i];

            string equip = "";


            if (item.isEquipped)
            {
                equip = "[E] "; // 장착 중이면 [E]를 표시
            }
            else
            {
                equip = ""; // 장착 안 된 아이템은 빈 문자
            }

            Console.Write($"- {i + 1}. {equip}{item.name} | ");


            if (item.attack > 0)
            {
                Console.Write($"공격력 +{item.attack} | ");
            }

            if (item.armor > 0)
            {
                Console.Write($"방어력 +{item.armor} | ");
            }
            Console.WriteLine($"{item.description}");
        }
    }

    public void AddItemInven(Item item)
    {
        item.isOwned = true;
        myItem.Add(item);
    }
}


public class Shop
{
    Inventory? inventory;
    public List<Item> shopItems = new List<Item>();

    public void ShopMenu()
    {
        while (true)
        {

            GoldPrint();

            ShopList(numbering: false);

            Console.WriteLine("\n1. 아이템 구매\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string? input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                break;
            }

            else if (input == "1")
            {
                Console.Clear();
                Shopping();
            }

            else
            {
                Console.Clear();
                Console.WriteLine("\n잘못된 입력입니다.\n");
            }
        }
    }

    public void Shopping()
    {       
        while (true)
        {
            ShopList(numbering: true);
            Console.WriteLine("\n구매하고 싶은 아이템의 번호를 입력해주세요.(0번 제외)");
            Console.WriteLine("\n0. 나가기");
            Console.Write(">> ");
            string? input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                break;
            }

            bool isInt = int.TryParse(input, out int itemNum);
            itemNum -= 1;

            if (isInt && itemNum >= 0 && itemNum < shopItems.Count)
            {
                Console.Clear();
                BuyItem(shopItems[itemNum]);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
    public void GoldPrint()
    {
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine($"\n[보유 골드]\n{Status.gold} G\n");
    }

    public void ShopList(bool numbering = false)
    {
        Console.WriteLine("[아이템 목록]\n");
        for (int i = 0; i < shopItems.Count; i++)
        {
            Item item = shopItems[i];
            string owned;

            if (item.isOwned)
            {
                owned = "구매완료";
            }
            else
            {
                owned = $"{item.price} G";
            }
            
            if (numbering)
            {
                Console.Write($"- {i + 1}. {item.name} | ");
            }
            else
            {
                Console.Write($"- {item.name} | ");
            }

            if (item.attack > 0)
            {
                Console.Write($"공격력 +{item.attack} | ");
            }

            if (item.armor > 0)
            {
                Console.Write($"방어력 +{item.armor} | ");
            }

            Console.WriteLine($"{item.description} | {owned}");
        }
    }

    public void BuyItem(Item item)
    {
        if (item.isOwned == true)
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
            return;
        }

        if (Status.gold >= item.price)
        {
            Status.gold -= item.price;
            inventory?.AddItemInven(item);
            item.isOwned = true;

            Console.WriteLine("구매를 완료했습니다.");
        }

        else if (Status.gold < item.price)
        {
            Console.WriteLine("Gold 가 부족합니다.");
        }

        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    public void AddItemShop(string name, string description,
                            int attack, int armor, int price)
    {
        Item item = new Item(name, description, attack, armor, price);
        shopItems.Add(item);
    }


    public Shop(Inventory inventory)
    {
        this.inventory = inventory;
    }
}


public class Item
{
    public string name;
    public string description;
    public int attack;
    public int armor;
    public int price;
    public bool isEquipped;
    public bool isOwned;

    public Item(string name, string description, int attack,
                int armor, int price)
    {
        this.name = name;
        this.description = description;
        this.attack = attack;
        this.armor = armor;
        this.price = price;
        this.isEquipped = false;
        this.isOwned = false;
    }
}