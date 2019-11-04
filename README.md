*EpicReader* jest aplikacją pozwalającą na odczyt tekstu z różnego typu dokumentów. Dokumenty (pliki) przesłane do aplikacji zostają umieszczone w kolejce a następnie przetwarzane jeden po drugim. Przeglądane repozytorium zawiera część programu odpowiedzialną za interakcje z użytkownikiem. Druga część programu, odpowiedzialna za odczyt tekstu z dokumentów, napisana została w języku Python oraz znajduję się w repozytorium *EpicReaderProcessor*.

Obsługiwane formaty plików:
- TODO

# Zasada działania
Aplikacja wykorzystuję system katalogów oraz odpowiednie nazewnictwo plików dzięki któremu implementuje wysoce niezawodną kolejkę, bez wykorzystywania zewnętrznych bibliotek. Implementacja oparta został na założeniu, ze operacja zmiany nazwy plików (przeniesienia pliku) jest operacją atomową w systemie Linux.

Struktura katalogów:
- temporary,
- queued,
- processing,
- processed,
- result.

# Proces odczytu dokumentu
TODO

# Nazewnictwo dokumentów
Maksymalna nazwa pojedynczego pliku przekazywanego do systemu może wynosić 128 znaków. W teorii Linux radzi sobie z nazwami do 255 znaków, jednak 127 znaków jest zarezerwowane dla aplikacji.

{timestamp}_{guid}_{filename}.{extension}

# Struktura pliku z wynikami
Wyniki odczytu tekstu przechowywane są w formacie JSON. Dla każdego pliku wejściowego tworzony jest dokładnie jeden plik wynikowy, którego nazwa składa się z nazwy pliku wejściowego 