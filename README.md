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

# Wewnętrzne nazewnictwo dokumentów
Nazwa dokumentu składa się z nazwy przekazanego pliku oraz informacji dodatkowych wygenerowanych przez program.

Maksymalna nazwa pojedynczego pliku przekazywanego do systemu może wynosić 200 znaków. System Linux radzi sobie z nazwami plików do 255 znaków, jednak 55 znaków jest zarezerwowane na specjalne potrzeby aplikacji.

Format nazewnictwa dokumentów:
 
 `{timestamp}_{guid}_{file name}.{extension}`

Gdzie:
- `{timestamp}` - to 10 znakowy ciąg reprezentujący liczbę sekund od 1970-01-01 (system Linuxowy). Obsługiwane są wartości z zakresu od 0000000000 do 9999999999.
- `{guid}` - to 32 znakowy GUID, zapisany bez myślników rozdzielających wartości.
- `{file name}` - to oryginalna nazwa pliku.
- `{extension}` - to oryginalne rozszerzenie pliku.

Rezerwacja 55 znaków na potrzeby aplikacji jest zdefiniowana z pewnym buforem, ponieważ faktycznie aplikacja potrzebuje 49 znaków dodatkowych, aby poprawnie nazwać dokumenty na dysku. 44 znaki zarezerwowane są na prefix dokumentu, 5 znaków zarezerwowanych jest na rozszerzenie .json, dla plików z wynikami.

# Struktura pliku z wynikami
Wyniki odczytu tekstu przechowywane są w formacie JSON. Dla każdego pliku wejściowego tworzony jest dokładnie jeden plik wynikowy, którego nazwa składa się z nazwy pliku wejściowego 