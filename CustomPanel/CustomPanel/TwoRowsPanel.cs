using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace CustomPanel
{
    public class TwoRowsPanel: Panel
    {
        public int ItemsInFirstRow { get; set; }= 2;
        public int ItemsInSecondRow { get; set; } = 5;

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children == null || Children.Count == 0)
                return new Size(0, 0);

            double x = .0;
            double y = .0;

            int count = GetCount();
            for (int i = 0; i < count; i++)
            {
                Children[i].Arrange(new Rect(new Point(x, y), Children[i].DesiredSize ));

                x += Children[i].DesiredSize.Width;
                if (i + 1 == ItemsInFirstRow)
                {
                    x = .0;
                    y = Children[0].DesiredSize.Height;
                }
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children == null || Children.Count == 0 || ItemsInFirstRow == 0 || ItemsInSecondRow == 0)
                return new Size(0, 0);

            double firstRowItemWidth = availableSize.Width / ItemsInFirstRow;
            double secondRowItemWidth = availableSize.Width / ItemsInSecondRow;

            int count = GetCount();
            for (var i = 0; i < count; i++)
            {
                Size childAvailableSize = i + 1 <= ItemsInFirstRow ? new Size(firstRowItemWidth, availableSize.Height) : new Size(secondRowItemWidth, availableSize.Height);
                Children[i].Measure(childAvailableSize);
            }

            double childrenDesiredHeight = Children.Count <= ItemsInFirstRow ? 
                Children[0].DesiredSize.Height : Children[0].DesiredSize.Height + Children[count - 1].DesiredSize.Height;

            double height = Math.Min(availableSize.Height, childrenDesiredHeight);

            return new Size(availableSize.Width, height);
        }

        private int GetCount() => Math.Min(ItemsInFirstRow + ItemsInSecondRow, Children.Count);
    }
}
