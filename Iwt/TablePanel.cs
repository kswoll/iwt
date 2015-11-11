using System;
using UIKit;
using CoreGraphics;
using System.Linq;
using System.Collections.Generic;

namespace Iwt
{
    public class TablePanel : Panel
    {
        public Size CellSpacing { get; set; }
        public Spacing CellPadding { get; set; }

        private TableWidth[] widths;
        private List<UIView[]> rows = new List<UIView[]>();
        private int currentRowIndex;
        private int currentColumnIndex;

        public TablePanel(params TableWidth[] widths) : base(new Style[0])
        {
            this.widths = widths;
            rows.Add(new UIView[widths.Length]);
        }

        public TablePanel(TableWidth[] widths, params Style[] styles) : base(styles)
        {
            this.widths = widths;
            rows.Add(new UIView[widths.Length]);
        }

        public override void AddSubview(UIView view)
        {
            base.AddSubview(view);

            var currentRow = rows[currentRowIndex];
            if (currentColumnIndex >= widths.Length)
            {
                currentRow = new UIView[widths.Length];
                rows.Add(currentRow);
                currentColumnIndex = 0;
                currentRowIndex++;
            }
            currentRow[currentColumnIndex] = view;
            currentColumnIndex++;
        }

        protected override void LayoutPanel(CGRect clientFrame)
        {
            var columnWidths = CalculateColumnWidths(clientFrame.Size);
            var rowHeights = CalculateRowHeights(columnWidths);

            var top = clientFrame.Top;
            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                var left = clientFrame.Left;
                for (var colIndex = 0; colIndex < widths.Length; colIndex++)
                {
                    var cell = row[colIndex];
                    cell.Frame = new CGRect(left + CellPadding.Left, top + CellPadding.Top, columnWidths[colIndex], rowHeights[rowIndex]);
                    left += columnWidths[colIndex] + CellPadding.Width + CellSpacing.Width;
                }
                top += rowHeights[rowIndex] + CellPadding.Height + CellSpacing.Height;
            }
        }

        protected override CGSize CalculatePreferredSize(CGSize availableSpace)
        {
            var columnWidths = CalculateColumnWidths(availableSpace);
            nint height = (nint)CalculateRowHeights(columnWidths).Sum(x => x);
            return new CGSize(columnWidths.Sum(x => x) + GetPaddingAndSpacingWidth(), height + GetPaddingAndSpacingHeight());
        }

        private nfloat[] CalculateRowHeights(nfloat[] columnWidths)
        {
            var result = new nfloat[rows.Count];
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                int maxHeight = 0;
                for (var j = 0; j < row.Length; j++) 
                {
                    var cell = row[j];
                    var availableCellSize = new CGSize(columnWidths[j], nfloat.MaxValue);
                    var cellHeight = cell.SizeThatFits(availableCellSize).Height;
                    if (cellHeight > maxHeight)
                        maxHeight = (int)cellHeight;
                }
//                var maxHeight = row.Select((x, index) => (nint)x.SizeThatFits().Height).Max();
                result[i] = maxHeight;
            }
            return result;
        }

        private nfloat GetPaddingAndSpacingWidth()
        {
            return CellPadding.Width * (widths.Length) +
                CellSpacing.Width * (widths.Length - 1);
        }

        private nfloat GetPaddingAndSpacingHeight()
        {
            return CellPadding.Height * (rows.Count) + 
                CellSpacing.Height * (rows.Count - 1);
        }

        private nfloat[] CalculateColumnWidths(CGSize availableSpace)
        {
            var widthsByRow = new List<nfloat[]>();
            foreach (var row in rows)
            {
                for (var i = 0; i < widths.Length; i++)
                {
                    var width = widths[i];
                    var sizeRow = new nfloat[widths.Length];
                    switch (width.Style)
                    {
                        case TableWidthStyle.SizeToFit:
                            sizeRow[i] = (nfloat)row[i].SizeThatFits(new CGSize(nfloat.MaxValue, nfloat.MaxValue)).Width;
                            break;
                        case TableWidthStyle.Fixed:
                            sizeRow[i] = width.Value;
                            break;
                        case TableWidthStyle.Percentage:
                        case TableWidthStyle.Weight:
                            sizeRow[i] = 0;
                            break;
                    }
                    widthsByRow.Add(sizeRow);
                }
            }

            var calculatedWidths = new nfloat[widths.Length];
            for (var i = 0; i < widths.Length; i++)
            {
                calculatedWidths[i] = (nfloat)widthsByRow.Max(x => x[i]);
            }
            var bespokeWidth = calculatedWidths.Sum(x => x) + GetPaddingAndSpacingWidth();
            var remainingWidth = availableSpace.Width - bespokeWidth;
            var weightWidth = (nfloat)(remainingWidth - (nfloat)((nfloat)remainingWidth * widths.Where(x => x.Style == TableWidthStyle.Percentage).Sum(x => x.Value) / 100));
            var totalWeight = widths.Where(x => x.Style == TableWidthStyle.Weight).Sum(x => x.Value);
            var individualWeightWidth = weightWidth / totalWeight;
            var oddManOutWeightWidth = weightWidth - individualWeightWidth * totalWeight;

            var result = new nfloat[widths.Length];
            for (var i = 0; i < result.Length; i++)
            {
                var width = widths[i];
                switch (width.Style)
                {
                    case TableWidthStyle.Fixed:
                    case TableWidthStyle.SizeToFit:
                        result[i] = calculatedWidths[i];
                        break;
                    case TableWidthStyle.Percentage:
                        result[i] = (nfloat)((nfloat)width.Value / 100 * remainingWidth);
                        break;
                    case TableWidthStyle.Weight:
                        result[i] = individualWeightWidth * width.Value + oddManOutWeightWidth;
                        oddManOutWeightWidth = 0;
                        break;
                }
            }
            return result;
        }
    }
}

