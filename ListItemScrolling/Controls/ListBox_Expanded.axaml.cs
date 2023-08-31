using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.LogicalTree;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;

namespace ListItemScrolling.Controls
{
    [TemplatePart("MainList", typeof(ListBox_wConfirmation))]
    [TemplatePart("Button_M_Select", typeof(Button))]
    [TemplatePart("Button_M_Cancel", typeof(Button))]
    public class ListBox_Expanded : TemplatedControl
    {
        #region Fields and Members
        private ListBox_wConfirmation ref_MainList;
        private Button ref_Button_M_Select;
        private Button ref_Button_M_Cancel;
        #endregion

        #region Properties
        #region Confirmed Item
        public static readonly StyledProperty<Object?> ConfirmedItemProperty =
            AvaloniaProperty.Register<ListBox_Expanded, Object?>(nameof(ConfirmedItem));

        public Object? ConfirmedItem
        {
            get
            {
                return GetValue(ConfirmedItemProperty);
            }
            set
            {
                SetValue(ConfirmedItemProperty, value);
            }
        }
        #endregion
        #endregion

        #region Overrides
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            //Get members
            ref_MainList = e.NameScope.Find<ListBox_wConfirmation>("MainList");
            ref_Button_M_Select = e.NameScope.Find<Button>("Button_M_Select");
            ref_Button_M_Cancel = e.NameScope.Find<Button>("Button_M_Cancel");

            //Assign events
            ref_MainList.DataContextChanged += Event_DataContext_Changed;            

            ref_Button_M_Select.Tapped += Event_Button_M_Select_Tapped;
            ref_Button_M_Cancel.Tapped += Event_Button_M_Cancel_Tapped;
        }
        #endregion

        #region Events
        #region MainList - DataContext Changed
        private void Event_DataContext_Changed(object? sender, EventArgs e)
        {
            if (ref_MainList.ItemsSource is not null)
            {
                ((INotifyCollectionChanged)ref_MainList.ItemsSource).CollectionChanged -= Event_MainList_DataCollectionChanged;
                ((INotifyCollectionChanged)ref_MainList.ItemsSource).CollectionChanged += Event_MainList_DataCollectionChanged;
            }
        }
        private void Event_MainList_DataCollectionChanged(object? sender, EventArgs e)
        {
            if (ref_MainList.ItemsSource is not null)
            {
                if (((ICollection)ref_MainList.ItemsSource).Count == 0)
                {
                    ref_MainList.SelectedItem = null;
                    var old_ConfirmedItem = ref_MainList.GetLogicalChildren().OfType<ListBoxItem_wConfirmation>().FirstOrDefault(x => x.IsConfirmed == true);
                    if (old_ConfirmedItem != null)
                        old_ConfirmedItem.IsConfirmed = false;
                    ConfirmedItem = null;
                }
            }
        }
        #endregion

        #region Main Button - Select/Confirm
        private void Event_Button_M_Select_Tapped(object? sender, TappedEventArgs e)
        {
            if (ref_MainList.SelectedItem is not null)
            {
                var children_ListBoxItems = ref_MainList.GetLogicalChildren().OfType<ListBoxItem_wConfirmation>();

                //LinQ query to get the item with correct property (single item):
                var old_ConfirmedItem = children_ListBoxItems.FirstOrDefault(x => x.IsConfirmed == true);
                var new_ConfirmTargetItem = children_ListBoxItems.FirstOrDefault(x => x.DataContext == ref_MainList.SelectedItem);

                //Only take action when there is actually new item selected.
                //If the old selected item is selected again, we just ignore it.
                if (old_ConfirmedItem != new_ConfirmTargetItem)
                {
                    if (old_ConfirmedItem is not null)
                        old_ConfirmedItem.IsConfirmed = false;
                    if (new_ConfirmTargetItem is not null)
                    {
                        new_ConfirmTargetItem.IsConfirmed = true;
                        ConfirmedItem = new_ConfirmTargetItem.DataContext;
                    }
                }
            }
        }
        #endregion

        #region Main Button - Cancel
        private void Event_Button_M_Cancel_Tapped(object? sender, TappedEventArgs e)
        {
            ref_MainList.SelectedItem = null;

            //Deconfirm the selected item.
            var old_ConfirmedItem = ref_MainList.GetLogicalChildren().OfType<ListBoxItem_wConfirmation>().FirstOrDefault(x => x.IsConfirmed == true);
            if (old_ConfirmedItem is not null)
            {
                old_ConfirmedItem.IsConfirmed = false;
                ConfirmedItem = null;
            }
        }
        #endregion
        #endregion
    }
}
