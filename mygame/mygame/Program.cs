using System;
using System.Media;
using System.Timers;

namespace mygame
{
    class Program
    {
        static Int32 MeX, lives=3, MeY, OldX, OldY, Score = 0, DartX = 31, fc = 0;//add simple inventory?
        static Timer newtimer = new Timer(3000);
        static Int32[] dartYarray = new Int32[10] { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26 };
        static Int32[] treasureX = new Int32[20];
        static Int32[] treasureY = new Int32[20]; 
        static Int32[] spiderX = new Int32[60];
        static Int32[] spiderY = new Int32[60];
        static DateTime timerstart;
        static void Main(string[] args)
        {
            //flashing character may be because of erasing when not moving.
            //gameover call in the while
            Console.WindowHeight = 50;
            Console.WindowWidth = 150;
            Console.CursorVisible = false;
            MeX = Console.WindowWidth / 3;
            MeY = Console.WindowHeight - 1;
            bool hallnotdrawn = true, mainroomnotdrawn = true, hallexists = true;
            Music();
            Menu();
            timerstart = DateTime.Now;
            while (!GameOver())
            {
                Move();
                Draw(ref hallnotdrawn, ref hallexists, ref mainroomnotdrawn);
                sidemenu();
                bool gamedone=YouWin(hallexists);
                if (gamedone)
                    break;
            }
            Endscreen();
        }
        public static bool YouWin(bool areyouinhall)
        { 
            if (areyouinhall && MeY==Console.WindowHeight-2 && Score!=0)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth/3, 5);
                if(Score<40000)
                    Console.WriteLine("You Escaped with $"+Score+ " But Left Loot Behind");
                else
                    Console.WriteLine("You Escaped with $"+Score+"!");
                Console.ReadKey();
                return true;

            }
            return false;
        }
        public static void Endscreen()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            if (lives <= 0)
            {
                Console.WriteLine("                                ██╗░░░██╗░█████╗░██╗░░░██╗  ██████╗░██╗███████╗██████╗░");
                Console.WriteLine("                                ╚██╗░██╔╝██╔══██╗██║░░░██║  ██╔══██╗██║██╔════╝██╔══██╗");
                Console.WriteLine("                                ░╚████╔╝░██║░░██║██║░░░██║  ██║░░██║██║█████╗░░██║░░██║");
                Console.WriteLine("                                ░░╚██╔╝░░██║░░██║██║░░░██║  ██║░░██║██║██╔══╝░░██║░░██║");
                Console.WriteLine("                                ░░░██║░░░╚█████╔╝╚██████╔╝  ██████╔╝██║███████╗██████╔╝");
                Console.WriteLine("                                ░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚═════╝░╚═╝╚══════╝╚═════╝░");
            }
            if (Math.Abs((timerstart - DateTime.Now).TotalSeconds) > 120)
            {
                Console.WriteLine("                                ░█████╗░██╗░░░██╗████████╗  ░█████╗░███████╗  ████████╗██╗███╗░░░███╗███████╗");
                Console.WriteLine("                                ██╔══██╗██║░░░██║╚══██╔══╝  ██╔══██╗██╔════╝  ╚══██╔══╝██║████╗░████║██╔════╝");
                Console.WriteLine("                                ██║░░██║██║░░░██║░░░██║░░░  ██║░░██║█████╗░░  ░░░██║░░░██║██╔████╔██║█████╗░░");
                Console.WriteLine("                                ██║░░██║██║░░░██║░░░██║░░░  ██║░░██║██╔══╝░░  ░░░██║░░░██║██║╚██╔╝██║██╔══╝░░");
                Console.WriteLine("                                ╚█████╔╝╚██████╔╝░░░██║░░░  ╚█████╔╝██║░░░░░  ░░░██║░░░██║██║░╚═╝░██║███████╗");
                Console.WriteLine("                                ░╚════╝░░╚═════╝░░░░╚═╝░░░  ░╚════╝░╚═╝░░░░░  ░░░╚═╝░░░╚═╝╚═╝░░░░░╚═╝╚══════╝");
            }
            System.Threading.Thread.Sleep(100);
            Console.ReadKey();
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("                                ████████╗██╗░░██╗░█████╗░███╗░░██╗██╗░░██╗░██████╗  ███████╗░█████╗░██████╗░");
            Console.WriteLine("                                ╚══██╔══╝██║░░██║██╔══██╗████╗░██║██║░██╔╝██╔════╝  ██╔════╝██╔══██╗██╔══██╗");
            Console.WriteLine("                                ░░░██║░░░███████║███████║██╔██╗██║█████═╝░╚█████╗░  █████╗░░██║░░██║██████╔╝");
            Console.WriteLine("                                ░░░██║░░░██╔══██║██╔══██║██║╚████║██╔═██╗░░╚═══██╗  ██╔══╝░░██║░░██║██╔══██╗");
            Console.WriteLine("                                ░░░██║░░░██║░░██║██║░░██║██║░╚███║██║░╚██╗██████╔╝  ██║░░░░░╚█████╔╝██║░░██║");
            Console.WriteLine("                                ░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝╚═════╝░  ╚═╝░░░░░░╚════╝░╚═╝░░╚═╝\n");

            Console.WriteLine("                                ██████╗░██╗░░░░░░█████╗░██╗░░░██╗██╗███╗░░██╗░██████╗░");
            Console.WriteLine("                                ██╔══██╗██║░░░░░██╔══██╗╚██╗░██╔╝██║████╗░██║██╔════╝░");
            Console.WriteLine("                                ██████╔╝██║░░░░░███████║░╚████╔╝░██║██╔██╗██║██║░░██╗░");
            Console.WriteLine("                                ██╔═══╝░██║░░░░░██╔══██║░░╚██╔╝░░██║██║╚████║██║░░╚██╗");
            Console.WriteLine("                                ██║░░░░░███████╗██║░░██║░░░██║░░░██║██║░╚███║╚██████╔╝");
            Console.WriteLine("                                ╚═╝░░░░░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚═╝╚═╝░░╚══╝░╚═════╝░");
            Console.WriteLine("\n                                           Press any Key To Quit");
            Console.ReadKey();
        }
        public static void Music()
        {
            //found this online:https://stackoverflow.com/questions/34116886/how-to-play-background-music-in-a-c-sharp-console-application
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "IndianaJones.wav";
            player.PlayLooping();
        }
        public static bool GameOver()
        {
            bool success = false;
            if (lives <= 0 || (Math.Abs((timerstart - DateTime.Now).TotalSeconds) > 120))//Or timer hits 0 or player escapes.(if player escapes show special screen)
                success = true;
            return success;
        }
        public static void Menu()
        {
            string prologue = "You find yourself in the entrance of an abandoned temple\nAvoid booby traps and escape through the tunnel with some valuable treasure before the temple collapses!";
            //TITLE
            Console.Title = "Raiders Of The Lost Terminal";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("                      ██╗███╗░░██╗██████╗░██╗░█████╗░███╗░░██╗░█████╗░  ░░░░░██╗░█████╗░███╗░░██╗███████╗░██████╗");
            Console.WriteLine("                      ██║████╗░██║██╔══██╗██║██╔══██╗████╗░██║██╔══██╗  ░░░░░██║██╔══██╗████╗░██║██╔════╝██╔════╝");
            Console.WriteLine("                      ██║██╔██╗██║██║░░██║██║███████║██╔██╗██║███████║  ░░░░░██║██║░░██║██╔██╗██║█████╗░░╚█████╗░");
            Console.WriteLine("                      ██║██║╚████║██║░░██║██║██╔══██║██║╚████║██╔══██║  ██╗░░██║██║░░██║██║╚████║██╔══╝░░░╚═══██╗");
            Console.WriteLine("                      ██║██║░╚███║██████╔╝██║██║░░██║██║░╚███║██║░░██║  ╚█████╔╝╚█████╔╝██║░╚███║███████╗██████╔╝");
            Console.WriteLine("                      ╚═╝╚═╝░░╚══╝╚═════╝░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝  ░╚════╝░░╚════╝░╚═╝░░╚══╝╚══════╝╚═════╝░\n");
            Console.WriteLine("                    ██████╗░░█████╗░██╗██████╗░███████╗██████╗░░██████╗  ░█████╗░███████╗  ████████╗██╗░░██╗███████╗");
            Console.WriteLine("                    ██╔══██╗██╔══██╗██║██╔══██╗██╔════╝██╔══██╗██╔════╝  ██╔══██╗██╔════╝  ╚══██╔══╝██║░░██║██╔════╝");
            Console.WriteLine("                    ██████╔╝███████║██║██║░░██║█████╗░░██████╔╝╚█████╗░  ██║░░██║█████╗░░  ░░░██║░░░███████║█████╗░░");
            Console.WriteLine("                    ██╔══██╗██╔══██║██║██║░░██║██╔══╝░░██╔══██╗░╚═══██╗  ██║░░██║██╔══╝░░  ░░░██║░░░██╔══██║██╔══╝░░");
            Console.WriteLine("                    ██║░░██║██║░░██║██║██████╔╝███████╗██║░░██║██████╔╝  ╚█████╔╝██║░░░░░  ░░░██║░░░██║░░██║███████╗");
            Console.WriteLine("                    ╚═╝░░╚═╝╚═╝░░╚═╝╚═╝╚═════╝░╚══════╝╚═╝░░╚═╝╚═════╝░  ░╚════╝░╚═╝░░░░░  ░░░╚═╝░░░╚═╝░░╚═╝╚══════╝\n");

            Console.WriteLine("                    ██╗░░░░░░█████╗░░██████╗████████╗  ████████╗███████╗██████╗░███╗░░░███╗██╗███╗░░██╗░█████╗░██╗░░░░░");
            Console.WriteLine("                    ██║░░░░░██╔══██╗██╔════╝╚══██╔══╝  ╚══██╔══╝██╔════╝██╔══██╗████╗░████║██║████╗░██║██╔══██╗██║░░░░░");
            Console.WriteLine("                    ██║░░░░░██║░░██║╚█████╗░░░░██║░░░  ░░░██║░░░█████╗░░██████╔╝██╔████╔██║██║██╔██╗██║███████║██║░░░░░");
            Console.WriteLine("                    ██║░░░░░██║░░██║░╚═══██╗░░░██║░░░  ░░░██║░░░██╔══╝░░██╔══██╗██║╚██╔╝██║██║██║╚████║██╔══██║██║░░░░░");
            Console.WriteLine("                    ███████╗╚█████╔╝██████╔╝░░░██║░░░  ░░░██║░░░███████╗██║░░██║██║░╚═╝░██║██║██║░╚███║██║░░██║███████╗");
            Console.WriteLine("                    ╚══════╝░╚════╝░╚═════╝░░░░╚═╝░░░  ░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░░░░╚═╝╚═╝╚═╝░░╚══╝╚═╝░░╚═╝╚══════╝");
            //the @ before the output allows me to put a \ without it being seen as an escape character
            Console.WriteLine("                                                                  _____");
            Console.WriteLine(@"                                                                _|[]_ |_");
            Console.WriteLine(@"                                                              _/_/=|_\_\_");
            Console.WriteLine(@"                                                            _/_ /==| _\ _\_");
            Console.WriteLine(@"                                                          _/__ /===|_ _\ __\_");
            Console.WriteLine(@"                                                        _/_ _ /====| ___\  __\_");
            Console.WriteLine(@"                                                      _/ __ _/=====|_ ___\ ___ \_");
            Console.WriteLine(@"                                                    _/ ___ _/======| ____ \_ __ \_");
            Console.WriteLine("\n\n                                                      Press any Key to Continue.");
            Console.ReadKey();
            Console.Clear();
            //scrolling text
            for (int i = 0; i < prologue.Length; i++)
            {
                Console.Write(prologue[i]);
                System.Threading.Thread.Sleep(20);
            }
            Console.WriteLine("\nInstructions:");
            Console.WriteLine("USE THE ARROW KEYS TO MOVE AROUND");
            Console.WriteLine("Make sure to turn on audio");
            Console.WriteLine("Avoid the darts in the hallway");
            Console.WriteLine("Move onto the treasure room and avoid the spiders");
            Console.WriteLine("You have TWO MINUTES to pick up as much treasure as you can and escape!");
            Console.WriteLine("Escape by returning to the beginning of the hallway");
            
            Console.ReadKey();
            Console.Clear();
            }
        public static void sidemenu()
        {
            int i = 0, x = Console.WindowWidth / 3 * 2 + 2, y = Console.WindowHeight / 5;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y + i);
            Console.Write("█ █▄░█ █▀▄ █ ▄▀█ █▄░█ ▄▀█   ░░█ █▀█ █▄░█ █▀▀ █▀");
            i++;
            Console.SetCursorPosition(x, y + i);
            Console.Write("█ █░▀█ █▄▀ █ █▀█ █░▀█ █▀█   █▄█ █▄█ █░▀█ ██▄ ▄█");
            i += 10;
            Console.SetCursorPosition(x, y + i);
            Console.Write("Money: $" + Score + "                Lives:" + lives);
            i++;
            Console.SetCursorPosition(x, y + i);
            Console.WriteLine("Time: "  + Math.Abs((timerstart-DateTime.Now).TotalSeconds));
            // timer,money earned
        }
        public static void Boundary(Int32 dLwall, Int32 dRwall, Int32 dCeiling)
        {
            //these lines stop you from going through walls
            if (MeX >= dRwall)
                MeX = dRwall - 1;
            if (MeX <= dLwall)
                MeX = (dLwall + 1);
            if (MeY >= Console.WindowHeight - 1)
                MeY--;
            if (MeY < dCeiling)
                MeY++;
        }
        public static void Move()
        {
            //sets K to a placeholder key.
            ConsoleKey K = ConsoleKey.NoName;
            /*if key is pressed, read what key.
             * preemptive position change
             */
            if (Console.KeyAvailable)
            {
                K = Console.ReadKey(true).Key;
                OldX = MeX;
                OldY = MeY;
            }
            switch (K)
            {
                case ConsoleKey.UpArrow:
                    MeY--;
                    break;
                case ConsoleKey.LeftArrow:
                    MeX--;
                    break;
                case ConsoleKey.DownArrow:
                    MeY++;
                    break;
                case ConsoleKey.RightArrow:
                    MeX++;
                    break;
            }
        }
        public static void Drawhall(ref bool hallnotdrawn, ref bool hallexists, ref bool mainroomnotdrawn)
        {
            Int32 hallleftwall = Console.WindowWidth / 5, hallrightwall = Console.WindowWidth / 2, hallCeiling = 0;
            //this stops the room from being redrawn.
            if (hallnotdrawn)
            {
                //draws the hallway
                Console.CursorVisible = false;
                Console.SetCursorPosition(hallleftwall, Console.WindowHeight - 1);
                for (int i = 0; i < hallrightwall - hallleftwall; i++)
                {
                    Console.Write("~");
                }
                for (int i = 0; i <= Console.WindowHeight; i++)
                {
                    Console.Write("|\n");
                    Console.SetCursorPosition(hallrightwall, i);
                }
                for (int i = 0; i <= Console.WindowHeight; i++)
                {
                    Console.Write("|\n");
                    Console.SetCursorPosition(hallleftwall, i);
                }
            }
            if(hallexists)
                darts(hallleftwall, hallrightwall);
            hallnotdrawn = false;
            //if we are in the hallway, make the boundaries these limits
            if (hallexists)
            {
                Boundary(hallleftwall, hallrightwall, hallCeiling);
            }
            //if these conditions are met, ie: we touch the top of the terminal, change rooms back to hall
            if (!hallnotdrawn && MeX > hallleftwall && MeX < hallrightwall && MeY == Console.WindowHeight - 1)
            {
                MeY = 1;
                OldY = 0;
                Console.Clear();
                Console.CursorVisible = false;
                Console.SetCursorPosition(hallleftwall, Console.WindowHeight - 1);
                for (int i = 0; i < hallrightwall - hallleftwall; i++)
                {
                    Console.Write("~");
                }
                for (int i = 0; i <= Console.WindowHeight; i++)
                {
                    Console.Write("|\n");
                    Console.SetCursorPosition(hallrightwall, i);
                }
                for (int i = 0; i <= Console.WindowHeight; i++)
                {
                    Console.Write("|\n");
                    Console.SetCursorPosition(hallleftwall, i);
                }
                hallnotdrawn = true;
                hallexists = true;
                mainroomnotdrawn = true;
            }
        }
        public static void Drawmainroom( ref bool hallexists, ref bool mainroomnotdrawn)
        {
            Int32 leftwall = 1, rightwall = (Console.WindowWidth / 3) * 2, ceiling = 2;
            Random r = new Random();
            //draw mainroom once
            if (mainroomnotdrawn)
            {
                if (MeY <= 0)
                {
                    MeY = Console.WindowHeight - 1;
                    OldY = Console.WindowHeight;
                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(leftwall, 1);
                    for (int i = 0; i < rightwall - leftwall; i++)
                    {
                        Console.Write("_");
                    }
                    for (int i = 0; i <= Console.WindowHeight; i++)
                    {
                        Console.Write("|\n");
                        Console.SetCursorPosition(rightwall, i);
                    }
                    Console.SetCursorPosition(leftwall, 0);
                    for (int i = 0; i <= Console.WindowHeight; i++)
                    {
                        Console.Write("|\n");
                        Console.SetCursorPosition(leftwall, i);
                    }
                    spiders(leftwall, rightwall);
                    treasure(leftwall, rightwall);
                    hallexists = false;
                    mainroomnotdrawn = false;
                }
            }
            //use these boundaries when in main room
            if (!hallexists)
                Boundary(leftwall, rightwall, ceiling);
            //if i hit treasure, boost my score by a random num
            for (int i = 0; i < treasureX.Length; i++)
            {
                if (MeX == treasureX[i] && MeY == treasureY[i])
                {
                    Score += r.Next(1000, 10001);
                    treasureX[i] = 0;
                    treasureY[i] = 0;
                    break;
                }
            }
            //if i hit a spider, remove a life and remove the spider I hit
            for (int i = 0; i < spiderX.Length; i++)
            {
                if (MeX == spiderX[i] && MeY == spiderY[i])
                {
                    lives--;
                    spiderX[i] = 0;
                    spiderY[i] = 0;
                }
            }
        }
        public static void spiders(Int32 Lwall,Int32 Rwall)
        {
            Random r = new Random();
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < spiderX.Length; i++)
            {
                spiderX[i] = r.Next(Lwall + 1, Rwall);
                spiderY[i] = r.Next(Console.WindowHeight / 2,Console.WindowHeight-2);
                Console.SetCursorPosition(spiderX[i], spiderY[i]);
                Console.WriteLine("*");
            }
        }
        public static void treasure(Int32 Lwall, Int32 Rwall)
        {
            Random r = new Random();
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < treasureX.Length; i++)
            {
                treasureX[i] = r.Next(Lwall + 1, Rwall);
                treasureY[i] = r.Next(2, Console.WindowHeight / 2);
                Console.SetCursorPosition(treasureX[i], treasureY[i]);
                Console.WriteLine("O");
            }
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void Draw(ref bool hallnotdrawn, ref bool hallexists, ref bool mainroomnotdrawn)
        {
            //sets default size
            Console.WindowHeight = 50;
            Console.WindowWidth = 150;
            Console.ForegroundColor = ConsoleColor.Red;
            Drawhall(ref hallnotdrawn, ref hallexists, ref mainroomnotdrawn);
            Drawmainroom(ref hallexists, ref mainroomnotdrawn);
            string meChar = "X", erase = " ";
            if ((MeX <= Console.WindowWidth) && (MeX >= 0) && (MeY <= Console.WindowHeight) && (MeY >= 0))
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(OldX, OldY);
                Console.Write(erase);
                Console.SetCursorPosition(MeX, MeY);
                Console.Write(meChar);
            }
        }
        public static void darts(Int32 LWall, Int32 RWall)
        {
            //Draw my dart shooter
            string dart = ">", erase = " ";
            Int32 oldDartX = 0;
            for (int i = 0; i < dartYarray.Length; i++)
            {
                Console.SetCursorPosition(LWall, dartYarray[i]);
                Console.WriteLine("}");
            }
                //every 3 iterations, draw dart and erase old dart
            if (fc % 3 == 0)
            {
                oldDartX = DartX;
                DartX++;
                for (int i = 0; i < dartYarray.Length; i++)
                {
                    Console.SetCursorPosition(DartX, dartYarray[i]);
                    Console.WriteLine(dart);
                    Console.SetCursorPosition(oldDartX, dartYarray[i]);
                    Console.WriteLine(erase);
                }
                //if im hit
            }
            if (MeX == DartX && (MeY == dartYarray[0]|| MeY == dartYarray[1] || MeY == dartYarray[2] || MeY == dartYarray[3] || MeY == dartYarray[4] || MeY == dartYarray[5] || MeY == dartYarray[6] || MeY == dartYarray[7] || MeY == dartYarray[8] || MeY == dartYarray[9]))
            {
                lives--;
                for (int i = 0; i < dartYarray.Length; i++)
                {
                    Console.SetCursorPosition(DartX, dartYarray[i]);
                    Console.WriteLine(erase);
                }
                DartX = LWall + 1; 
            }
            fc++;
            //redraw when hit
            if (DartX >= RWall - 1)
            {
                for (int i = 0; i < dartYarray.Length; i++)
                {
                    Console.SetCursorPosition(DartX, dartYarray[i]);
                    Console.WriteLine(erase);
                }
                DartX = LWall + 1;
            }
            
        }
    }
}