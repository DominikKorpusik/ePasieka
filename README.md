# ePasieka

Jednym z problemów, z którymi borykają się pszczelarze, jest
znalezienie sposobu na przechowywanie danych o własnej pasiece, ulu,
rodzinach pszczelich, datach ostatnio przeprowadzanych inspekcji rodzin,
datach odnoszących się do ostatnio przeprowadzanych badań
weterynaryjnych. Problem polega na braku szeroko dostępnej platformy
umożliwiającej pszczelarzom przechowywanie szczegółowych danych o ich
pasiekach, ale również wspierające pracę, poprzez umożliwienie im
zapisania danych w bezpiecznym miejscu czy przyporządkowania prac do
poszczególnych dni w pasiecie. 

## Założenia

Projekt ma na celu ułatwić przechowywanie informacji zebranych
przez pszczelarzy podczas ich pracy i umożliwić im wprowadzenie danych
przy użyciu przyjaznego dla użytkownika interfejsu graficznego.

## Funkcjonalności

- Program umożliwia pszczelarzom utworzenie własnej bazy
zawierającej informacje o ich pasiekach, ulach, odkładach i rodzinach pszczelich, datach
przeprowadzanych inspekcji czy też wizyt weterynaryjnych
- Daje możliwość zarządzania wbudowanym kalendarzem, w którym pszczelarze
moga prowadzić własny terminarz, ułatwiający im pracę
- Tworzy przykładowy kalendarz wychowu matek pszczelich
- Posiada on również funkcjonalność oznaczania własnych
pasiek na mapie Google’a, manipulowania nimi oraz wyświetlania ich
- Przy każdej pasiece zostaje wyświetlona automatycznie pobrana pogoda na dany dzień
- Każdy klient aplikacji tworzy własne konto użytkownika
  - Rejestracja, Logowanie, potwierdzenie adresu email, zmiana hasła, edycja danymi użytkownika

## Technologie

W których czułem się pewniej podczas pisania projektu
- C#
- ASP.NET Core
- MVC
- Entity Framework
- Identity

Których uczyłem się dodatkowo
- CSS
- HTML
- JavaScript
- Bootstrap 

Wykorzystane podczas tworzenia projektu
- Google Maps JavaScript API
- FullCalendar
- SendGrid
- OpenWeatherMap

## Co powinno zostać zmienione/dodane

- Część metod powinna zostać zapisana jako REST API
- Wprowadzić TPL async/await 
- Poprawić odbiór danych w VIEW
- Stworzyć lepszą szatę graficzną, szczególnie dla wyświetlania pogody.
- Przeprowadzić refaktoryzację
- Dodać testy
- Dodać uwierzytelnienie dwuskładnikowe






