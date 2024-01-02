using MAUIDemos.Models;

namespace MAUIDemos.Views;

public partial class CollectionViewPage : ContentPage
{
	public CollectionViewPage()
	{
		InitializeComponent();

        collectionCountries.ItemsSource = GetCountries();

    }

	private List<Country> GetCountries()
	{
        return new List<Country>
        {
            new Country { Name = "United States", IsoCode = "USA", FlagUrl = "https://flagcdn.com/w320/us.png" },
            new Country { Name = "Canada", IsoCode = "CAN", FlagUrl = "https://flagcdn.com/w320/ca.png" },
            new Country { Name = "Germany", IsoCode = "DEU", FlagUrl = "https://flagcdn.com/w320/de.png" },
            new Country { Name = "Japan", IsoCode = "JPN", FlagUrl = "https://flagcdn.com/w320/jp.png" },
            new Country { Name = "Australia", IsoCode = "AUS", FlagUrl = "https://flagcdn.com/w320/au.png" },
            new Country { Name = "United Kingdom", IsoCode = "GBR", FlagUrl = "https://flagcdn.com/w320/gb.png" },
            new Country { Name = "France", IsoCode = "FRA", FlagUrl = "https://flagcdn.com/w320/fr.png" },
            new Country { Name = "Italy", IsoCode = "ITA", FlagUrl = "https://flagcdn.com/w320/it.png" },
            new Country { Name = "Spain", IsoCode = "ESP", FlagUrl = "https://flagcdn.com/w320/es.png" },
            new Country { Name = "Brazil", IsoCode = "BRA", FlagUrl = "https://flagcdn.com/w320/br.png" },
            new Country { Name = "India", IsoCode = "IND", FlagUrl = "https://flagcdn.com/w320/in.png" },
            new Country { Name = "China", IsoCode = "CHN", FlagUrl = "https://flagcdn.com/w320/cn.png" },
            new Country { Name = "Russia", IsoCode = "RUS", FlagUrl = "https://flagcdn.com/w320/ru.png" },
            new Country { Name = "South Africa", IsoCode = "ZAF", FlagUrl = "https://flagcdn.com/w320/za.png" },
            new Country { Name = "Mexico", IsoCode = "MEX", FlagUrl = "https://flagcdn.com/w320/mx.png" }
            // Add more countries as needed
        };
    }
}
