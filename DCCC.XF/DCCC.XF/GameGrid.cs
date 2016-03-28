﻿using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameGrid : Grid
    {
        private readonly int _size;
        private GameCell[,] _cells;

        public GameGrid(double dimension, int size)
        {
            _size = size;
            BackgroundColor = Color.FromHex("101010");
            var spacing = dimension * .01;
            Padding = RowSpacing = ColumnSpacing = spacing;
            WidthRequest = HeightRequest = dimension;

            _cells = new GameCell[_size, _size];

            for (int index = 0; index < _size; index++)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            var childDimension = (dimension - (spacing * (size + 1))) / size;

            for (int xIndex = 0; xIndex < _size; xIndex++)
                for (int yIndex = 0; yIndex < _size; yIndex++)
                {
                    var cell = new GameCell(childDimension);
                    Children.Add(cell);
                    SetRow(cell, yIndex);
                    SetColumn(cell, xIndex);
                    _cells[xIndex, yIndex] = cell;
                }
        }

        public void Update(GameTile[,] tiles)
        {
            foreach (var cell in _cells)
                cell.Text = string.Empty;

            foreach (var tile in tiles)
            {
                if (tile == null) continue;

                _cells[tile.Position.X, tile.Position.Y].Text = tile.Value == 0 ? string.Empty : tile.Value.ToString();
            }
        }
    }
}
