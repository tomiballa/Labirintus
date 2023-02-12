using System;
using System.Numerics;
namespace Labirintus
{
	public class Player
	{
		private int posX;
		private int posY;
        private Labirintus.MapManager mapManager;
		private int moveCount = -1;

		public void setPos(int px, int py)
		{
            posX = px;
			posY = py;
			moveCount += 1;
        }

		public int getMoveCount()
		{
			return moveCount;
		}

		public int getX()
		{
			return posX;
		}

        public int getY()
        {
            return posY;
        }

		public Player()
		{
			
        }
	}
}

