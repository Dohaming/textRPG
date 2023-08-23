using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG2
{
    public interface IPlayer
    {
        int level { get; set; }
        string name { get; set; }
        string job { get; set; }
        int attack { get; set; }
        int defense { get; set; }
        int hp { get; set; }
        int gold { get; set; }

    }

    public class Player : IPlayer
    { 
        public int level { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }

        public void Init()
        {
            level = 1;
            name = "DoHyeon";
            job = "전사";
            attack = 10;
            defense = 5;
            hp = 100;
            gold = 1500;
        }
    }

    public class Item
    {
        public bool equip;
        public string name;
        public int attack;
        public int defence;
        public string effect;
        public string content;

        public Item(bool equip, string name, int attack, int defence,string effect, string content)
        {
            this.equip = equip;
            this.name = name;
            this.attack = attack;
            this.defence = defence;
            this.effect = effect;
            this.content = content;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Player player = new Player();
            player.Init();
            List<Item> inventoryList = new List<Item>();
            inventoryList.Add(new Item(false, "무쇠갑옷", 0, 5, "방어력 +5" ,"무쇠로 만들어져 튼튼한 갑옷입니다."));
            inventoryList.Add(new Item(false, "낡은 검", 0, 2, "공격력 +2","쉽게 볼 수 있는 낡은 검입니다."));

            UI_Main();
            Console.ReadLine();
            
            void DrawUI()
            { 
                
            }

            void UI_Main()
            {
                Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n" +
                              "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n"+
                              "\n"+
                              "1. 상태 보기\n"+
                              "2. 인벤토리\n"+
                              "\n" +
                              "원하시는 행동을 입력해주세요.\n"+
                              ">> ");
                Console.SetCursorPosition(3, 7);
                int select = SelectCheck(2,false);
                if (select == 1)
                {
                    Console.Clear();
                    UI_State();
                }
                else if (select == 2)
                {
                    Console.Clear();
                    UI_Inventory();
                }
            }

            void UI_State()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("상태보기\n");
                Console.ResetColor();
                Console.Write("캐릭터의 정보가 표시됩니다.\n");
                Console.Write("\n" +
                          $"Lv. {player.level.ToString("D2")}\n" +
                          $"{player.name} ( {player.job} )\n" +
                          $"공격력 : {player.attack}\n" +
                          $"방어력 : {player.defense}\n" +
                          $"체  력 : {player.hp}\n" +
                          $"Gold   : {player.gold}\n" +
                          "\n" +
                          "0. 나가기\n" +
                          "\n" +
                          "원하시는 행동을 입력해주세요.\n" +
                          ">> ");
                Console.SetCursorPosition(3, 13);
                int select = SelectCheck(0,true);
                if (select == 0)
                {
                    Console.Clear();
                    UI_Main();
                }
            }

            void UI_Inventory()
            {
                bool exit = true;
                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("인벤토리\n");
                    Console.ResetColor();
                    Console.Write("보유 중인 아이템을 관리할 수 있습니다.\n" +
                                  "\n" +
                                  "[아이템 목록]\n");
                    for (int i = 0; i < inventoryList.Count; i++)
                    {
                        Console.Write($"- {i + 1} {CheckEquip(inventoryList[i].equip)} {inventoryList[i].name}     | {inventoryList[i].effect} | {inventoryList[i].content} \n");
                    }

                    Console.Write("\n" +
                                  "해당 아이템 번호 입력시 장착 및 해제\n" +
                                  "0. 나가기\n");

                    Console.WriteLine("\n" +
                                "원하는 행동을 입력해주세요.\n" +
                                ">> ");

                    int select = SelectCheck(inventoryList.Count + 1, true);
                    if (select == 0)
                    {
                        exit = false;
                    }
                    else
                    {
                        if (inventoryList[select - 1].equip)
                        {
                            inventoryList[select - 1].equip = false;
                        }
                        else { inventoryList[select - 1].equip = true; }
                    }
                } while (exit);


                Console.Clear();
                UI_Main();

            }

            string CheckEquip(bool b)
            {
                string result = "";
                if (b)
                {
                    return "[E]";
                }else
                return result;
            }

            void DeleteInputLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
                Console.Write(">> ");
            }

            int SelectCheck(int lenght, bool exit)
            {
                bool check = false;
                string input;
                int output = 0;
                do {
                    DeleteInputLine();
                    
                    input = Console.ReadLine();
                    check = int.TryParse(input, out _);
                    if (check)
                    {
                        output = int.Parse(input);
                        if (output > lenght)
                        {
                            check = false;
                        }
                    }

                    if (exit == false && output == 0)
                    { check = false; }
                }
                while (check == false);

                return output;
            }
        }
    }
}
