using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using UmbracoCompare;
using UmbracoDiff.Entities;
using UmbracoDiff.Helpers;

namespace UmbracoDiff.Views.Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CompareScreenView : UserControl
    {
        private CmsNodeHelper _left;
        private CmsNodeHelper _right;

        public CompareScreenView()
        {
            InitializeComponent();
        }

        // depriacated and moved to MismatchedDocTypeItemModel
        public class MismatchedDocTypeItemViewModel
        {
            public string Name { get { return Left.Text; } }
            public DocType Left { get; set; }
            public DocType Right { get; set; }
        }

        public class PropertyViewModel
        {
            public PropertyType Left { get; set; }
            public PropertyType Right { get; set; }

            public bool AreEqual
            {
                get { return new PropertyComparer().Equals(Left, Right); }
            }
        }

        private void OutputDocTypesMismatched_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataItem = e.AddedCells.FirstOrDefault().Item as MismatchedDocTypeItemViewModel;
            if (dataItem != null)
            {
                var models = new List<PropertyViewModel>();
                foreach (var leftProp in dataItem.Left.Properties)
                {
                    PropertyType prop = leftProp;
                    var rightProp = dataItem.Right.Properties.FirstOrDefault(x => x.Alias == prop.Alias) ?? new PropertyType();
                    models.Add(new PropertyViewModel {Left = leftProp, Right = rightProp});
                }

                //Let's take what's left and add it to the bottom
                foreach (var rightProp in dataItem.Right.Properties.Where(x => dataItem.Right.Properties.All(y => x.Alias != y.Alias)))
                {
                    models.Add(new PropertyViewModel {Left = new PropertyType(), Right = rightProp});
                }

                //OutputDocTypesMismatchedDetail.ItemsSource = models;
            }
        }

        private void OutputDocTypesMismatchedDetail_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataItem = e.AddedCells.FirstOrDefault().Item as PropertyViewModel;
            if (dataItem != null)
            {
                var viewer = new PropertyViewer(dataItem);
                viewer.Show();
            }
        }

        private void OutputDocTypesMismatchedDetail_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var data = e.Row.DataContext as PropertyViewModel;
            if(data != null)
            {
                if (!data.AreEqual)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
            }

        }
    }
}


