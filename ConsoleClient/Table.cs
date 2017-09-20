using System;

namespace Library.DataAccessLayer.ConsoleClient
{
    static class Table
    {
        public static int Width { get; set; }
        public static string Title { get; set; }
        public static string[] ColumnNames { get; set; }

        private static int row = 1;
        private static int tempRow = 0;
        private static int maxRow = 0;

        public static void Setup()
        {
            Console.WindowWidth = Width + 1;
            CreateTitle();
            CreateHeader();
            CreateEmptyRow();
        }
        static void CreateTitle()
        {
            Console.Title = "Console Table - " + Title;
            int left = (Width / 2) - (Title.Length / 2) - 4;
            Console.SetCursorPosition(1, 1);
            for (int i = 1; i < Width; i++)
            {
                Console.Write("_");
            }
            Console.SetCursorPosition(left, row);
            Console.Write("/ " + Title + " \\");
            Console.SetCursorPosition(0, 0);
            row++;
        }
        static void CreateHeader()
        {
            int cols = ColumnNames.Length;
            int size = Width / cols;
            int i = 0;
            foreach (string col in ColumnNames)
            {
                int mid = (size / 2) - (col.Length / 2) + size * i;
                Console.SetCursorPosition(mid, row);
                Console.Write(col);
                Console.SetCursorPosition(size * i, row);
                Console.Write("|");
                i++;
            }
            Console.SetCursorPosition(Width, row);
            Console.Write("|");
            Console.SetCursorPosition(0, 0);
            row++;
        }
        static void CreateEmptyRow()
        {
            int size = Width / ColumnNames.Length;
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                Console.SetCursorPosition(i * size, row);
                Console.Write("|");
            }
            Console.SetCursorPosition(Width, row);
            Console.Write("|");
            Console.SetCursorPosition(0, 0);
            row++;
        }

        public static void Insert(int pos, string value, bool multi = false)
        {
            int size = Width / ColumnNames.Length;
            Console.SetCursorPosition(((pos + 1) - 2) * size, row);
            Console.Write("| " + value);

            if (pos == ColumnNames.Length)
            {
                Console.SetCursorPosition(Width, row);
                Console.Write("|");
            }
            else
            {
                Console.SetCursorPosition(size * pos, row);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 0);
        }

        public static void NewRow()
        {
            /*row++;
            Console.SetCursorPosition(0, row);
            for (int i = 0; i < Width; i++) { Console.Write("-"); }
            Console.SetCursorPosition(0, 0);*/
            row++;
        }

        public static void More()
        {
            row++;
            tempRow++;
        }
        public static void NoMore()
        {
            if (maxRow < tempRow)
            {
                maxRow = tempRow;
            }
            row = row - tempRow;
            tempRow = 0;
        }
        public static void DoneMore()
        {
            row += maxRow;
        }
    }
}
