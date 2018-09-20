using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ComboBoxWidthFromItemsDemo
{
    /// <summary>
    /// Code originally sourced from:  https://stackoverflow.com/questions/1034505/how-can-i-make-a-wpf-combo-box-have-the-width-of-its-widest-element-in-xaml
    /// </summary>
    public static class ComboBoxExtensionMethods
    {
        public static void SetWidthFromItems(this ComboBox box)
        {
            double comboBoxWidth = 19.0;

            ComboBoxAutomationPeer peer = new ComboBoxAutomationPeer(box);
            IExpandCollapseProvider provider = (IExpandCollapseProvider)peer.GetPattern(PatternInterface.ExpandCollapse);
            EventHandler handler = null;

            handler = new EventHandler(delegate
            {
                if (box.IsDropDownOpen && box.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    double width = 0.0;

                    foreach (var item in box.Items)
                    {
                        ComboBoxItem boxItem = box.ItemContainerGenerator.ContainerFromItem(item) as ComboBoxItem;
                        boxItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                        if(boxItem.DesiredSize.Width > width)
                            width = boxItem.DesiredSize.Width;
                    }
                    box.Width = comboBoxWidth + width;

                    box.ItemContainerGenerator.StatusChanged -= handler;
                    box.DropDownOpened -= handler;
                    provider.Collapse();
                }
            });

            box.ItemContainerGenerator.StatusChanged += handler;
            box.DropDownOpened += handler;
            provider.Expand();
        }
    }
}
