using System;
using System.Collections;
using System.IO;

namespace Labirintus
{
    class MenuItem
    {
        public string type;
        public string text;
        public Action? callback;
        public string[]? selectable;
        public Action<string>? selCallback;
        public int selected;

        public MenuItem(string type, string text, Action? callback = null, string[]? selectable = null, int selected = 0, Action<string>? selCallback = null)
        {
            this.type = type;
            this.text = text;
            this.callback = callback;
            this.selectable = selectable;
            this.selected = selected;
            this.selCallback = selCallback;
        }
    }

	public class MenuManager
	{
        int cpos = 0;
        public string selectedMap = "minta.txt";
        public string selectedLang = "hu";
        bool started = false;

        List<MenuItem> items;

        public MenuManager()
		{
            string[] files = System.IO.Directory.GetFiles("./", "*.txt");

            if(files.Length == 0)
            {
                Console.WriteLine("Rakj be legalább egy pályát ugyan ebbe a mappába, majd indítsd újra a játékot.");
                Environment.Exit(0);
                return;
            }

            items = new List<MenuItem>();

            addButton("Inditas", () => started = true);
            addSelectable("Nyelv", new string[] { "hu", "en" }, (s) => selectedLang = s);
            addSelectable("Palya", files, (s) => selectedMap = s);
            addButton("Kilepes", () => Environment.Exit(0));
        }

        void addButton(string msg, Action callback)
		{
            // () => Console.WriteLine("start")
            items.Add(new MenuItem("button", msg, callback: callback));
		}

        void addSelectable(string msg, string[] selectable, Action<string> selCallback)
        {
            items.Add(new MenuItem("selectable", msg, selectable: selectable, selCallback: selCallback));
        }

        void show()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("***********************");

            for(int i = 0; i < items.Count; i++)
            {
                MenuItem item = items[i];

                Console.BackgroundColor = ConsoleColor.DarkGray;
                if (cpos == i) Console.BackgroundColor = ConsoleColor.Black;
                showMessageAtLine(Console.CursorTop + 1, item.text);

                if (item.type == "selectable") Console.Write($" < {item.selectable![item.selected]} >");
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.SetCursorPosition(1, Console.CursorTop + 2);
            Console.WriteLine("***********************");

            Console.BackgroundColor = ConsoleColor.DarkGray;
        }

        public void showMessageAtLine(int line, string msg)
        {
            Console.SetCursorPosition(1, line);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(1, currentLineCursor);
            Console.Write(msg);
        }

        public void menuLoop()
        {
            show();
            while (true)
            {
                if (started)
                {
                    foreach(MenuItem item in items)
                    {
                        if (item.selCallback != null) item.selCallback(item.selectable![item.selected]);
                    }
                    break;
                }

                var ch = Console.ReadKey(true).Key;
                switch (ch)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        return;
                    case ConsoleKey.Enter:
                        if (items[cpos].type == "button")
                        {
                            items[cpos].callback!();
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (cpos > 0) cpos -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (cpos < items.Count) cpos += 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (items[cpos].type == "selectable")
                        {
                            items[cpos].selected += 1;
                            if (items[cpos].selected >= items[cpos].selectable!.Length) items[cpos].selected = 0;
                            items[cpos].selCallback!(items[cpos].selectable![items[cpos].selected]);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (items[cpos].type == "selectable")
                        {
                            items[cpos].selected -= 1;
                            if (items[cpos].selected < 0) items[cpos].selected = items[cpos].selectable!.Length - 1;
                            items[cpos].selCallback!(items[cpos].selectable![items[cpos].selected]);
                        }
                        break;
                }
                show();
            }

            Console.Clear();
        }
    }
}

