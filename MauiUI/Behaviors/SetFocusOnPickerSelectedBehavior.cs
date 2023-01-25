namespace Maui.Behaviors;

/// <summary>
/// The <see cref="SetFocusOnPickerSelectedBehavior"/> is an attached property for pickers that allows the user to specify what <see cref="VisualElement"/> should gain focus after the user selects an element.
/// </summary>
public class SetFocusOnPickerSelectedBehavior : BaseBehavior<VisualElement>
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
        var picker = (Picker)bindable;
        var weakPicker = new WeakReference<Picker>(picker);
        picker.SelectedIndexChanged += SelectedIndexChanged;

        void SelectedIndexChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender;

            if (picker.SelectedIndex < 1)
                return;

            if (weakPicker.TryGetTarget(out var origPicker))
            {
                GetNextElement(origPicker)?.Focus();
            }
        }
    }
}
