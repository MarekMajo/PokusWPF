using MaterialDesignThemes.Wpf;

namespace MSK_PC_Controller.Services;

internal static class ThemeService
{
    public static void ApplyTheme(bool isDark)
    {
        var helper = new PaletteHelper();
        var theme = helper.GetTheme();
        theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
        helper.SetTheme(theme);
        AppState.IsDarkMode = isDark;
    }

    public static void ToggleTheme()
    {
        var helper = new PaletteHelper();
        var theme = helper.GetTheme();
        var toDark = theme.GetBaseTheme() != BaseTheme.Dark;
        ApplyTheme(toDark);
    }
}
