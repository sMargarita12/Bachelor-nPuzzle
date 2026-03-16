using System;
using System.Linq;
using System.Collections.Generic;


namespace nPuzzle
{
    public class NPuzzle
    {
        const string _InvalidNExceptionMessage =
            "Size 'N' of N-Puzzle must be greather or equal of 8!";

        const string _SizeNotSquareExceptionMessage =
            "Size 'N' must be a square of any!";


        /// <summary>
        /// Get game board
        /// </summary>
        public byte[,] Board { get; set; }

        /// <summary>
        /// Game size
        /// </summary>
        public ushort N { get; private set; }

        /// <summary>
        /// Game Width
        /// </summary>
        public ushort Width => Convert.ToUInt16(Math.Sqrt(N + 1));

        /// <summary>
        /// Get access to Tile by Column & Row
        /// </summary>
        /// <param name="c">Column index</param>
        /// <param name="r">Row index</param>
        /// <returns>Value of a tile in [c, r] position</returns>
        public byte this[int c, int r] => Board[c, r];


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="N">Game size</param>
        public NPuzzle(ushort N = 15)
        {
            // Exceptions
            if (N < 8)
                throw new ArgumentException(_InvalidNExceptionMessage);
            if (Math.Sqrt(N + 1) % 1 != 0)
                throw new ArgumentException(_SizeNotSquareExceptionMessage);

            // Set Puzzle size
            this.N = N;

            // Create array
            ushort _size = Size(N);
            Board = new byte[_size, _size];

            // Generate Puzzle as default
            Generate();
        }


        /// <summary>
        /// Generates the Puzzle tiles & ensures Puzzle's resolveability
        /// </summary>
        public void Generate()
        {
            // Random generator initialize
            Random _random = new Random(Environment.TickCount);

            // Tiles for Puzzle
            List<int> _tile_nums = Enumerable.Range(0, N + 1).ToList();

            // For each tile [c,r] in Puzzle board
            for (int _c = 0; _c < Size(N); _c++)
            {
                for (int _r = 0; _r < Size(N); _r++)
                {
                    // Random selection of an value
                    int _v_index = _random.Next(0, _tile_nums.Count);
                    Board[_c, _r] = (byte)_tile_nums[_v_index];
                    _tile_nums.RemoveAt(_v_index);
                }
            }

            // Resolveable checking
            if(!Resolveable(Board, N))
            {
                // Exchanging of the the pair of largest numbers 
                // the parity of the inversions will changed.
                // It will ensure the resolveable of the Puzzle.
                
                // Find the pair of largest nubmers in tiles array
                (int _cNv, int _rNv) = FindTileByNumber((byte)N);
                (int _cN_1v, int _rN_1v) = FindTileByNumber((byte)(N - 1));

                // Exchanging
                Board[_cN_1v, _rN_1v] = (byte)N;
                Board[_cNv, _rNv] = (byte)(N - 1);
            }
        }

        /// <summary>
        /// Gets the goal state game board
        /// </summary>
        /// <returns>The goal state game board array</returns>
        public byte[,] GetGoalState()
        {
            byte _value = 1;
            ushort _size = Size(N);
            byte[,] _gs = new byte[_size, _size];
            for (short _r = 0; _r < Size(N); _r++)
                for (short _c = 0; _c < Size(N); _c++)
                    _gs[_c, _r] = _value > N ? (byte) 0 : _value++;
            return _gs;
        }

        /// <summary>
        /// Calculate the size (width) of the Puzzle
        /// </summary>
        /// <param name="N">Board tiles count</param>
        /// <returns>Puzzle widht</returns>
        private ushort Size(ushort N) => Convert.ToUInt16(Math.Sqrt(N + 1));

        /// <summary>
        /// Finds the coordinates [col, row] of the board tile with specified number
        /// </summary>
        /// <param name="_number">Specified number of the tile</param>
        /// <returns>The coordinates [col, row]</returns>
        public (int, int) FindTileByNumber(byte _number)
        {
            // Define the hole row-coordinate
            for (short _c = 0; _c < Size(N); _c++)
            {
                for (short _r = 0; _r < Size(N); _r++)
                {
                    if (Board[_c, _r] == _number)
                    {
                        return (_c, _r);
                    }
                }
            }
            // Tile not found 
            return (-1, -1);
        }
        
        /// <summary>
        /// Check puzzle for resolveable
        /// </summary>
        /// <param name="_tiles">Array of Puzzle</param>
        /// <param name="_n">Board tiles count</param>
        /// <returns>Result of checking</returns>
        protected bool Resolveable(byte[,] _tiles, ushort _n)
        {
            // Puzzle widht
            ushort _size = Size(_n);

            // Define the hole row-coordinate
            // [Item1, Item2] = [col, row] of Tiles array
            short _hole_row = (short)FindTileByNumber(0).Item2;

            // Calculate inversions
            ushort _inversions = 0;
            for (int _kf = 0; _kf < _n; _kf++)
            {
                byte _ef = _tiles[_kf % _size, (int)Math.Truncate((decimal)_kf / _size)];
                for (int _ks = _kf + 1; _ks <= _n; _ks++)
                {
                    byte _es = _tiles[_ks % _size, (int)Math.Truncate((decimal)_ks / _size)];
                    if (_es < _ef && _es != 0)
                        _inversions++;
                }
            }

            // Checking conditions
            // See: 
            //     Peter Trapa: Permutations and the 15-Puzzle
            //     January 21, 2004
            //  url: https://www.math.utah.edu/mathcircle/notes/permutations.pdf
            return (_size % 2 != 0)
                ? _inversions % 2 == 0
                : (_inversions % 2 == 0)
                    ? _hole_row % 2 != 0
                    : _hole_row % 2 == 0;
        }
    }
}