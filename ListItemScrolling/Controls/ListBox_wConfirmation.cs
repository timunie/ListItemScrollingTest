using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListItemScrolling.Controls;

public class ListBox_wConfirmation : ListBox
{
    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new ListBoxItem_wConfirmation();
    }
    protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
    {
        return NeedsContainer<ListBoxItem_wConfirmation>(item, out recycleKey);
    }
}

[PseudoClasses(":confirmed")]//The new custom pseudo class.
public class ListBoxItem_wConfirmation : ListBoxItem
{
    #region Properties
    #region Main UI Properties
    #region Method Property - Update Status Icon
    public static readonly StyledProperty<bool> IsConfirmedProperty =
    AvaloniaProperty.Register<ListBoxItem_wConfirmation, bool>(nameof(IsConfirmed), false);

    public bool IsConfirmed
    {
        get
        {
            return GetValue(IsConfirmedProperty);
        }
        set
        {
            SetValue(IsConfirmedProperty, value);
        }
    }
    #endregion
    #endregion
    #endregion

    #region Override
    /// <summary>
    /// The entry point of new pseudo class update.
    /// </summary>
    /// <param name="change"></param>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsConfirmedProperty)
        {
            PseudoClasses.Set(":confirmed", change.GetNewValue<bool>());
        }
    }
    #endregion

    #region Method
    #endregion
}