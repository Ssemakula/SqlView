using System;
using System.Collections.Generic;
using System.Text;

namespace SqlView.Utilities
{
    public class GridSort
    {
        private readonly DataGridView _grid;
        private readonly BindingSource _bindingSource;
        private readonly Action? _afterSortAction;

        private readonly List<SortDefinition> _sortDefinitions = new();

        private class SortDefinition
        {
            public string Column { get; set; }
            public bool Descending { get; set; }
        }

        public GridSort(
            DataGridView grid,
            BindingSource bindingSource,
            Action? afterSortAction = null)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _bindingSource = bindingSource ?? throw new ArgumentNullException(nameof(bindingSource));
            _afterSortAction = afterSortAction;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            _grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                // BackColor = SystemColors.Control,
                // ForeColor = SystemColors.WindowText,
                Font = _grid.Font,
                WrapMode = DataGridViewTriState.True
            };
            _grid.ColumnHeaderMouseClick += OnHeaderClick;
            _grid.ColumnHeaderMouseDoubleClick += OnHeaderDoubleClick;
            _grid.CellPainting += OnCellPainting;

        }

        private void OnHeaderClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            var column = _grid.Columns[e.ColumnIndex];

            if (string.IsNullOrWhiteSpace(column.DataPropertyName))
                return;

            var existing = _sortDefinitions
                .FirstOrDefault(s => s.Column == column.DataPropertyName);

            if (existing == null)
            {
                _sortDefinitions.Add(new SortDefinition
                {
                    Column = column.DataPropertyName,
                    Descending = false
                });
            }
            else
            {
                // Has sort definition, toggle direction
                existing.Descending = !existing.Descending;
            }

            ApplySort();

            column.HeaderCell.SortGlyphDirection =
                SortOrder.Ascending;
        }

        private void OnHeaderDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            var column = _grid.Columns[e.ColumnIndex];

            _sortDefinitions.RemoveAll(s =>
                s.Column == column.DataPropertyName);

            ApplySort();
        }

        private void ApplySort()
        {
            if (_sortDefinitions.Count == 0)
            {
                _bindingSource.RemoveSort();
            }
            else
            {
                var sortString = string.Join(", ",
                    _sortDefinitions.Select(s =>
                        $"{s.Column} {(s.Descending ? "DESC" : "ASC")}"));

                _bindingSource.Sort = sortString;
            }

            _grid.Invalidate();

            _afterSortAction?.Invoke();
        }

        public void ClearSort()
        {
            _sortDefinitions.Clear();
            _bindingSource.RemoveSort();
            _afterSortAction?.Invoke();
        }

        private void UpdateSortGlyphs()
        {
            // Clear all glyphs first
            foreach (DataGridViewColumn col in _grid.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            // Apply glyphs for sorted columns
            foreach (var sort in _sortDefinitions)
            {
                var column = _grid.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => c.DataPropertyName == sort.Column);

                if (column != null)
                {
                    column.HeaderCell.SortGlyphDirection =
                        sort.Descending
                            ? SortOrder.Descending
                            : SortOrder.Ascending;
                }
            }
        }

        private void OnCellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0) // Header row
            {
                e.PaintBackground(e.ClipBounds, false);

                var column = _grid.Columns[e.ColumnIndex];

                var sortInfo = _sortDefinitions
                    .Select((s, index) => new { s, index })
                    .FirstOrDefault(x => x.s.Column == column.DataPropertyName);

                string headerText = column.HeaderText;

                if (sortInfo != null)
                {
                    int priority = sortInfo.index + 1;
                    string arrow = sortInfo.s.Descending ? "▼" : "▲";

                    headerText += $"  {arrow}{priority}";
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    headerText,
                    e.CellStyle.Font,
                    e.CellBounds,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                e.Handled = true;
            }
        }
    }
}
