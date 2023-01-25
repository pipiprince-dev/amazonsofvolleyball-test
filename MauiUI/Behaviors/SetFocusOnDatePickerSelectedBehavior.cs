namespace Maui.Behaviors;

/// <summary>
/// The <see cref="SetFocusOnDatePickerSelectedBehavior"/> is an attached property for pickers that allows the user to specify what <see cref="VisualElement"/> should gain focus after the user selects an element.
/// </summary>
public class SetFocusOnDatePickerSelectedBehavior : BaseBehavior<VisualElement>
{
    /// <summary>
	/// The <see cref="NextElementProperty"/> attached property.
	/// </summary>
	public static readonly BindableProperty NextElementProperty = BindableProperty.CreateAttached(
            "NextElement",
            typeof(VisualElement),
            typeof(SetFocusOnPickerSelectedBehavior),
            default(VisualElement),
            propertyChanged: OnNextElementChanged);

    /// <summary>
    /// Required <see cref="GetNextElement"/> accessor for <see cref="NextElementProperty"/> attached property.
    /// </summary>
    public static VisualElement GetNextElement(BindableObject view) => (VisualElement)view.GetValue(NextElementProperty);

    /// <summary>
    /// Required <see cref="SetNextElement"/> accessor for <see cref="NextElementProperty"/> attached property.
    /// </summary>
    public static void SetNextElement(BindableObject view, VisualElement value) => view.SetValue(NextElementProperty, value);

    static void OnNextElementChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var datePicker = (DatePicker)bindable;
        var weakDatePicker = new WeakReference<DatePicker>(datePicker);
        datePicker.DateSelected += SelectedDateChanged;

        void SelectedDateChanged(object? sender, EventArgs e)
        {
            var picker = (DatePicker)sender;

            if (weakDatePicker.TryGetTarget(out var origPicker))
            {
                GetNextElement(origPicker)?.Focus();
            }
        }
    }
}
