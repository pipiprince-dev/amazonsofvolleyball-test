using UIKit;

namespace MauiUI.Platforms.iOS;

public static partial class KeyboardHelper
{
    public static void HideKeyboard()
    {
        UIApplication.SharedApplication.Delegate.GetWindow().EndEditing(true);
    }
}
