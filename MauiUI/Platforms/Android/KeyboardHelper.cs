﻿using Android.Content;
using Android.Views.InputMethods;

namespace MauiUI.Platforms.Android;

public static partial class KeyboardHelper
{
    public static void HideKeyboard()
    {
        var context = Platform.AppContext;
        var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
        if (inputMethodManager != null)
        {
            var activity = Platform.CurrentActivity;
            var token = activity.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            activity.Window.DecorView.ClearFocus();
        }
    }
}
