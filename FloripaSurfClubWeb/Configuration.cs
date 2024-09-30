using MudBlazor;

namespace FloripaSurfClubWeb
{
    public static class Configuration
    {
        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new Default()
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight
            {
                Primary = "#001F3F",
                Secondary = Colors.Cyan.Lighten2,
                Background = "#f5efe4",
                AppbarBackground = Colors.Blue.Lighten2,
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.White,
                DrawerText = Colors.Shades.White,
                DrawerBackground = Colors.LightBlue.Lighten4
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightBlue.Accent3,
                Secondary = Colors.LightBlue.Darken3,
                AppbarBackground = Colors.LightBlue.Accent3,
                AppbarText = Colors.Shades.Black,

            }

        };
    }
}
