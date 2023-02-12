// See https://aka.ms/new-console-template for more information
using System;
using System.Numerics;

if(!File.Exists("languages.json"))
{
    Console.WriteLine("Hiányzik a languages.json file. Kérlek rakd be ugyan ebbe a mappába.");
}

while (true)
{
    Labirintus.MenuManager menuManager = new Labirintus.MenuManager();

    menuManager.menuLoop();

    Labirintus.LanguageManager languageManager = new Labirintus.LanguageManager(menuManager.selectedLang);

    Labirintus.Player player = new Labirintus.Player();
    Labirintus.MapManager mapManager = new Labirintus.MapManager(player, languageManager, menuManager.selectedMap);
    Labirintus.ControlManager controlManager = new Labirintus.ControlManager(mapManager, player);

    Console.CursorVisible = false;

    controlManager.mainLoop();

    Console.Clear();
}