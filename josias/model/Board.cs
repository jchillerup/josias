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

		public ulong whitePawns;		// bonde
		public ulong whiteKnights;		// springer 
		public ulong whiteBishops;		// løber
		public ulong whiteRooks;		// tårn
		public ulong whiteQueens;
		public ulong whiteKing;

		public ulong blackPawns;
		public ulong blackKnights;
		public ulong blackBishops;
		public ulong blackRooks;
		public ulong blackQueens;
		public ulong blackKing;

		/// <summary>
		/// Initializes a new instance of the <see cref="josias.Board"/> class.
		/// </summary>
		public Board ()
		{
			Initialize ();
		}

		/// <summary>
		/// Initialize the chessboard.
		/// </summary>
		public void Initialize() {
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
			/**
			 * Actually because we're representing the board h8 -> a1 we build a string where the 
			 * files are correct but the ranks are in the wrong order. We're going to do it anyway
			 * and then reverse the order of the lines at a later point
			 * */

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < 64; i++) {
				/**
				 * Figure out which color to put on the field. Every other field should be white but
				 * the order should also shift for each rank. So we divide the board into four bundles
				 * comprising two ranks and swap the color for half of the bundle (= one rank).
				 * */
				if ((i % 2 == 0) ^ (i % 16 < 8)) {
					sb.Append ("\x1b[47;31;2m");	// White
				} else {
					sb.Append ("\x1b[40;31;1m");	// Black
				}

				// TODO: Assert that no pieces are overlapping.

				// Look at bit number (64-i) and figure out what character to put on
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
				sb.Append ((i % 8 == 7) ? "\x1b[0m\n" : "");
			}

			// The board is now upside down. Let's reverse it
			String[] reversed = sb.ToString ().Split ('\n');
			Array.Reverse (reversed);
			return String.Join ("\n", reversed);
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