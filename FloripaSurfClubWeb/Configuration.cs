using MudBlazor;

namespace FloripaSurfClubWeb
{
    public static class Configuration
    {
        public const string HttpClientName = "FloripaSurfClub";

        public static string BackendUrl = "";

        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new Default()
                {
                    FontFamily = new[] { "Raleway", "sans-serif" },
                    FontSize = "0.875rem", // Ajuste da fonte para ser mais legível
                    LineHeight = 1.5,     // Melhora a legibilidade com um espaçamento de linha maior
                    LetterSpacing = "0.00938em"
                },
                H1 = new H1() { FontSize = "2.25rem", FontWeight = 700 },
                H2 = new H2() { FontSize = "2rem", FontWeight = 600 },
                H3 = new H3() { FontSize = "1.75rem", FontWeight = 500 },
                H4 = new H4() { FontSize = "1.5rem", FontWeight = 500 },
                H5 = new H5() { FontSize = "1.25rem", FontWeight = 500 },
                H6 = new H6() { FontSize = "1rem", FontWeight = 500 },
                Button = new Button() { FontWeight = 600 },
            },
            PaletteLight = new PaletteLight
            {
                Primary = "#0D47A1",               // Azul mais profundo para uma presença forte
                Secondary = Colors.Cyan.Accent4,   // Cyan com mais vibração
                Background = "#F0F0F5",            // Fundo mais suave para melhorar contraste
                AppbarBackground = Colors.Blue.Darken1, // Appbar mais escura para contraste com o texto
                AppbarText = Colors.Shades.White,       // Appbar com texto claro
                TextPrimary = Colors.Shades.Black,      // Texto primário em preto para melhor legibilidade
                TextSecondary = Colors.Shades.Black,     // Texto secundário um pouco mais escuro
                DrawerBackground = Colors.BlueGray.Lighten5, // Tom mais suave para o Drawer
                DrawerText = Colors.Shades.Black,
                Success = Colors.Green.Darken1,    // Mais ênfase para mensagens de sucesso
                Info = Colors.LightBlue.Default,   // Informação clara
                Warning = Colors.Amber.Darken1,    // Cores de aviso mais impactantes
                Error = Colors.Red.Accent2         // Cores de erro vibrantes
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightBlue.Accent2,   // Cores suaves para o tema escuro
                Secondary = Colors.Cyan.Lighten1,
                Background = "#121212",               // Fundo escuro
                Surface = "#1E1E1E",                  // Cor da superfície no tema escuro
                AppbarBackground = "#212121",         // Appbar um pouco mais clara no tema escuro
                AppbarText = Colors.Shades.White,     // Texto claro na Appbar
                DrawerBackground = "#1E1E1E",         // Mantém o Drawer em um tom de cinza
                DrawerText = Colors.Shades.White,     // Texto claro no Drawer
                Success = Colors.Green.Lighten1,
                Info = Colors.Cyan.Lighten1,
                Warning = Colors.Orange.Accent2,
                Error = Colors.Red.Lighten2,
                TextPrimary = Colors.Shades.White,
                TextSecondary = Colors.Gray.Lighten1
            }
        };
    }
}
