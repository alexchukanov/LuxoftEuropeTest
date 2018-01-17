using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GhostHouse
{
    class Program
    {
        private static int BOARD_X_SIZE;        
        private static int BOARD_Y_SIZE;


        public static string[,] board = null;
        /*
        {
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " },
            { " ", " ", "*", " ", " ", "*"," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", "*"," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", "*", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", "*", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " },
            { " ", " ", " ", " ", " ", " "," ", " ", " ", " " }
        };
        */
        

        static Random rd = new Random(); 

        static void Main(string[] args)        
        {
            const string f = "MazePlot.txt";
            int xSize = 0;

            List<char[]> lines = new List<char[]>();

            try
            {
                using (StreamReader r = new StreamReader(f))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        char[] lineArr = line.ToArray<char>();

                        lines.Add(lineArr);
                    }
                }

                BOARD_Y_SIZE = lines.Count();
                BOARD_X_SIZE = lines[0].Length;

                if (BOARD_Y_SIZE != BOARD_X_SIZE)
                    throw new Exception(String.Format("Maze should be square: X={0} != Y={1}", BOARD_X_SIZE, BOARD_Y_SIZE));
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file MazePlot.txt could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }

           
            board = new string[BOARD_Y_SIZE, BOARD_X_SIZE];

            for(int y = 0; y < BOARD_Y_SIZE; y++)
            {
                for (int x = 0; x < BOARD_X_SIZE; x++)
                {
                    board[y, x] = lines[y][x].ToString() == "X" ? "*" : " ";
                }  
            };


            eMotion motionType = eMotion.eHauntMotion;
            //eMotion motionType = eMotion.eChaosMotion;
            Coord minCoord = new Coord(0, 0);
            Coord maxCoord = new Coord(BOARD_X_SIZE-1, BOARD_Y_SIZE-1);
     
            Coord fCoord = new Coord(rd.Next(BOARD_X_SIZE - 1), rd.Next(BOARD_X_SIZE - 1));


            Ghost firstGhost = new Ghost("W", fCoord);
            Ghost secondGhost = new Ghost("M", maxCoord);           

            Desk desk = new Desk(minCoord, maxCoord, firstGhost, secondGhost, motionType);

            board[firstGhost.Coord.y, firstGhost.Coord.x] = firstGhost.Name;
            board[secondGhost.Coord.y, secondGhost.Coord.x] = secondGhost.Name;

            desk.PrintBoard();

            Console.WriteLine("Use file Maze.txt to design labirint");
            Console.WriteLine("Man is going to catch Woman. Start haunting, press Enter!");
                               
            Console.ReadLine();

            for(int i = 0; i < 200; i++)
            {
                board[firstGhost.Coord.y, firstGhost.Coord.x] = " ";                
                board[secondGhost.Coord.y, secondGhost.Coord.x] = " ";

                if (!desk.Move())
                {
                    board[firstGhost.Coord.y, firstGhost.Coord.x] = firstGhost.Name;
                    board[secondGhost.Coord.y, secondGhost.Coord.x] = secondGhost.Name;
                }
                else
                {
                    desk.PrintBoard();      
                    Console.WriteLine("I have caught you baby!");
                    Console.WriteLine("Press Enter to stop.");            
                    Console.ReadLine();
                    return;
                }
                                
                desk.PrintBoard();
                Thread.Sleep(500);
               // Console.ReadLine();
            }

            Console.WriteLine("Ha-ha-ha! You can not catch me!");
            Console.WriteLine("Press Enter to stop.");    
            Console.ReadLine();
                 
        }
    }


    class Desk
    {

        public Desk() { }
        public Desk(Coord minCoord, Coord maxCoord, Ghost firstGhost, Ghost secondGhost, eMotion motionType)
        {

            MinCoord = minCoord;
            MaxCoord = maxCoord;

            WallList = GetWalls();
            FirstGhost = firstGhost;
            SecondGhost = secondGhost;


            switch (motionType)
            {
                case eMotion.eChaosMotion:
                    Motion = new ChaosMotion(this);
                    break;
                case eMotion.eHauntMotion:
                    Motion = new HauntMotion(this);
                    break;
            }
        }

        public Coord MinCoord { get; set; }
        public Coord MaxCoord { get; set; }
        public Ghost FirstGhost { get; set; }
        public Ghost SecondGhost { get; set; }
        public List<Coord> WallList { get; set; }
        public Motion Motion { get; set; }

        public bool IsWall(Coord coord)
        {
            bool res = WallList.Contains(coord);
            return res;
        }

        public bool IsBoundary(Coord coord)
        {            return coord.x == MinCoord.x - 1 || coord.x == MaxCoord.x + 1 ||
                   coord.y == MinCoord.y - 1 || coord.y == MaxCoord.y + 1;
        }

        public bool IsFree(Coord coord)
        {
            return !IsWall(coord) && !IsBoundary(coord);
        }

        public bool Move()
        { 
            
            Motion.MoveNextSecondCoord();
            if (FirstGhost.Coord == SecondGhost.Coord)
                return true;                 
                
            
            Motion.MoveNextFirstCoord();
            if (FirstGhost.Coord == SecondGhost.Coord)
                return true;
                
            return false;
        }

        public void PrintBoard()
        {
            Console.WriteLine("----------");
            for (int x = 0; x <= MaxCoord.x; x++)
            {
                for (int y = 0; y <= MaxCoord.y; y++)
                {
                    Console.Write(Program.board[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------");
        }

        public List<Coord> GetWalls()
        {
            List<Coord> listCoord = new List<Coord>();

            for (int x = 0; x < MaxCoord.x + 1; x++)
            {
                for (int y = 0; y < MaxCoord.y + 1; y++)
                {
                    if (Program.board[y, x] == "*")
                        listCoord.Add(new Coord(x, y));
                }
            }
            return listCoord;
        }
    }

     
    class Ghost
    {
        public Ghost() { }
        public Ghost(string name, Coord coord) { Name = name; Coord = coord;}

        public string Name { get; set; }
        public Coord Coord { get; set; }      
    }

    class Motion
    {
        public Motion(){}
        public Motion(Desk desk) { Desk = desk; }

        public Desk Desk{ get; set; }
        public virtual void MoveNextSecondCoord() 
        {            
        }
        public virtual void MoveNextFirstCoord()
        {
        }       
    }

    class ChaosMotion : Motion
    {        
        public ChaosMotion(Desk desk) : base(desk){}

        public override void MoveNextSecondCoord()
        { 
            Coord coordAny = Desk.SecondGhost.Coord.Any();
            if (Desk.IsFree(coordAny))        
                Desk.SecondGhost.Coord = coordAny;            
        }

        public override void MoveNextFirstCoord()
        {
            Coord coordAny = Desk.FirstGhost.Coord.Any();
            if (Desk.IsFree(coordAny))
                Desk.FirstGhost.Coord = coordAny;
        }
    }

    class HauntMotion : Motion
    {
        public HauntMotion(Desk desk) : base(desk) { }      

        public override void MoveNextSecondCoord()
        {
            bool res = true;

            double dist = Desk.SecondGhost.Coord - Desk.FirstGhost.Coord;
            double delt = 0;            

            Coord coordAny = null;
            int i = 0;
            
            while (res)
            {
                coordAny = Desk.SecondGhost.Coord.Any();
                if (Desk.IsFree(coordAny))
                {                   
                    
                    delt = coordAny - Desk.FirstGhost.Coord;
                    res = delt > dist;
                    if (i++ == 10)
                        res = false;
                }                
            }

            Desk.SecondGhost.Coord = coordAny;
                                                       }

        public override void MoveNextFirstCoord()
        {
            bool res = true;

            double dist = Desk.SecondGhost.Coord - Desk.FirstGhost.Coord;
            double delt = 0;

            Coord coordAny = null;
            int i = 0;
            
            
            while (res)
            {                
                coordAny = Desk.FirstGhost.Coord.Any();
                if (Desk.IsFree(coordAny))
                {
                    delt = coordAny - Desk.SecondGhost.Coord;
                    res = delt < dist;
                   if (i++ == 10)
                        res = false;
                }                
            }

            Desk.FirstGhost.Coord = coordAny;
        }
    }

    class Coord
    {
        public Coord(){}
        public Coord(int x, int y) { this.x = x; this.y = y; }

        public int x;
        public int y;

        Random randomNumbers = new Random();

        public override bool Equals(object obj)
        {
            if (obj is Coord)
            {
                if (((Coord)obj).x == this.x &&
                           ((Coord)obj).y == this.y)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1}", this.x, this.y);
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !c1.Equals(c2);
        }

        public static double operator -(Coord c1, Coord c2)
        {
            double katX = Math.Pow((c2.x - c1.x), 2);
            double katY = Math.Pow((c2.y - c1.y), 2);
            return Math.Sqrt(katX + katY);
        }

        public Coord Up()
        {
            return new Coord(x, y-1);
        }

        public Coord Down()
        {
            return new Coord(x, y+1);
        }

        public Coord Left()
        {
            return new Coord(x-1, y);
        }

        public Coord Right()
        {
            return new Coord(x+1, y);
        }

        public Coord Any()
        {
            Coord coord = null ;   
            int num = randomNumbers.Next(4);

            switch (num)
            {
                case 0:
                    coord = new Coord(x, y-1);
                    break;
                case 1:
                    coord = new Coord(x, y+1);
                    break;
                case 2:
                    coord = new Coord(x-1, y);
                    break;
                case 3:
                    coord = new Coord(x+1, y);
                    break;
            }           

            return coord;
        }
    }
    

    enum eMotion
    {
        eChaosMotion,
        eHauntMotion        
    }
}
