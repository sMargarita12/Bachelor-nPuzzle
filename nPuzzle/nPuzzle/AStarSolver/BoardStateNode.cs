using System;


namespace nPuzzle.AStarSolver
{
    public class BoardStateNode : IComparable<BoardStateNode>
    {
        /// <summary>
        /// Board state array
        /// </summary>
        public byte[,] State;

        /// <summary>
        /// Row index of hole tile
        /// </summary>
        public int ZeroRow;

        /// <summary>
        /// Column index of hole tile
        /// </summary>
        public int ZeroColumn;

        /// <summary>
        /// Calculated cost F = depth (G) + heuristic(H)
        /// </summary>
        public int Cost;

        /// <summary>
        /// Depth in solution tree
        /// </summary>
        public int Depth;

        /// <summary>
        /// Unique identifier of state
        /// </summary>
        public string Keystr;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_state">Board state array</param>
        /// <param name="_0_row">Row index of hole tile</param>
        /// <param name="_0_col">Column index of hole tile</param>
        /// <param name="_depth">Depth in solution tree</param>
        /// <param name="_create_keystring">Flag: Key string will created in Ctor</param>
        public BoardStateNode(byte[,] _state, int _0_row, int _0_col, int _depth,
            bool _create_keystring = true)
        {
            State = _state.Clone() as byte[,];
            ZeroRow = _0_row;
            ZeroColumn = _0_col;
            Depth = _depth;

            Keystr = "";
            if (_create_keystring) UpdateKeyString();
        }


        /// <summary>
        /// Refresh key string
        /// </summary>
        public void UpdateKeyString()
        {
            Keystr = "|";
            for (int c = 0; c < State.GetLength(0); c++)
                for (int r = 0; r < State.GetLength(1); r++)
                    Keystr += $"{State[c, r]}|";
        }

        /// <summary>
        /// Refresh key string by template with replacing
        /// </summary>
        /// <param name="_base">Basic string</param>
        /// <param name="_swap_v1">Valie 1 for swapping</param>
        /// <param name="_swap_v2">Valie 2 for swapping</param>
        public void UpdateKeyString(string _base, int _swap_v1, int _swap_v2)
        {
            Keystr = _base.Replace($"|{_swap_v1}|", "|S|");
            Keystr = Keystr.Replace($"|{_swap_v2}|", $"|{_swap_v1}|");
            Keystr = Keystr.Replace("|S|", $"|{_swap_v2}|");
        }

        /// <summary>
        /// Comparer by Cost
        /// </summary>
        /// <param name="_otherState">Node to compare with</param>
        /// <returns>Comparsion result (-1, 0, 1)</returns>
        public int CompareTo(BoardStateNode _otherState)
            => Cost > _otherState.Cost
                ? 1
                : Cost < _otherState.Cost
                    ? -1 : 0;
    }
}