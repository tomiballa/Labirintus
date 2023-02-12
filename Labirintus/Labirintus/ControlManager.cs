using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Labirintus
{
	public class ControlManager
	{
        Labirintus.MapManager mapManager;
        Labirintus.Player player;
        bool finished = false;

		public ControlManager(Labirintus.MapManager mm, Labirintus.Player p)
		{
            mapManager = mm;
            player = p;
		}

        public void tryMove(int dir)
        {
            int[,][] moveMatrix = mapManager.getMoveMatrix();

            int posX = player.getX();
            int posY = player.getY();

            if (moveMatrix[posY, posX].Contains(dir))
            {
                int nextY = dir == 1 ? posY - 1 : dir == 3 ? posY + 1 : posY;
                int nextX = dir == 2 ? posX + 1 : dir == 4 ? posX - 1 : posX;

                int isf = nextMoveFinish(nextX, nextY);

                if((isf == 1 || isf == 0) && mapManager.getDiscTreasureCount() == mapManager.getTreasureCount())
                {
                    mapManager.showSuccess("win");
                    Thread.Sleep(3000);
                    finished = true;
                }

                if (isf != -1 && isf != 3)
                {
                    mapManager.showError("cant_exit");
                    if (isf == 0) return;
                }

                if(isf != 1 && isf != 3) player.setPos(nextX, nextY);

                mapManager.setAreaDiscovered(nextY, nextX);
            }
        }

        int nextMoveFinish(int x, int y)
        {
            if (x >= mapManager.getWidth() || y >= mapManager.getHeight() || y < 0 || x < 0) return 0;
            if (mapManager.getMatrix()[y, x] == '█') return 3;
            if (mapManager.getMoveMatrix()[y, x].Length == 0) return 1;
            return -1;
        }

        void show()
        {
                mapManager.writeMap();
                mapManager.writeInfo();
        }

        public void mainLoop()
		{
            show();
            while (true)
            {
                if (finished) break;
                var ch = Console.ReadKey(true).Key;
                switch (ch)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        return;
                    case ConsoleKey.UpArrow:
                        tryMove(1);
                        break;
                    case ConsoleKey.DownArrow:
                        tryMove(3);
                        break;
                    case ConsoleKey.RightArrow:
                        tryMove(2);
                        break;
                    case ConsoleKey.LeftArrow:
                        tryMove(4);
                        break;
                }
                show();
            }
        }
	}
}

