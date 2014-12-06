using System;
using System.Text;

/**
 * A chess board looks like this
 * 
 *  8  WBWBWBWB
 *  7  BWBWBWBW  
 *  6  WBWBWBWB  r 
 *  5  BWBWBWBW  a
 *  4  WBWBWBWB  n
 *  3  BWBWBWBW  k
 *  2  WBWBWBWB  s
 *  1  BWBWBWBW
 *     ABCDEFGH
 * 
 *      files
 * 
 * */

namespace josias
{
	public enum Color : int {
		WHITE,
		BLACK
	}

	public enum sq : ulong {
		a1 = (ulong)(1ul<<0), b1 = (ulong)(1ul<<1), c1 = (ulong)(1ul<<2), d1 = (ulong)(1ul<<3), e1 = (ulong)(1ul<<4), f1 = (ulong)(1ul<<5), g1 = (ulong)(1ul<<6), h1 = (ulong)(1ul<<7),
		a2 = (ulong)(1ul<<8), b2 = (ulong)(1ul<<9), c2 = (ulong)(1ul<<10), d2 = (ulong)(1ul<<11), e2 = (ulong)(1ul<<12), f2 = (ulong)(1ul<<13), g2 = (ulong)(1ul<<14), h2 = (ulong)(1ul<<15),
		a3 = (ulong)(1ul<<16), b3 = (ulong)(1ul<<17), c3 = (ulong)(1ul<<18), d3 = (ulong)(1ul<<19), e3 = (ulong)(1ul<<20), f3 = (ulong)(1ul<<21), g3 = (ulong)(1ul<<22), h3 = (ulong)(1ul<<23),
		a4 = (ulong)(1ul<<24), b4 = (ulong)(1ul<<25), c4 = (ulong)(1ul<<26), d4 = (ulong)(1ul<<27), e4 = (ulong)(1ul<<28), f4 = (ulong)(1ul<<29), g4 = (ulong)(1ul<<30), h4 = (ulong)(1ul<<31),
		a5 = (ulong)(1ul<<32), b5 = (ulong)(1ul<<33), c5 = (ulong)(1ul<<34), d5 = (ulong)(1ul<<35), e5 = (ulong)(1ul<<36), f5 = (ulong)(1ul<<37), g5 = (ulong)(1ul<<38), h5 = (ulong)(1ul<<39),
		a6 = (ulong)(1ul<<40), b6 = (ulong)(1ul<<41), c6 = (ulong)(1ul<<42), d6 = (ulong)(1ul<<43), e6 = (ulong)(1ul<<44), f6 = (ulong)(1ul<<45), g6 = (ulong)(1ul<<46), h6 = (ulong)(1ul<<47),
		a7 = (ulong)(1ul<<48), b7 = (ulong)(1ul<<49), c7 = (ulong)(1ul<<50), d7 = (ulong)(1ul<<51), e7 = (ulong)(1ul<<52), f7 = (ulong)(1ul<<53), g7 = (ulong)(1ul<<54), h7 = (ulong)(1ul<<55),
		a8 = (ulong)(1ul<<56), b8 = (ulong)(1ul<<57), c8 = (ulong)(1ul<<58), d8 = (ulong)(1ul<<59), e8 = (ulong)(1ul<<60), f8 = (ulong)(1ul<<61), g8 = (ulong)(1ul<<62), h8 = (ulong)(1ul<<63)
	}

	public class Board
	{
		public const ulong FULL_BOARD = ulong.MaxValue;

		ulong whitePawns;		// bonde
		ulong whiteKnights;		// springer 
		ulong whiteBishops;		// løber
		ulong whiteRooks;		// tårn
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

			whitePawns = (ulong)(sq.a7 | sq.b7 | sq.c7 | sq.d7 | sq.e7 | sq.f7 | sq.g7 | sq.h7);
			whiteKnights = (ulong)(sq.b8 | sq.g8);
			whiteBishops = (ulong)(sq.c8 | sq.f8);
			whiteRooks = (ulong)(sq.a8 | sq.h8);
			whiteQueens = (ulong) sq.d8;
			whiteKing = (ulong) sq.e8;

			blackPawns = (ulong)(sq.a2 | sq.b2 | sq.c2 | sq.d2 | sq.e2 | sq.f2 | sq.g2 | sq.h2);
			blackKnights = (ulong)(sq.b1 | sq.g1);
			blackBishops = (ulong)(sq.c1 | sq.f1);
			blackRooks = (ulong)(sq.a1 | sq.h1);
			blackQueens = (ulong) sq.d1;
			blackKing = (ulong) sq.e1;
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

		/// <summary>
		/// Returns a representation of the board suitable for output on a console. Prints the chess
		/// pieces as Unicode characters and colors the background to reflect black and white fields
		/// on the board.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="josias.Board"/>.</returns>
		public override String ToString ()
		{
			StringBuilder sb = new StringBuilder();



			for (int i = 0; i < 64; i++) {
				if ((i % 2 == 0) ^ (i % 16 > 7)) {
					sb.Append ("\x1b[47;31;2m");
				} else {
					sb.Append ("\x1b[40;31;1m");
				}
				sb.Append(
					((whitePawns >> i) & 1) == 1 ? "♙" :
					((blackPawns >> i) & 1) == 1 ? "♟" :
					((whiteKnights >> i) & 1) == 1 ? "♘" :
					((blackKnights >> i) & 1) == 1 ? "♞" :
					((whiteBishops >> i) & 1) == 1 ? "♗" :
					((blackBishops >> i) & 1) == 1 ? "♝" :
					((whiteRooks >> i) & 1) == 1 ? "♖" :
					((blackRooks >> i) & 1) == 1 ? "♜" :
					((whiteQueens >> i) & 1) == 1 ? "♕" :
					((blackQueens >> i) & 1) == 1 ? "♛" :
					((whiteKing >> i) & 1) == 1 ? "♔" :
					((blackKing >> i) & 1) == 1 ? "♚" : 
					" "
				);

				// Reset colors after each line
				sb.Append ("\x1b[0m");

				sb.Append ((i % 8 == 7) ? "\n" : "");
			}

			return sb.ToString();
		}

		public static int XY (int rank, int file)
		{
			return 8 * rank + file;
		}

		public Color negateColor(Color c) {
			return (Color) (((int) c + 1) % 2);
		}
	}
}