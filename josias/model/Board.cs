using System;

namespace josias
{
	public enum Color {
		WHITE,
		BLACK
	}

	public class Board
	{

		ulong whitePawns;
		ulong whiteKnights;
		ulong whiteBishops;
		ulong whiteRooks;
		ulong whiteQueens;
		ulong whiteKing;

		ulong blackPawns;
		ulong blackKnights;
		ulong blackBishops;
		ulong blackRooks;
		ulong blackQueens;
		ulong blackKing;

		public Board ()
		{
			// Initialize a standard chess board
			whitePawns = 0;
			whiteKnights = 0;
			whiteBishops = 0;
			whiteRooks = 0;
			whiteQueens = 0;
			whiteKing = 0;

			blackPawns = 0;
			blackKnights = 0;
			blackBishops = 0;
			blackRooks = 0;
			blackQueens = 0;
			blackKing = 0;
		}

		public ulong GetOccupiedSquares (Color c)
		{
			switch (c) {
			case Color.BLACK:
				return blackPawns | blackKnights | blackBishops | blackRooks | blackQueens | blackKing;
			case Color.WHITE:
				return whitePawns | whiteKnights | whiteBishops | whiteRooks | whiteQueens | whiteKing;
			}

			// Shut up, MonoDevelop.
			return ulong.MaxValue;
		}

		public ulong GetWhiteOccupiedSquares() {
			return GetOccupiedSquares (Color.WHITE);
		}

		public ulong GetBlackOccupiedSquares ()
		{
			return GetOccupiedSquares (Color.BLACK);
		}

		public ulong GetOccupiedSquares ()
		{
			return GetWhiteOccupiedSquares () | GetBlackOccupiedSquares ();
		}

		public ulong GetEmptySquares ()
		{
			return ~GetOccupiedSquares ();
		}

		public String toString ()
		{
			return "";
		}

		public static int XY (int x, int y)
		{
			return 8 * x + y;
		}
	}
}