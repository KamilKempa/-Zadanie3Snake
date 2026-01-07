using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program

{

    static void Main()

    {
        Pixel hoofd = new Pixel();

        Console.WindowHeight = 16;

        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;

        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        hoofd.xPos = screenwidth / 2;

        hoofd.yPos = screenheight / 2;

        hoofd.schermKleur = ConsoleColor.Red;

        string movement = "RIGHT";

        List<(int x, int y)> tail = new List<(int x, int y)>();

        int score = 0;

        bool increaseTail = false;
        
        DateTime tijd = DateTime.Now;

        string obstacle = "*";

        int obstacleXpos = randomnummer.Next(1, screenwidth);

        int obstacleYpos = randomnummer.Next(1, screenheight);

        List<(int x, int y)> dangerObstacles = new List<(int x, int y)>();
        string dangerObstacle = "■";

        int dangerXpos = randomnummer.Next(1, screenwidth - 2);

        int dangerYpos = randomnummer.Next(1, screenheight - 2);

        while (true)

        {

            Console.Clear();

            //Draw Obstacle

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(obstacleXpos, obstacleYpos);

            Console.Write(obstacle);

            // Draw Danger Obstacle
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var danger in dangerObstacles)
            {
                Console.SetCursorPosition(danger.x, danger.y);
                Console.Write(dangerObstacle);
            }

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");



            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, 0);

                Console.Write("■");

            }

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, screenheight - 1);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(0, i);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(screenwidth - 1, i);

                Console.Write("■");

            }

            Console.ForegroundColor =  ConsoleColor.Yellow;

            Console.WriteLine("Score: " + score);

            Console.ForegroundColor = ConsoleColor.White;

            

            //Draw Snake

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            foreach (var part in tail)

            {
    
            Console.SetCursorPosition(part.x, part.y);
            Console.Write("■");

            }



            ConsoleKeyInfo info = Console.ReadKey();

            //Game Logic

            switch (info.Key)

            {

                case ConsoleKey.UpArrow:

                    movement = "UP";

                    break;

                case ConsoleKey.DownArrow:

                    movement = "DOWN";

                    break;

                case ConsoleKey.LeftArrow:

                    movement = "LEFT";

                    break;

                case ConsoleKey.RightArrow:

                    movement = "RIGHT";

                    break;

            }

            if (movement == "UP")

                hoofd.yPos--;

            if (movement == "DOWN")

                hoofd.yPos++;

            if (movement == "LEFT")

                hoofd.xPos--;

            if (movement == "RIGHT")

                hoofd.xPos++;

            tail.Insert(0, (hoofd.xPos, hoofd.yPos));

            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
 
                score++;
 
                increaseTail = true;
  
                do

                {
    
                    obstacleXpos = randomnummer.Next(1, screenwidth - 1);
   
                    obstacleYpos = randomnummer.Next(1, screenheight - 1);

                }

                while (tail.Any(p => p.x == obstacleXpos && p.y == obstacleYpos) 
  
                       || (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos));

                int newDangerX = randomnummer.Next(1, screenwidth - 2);
                int newDangerY = randomnummer.Next(1, screenheight - 2);
                dangerObstacles.Add((newDangerX, newDangerY));
            
            }

            // jeśli NIE zjedzono → normalny ruch: usuń koniec ogona

            if (!increaseTail)

            {
  
                if (tail.Count > 0) // zabezpieczenie przed błędem

                    tail.RemoveAt(tail.Count - 1);

            }

            else

            {

                // jeśli zjedzono → ogon rośnie (nie usuwamy)

                increaseTail = false;

            }

            // Kolizja z niebezpieczną przeszkodą
            foreach (var danger in dangerObstacles)
            {
                if (hoofd.xPos == danger.x && hoofd.yPos == danger.y)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                    Console.WriteLine("Game Over - Trafiłeś w przeszkodę!");
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                    Console.WriteLine("Twój wynik: " + score);
                    Environment.Exit(0);
                }
            }


            //Kollision mit Wände oder mit sich selbst

            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)

            {

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

                Console.WriteLine("Game Over");

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

                Console.WriteLine("Dein Score ist: " + score);

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);

                Environment.Exit(0);

            }

            foreach (var part in tail.Skip(1)) // pomijamy głowę

            {
    
                if (part.x == hoofd.xPos && part.y == hoofd.yPos)
    
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                    Console.WriteLine("Game Over - Uderzyłeś w ogon!");
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                    Console.WriteLine("Wynik: " + score);
                    Environment.Exit(0);
    
                }

            }
            

            Thread.Sleep(50);

        }

    }

}




