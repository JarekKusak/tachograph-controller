# Tachografový nastavovač a vyčítač - Dokumentace

## O programu

Tahle dokumentace popisuje program na vyčítání a zápis do Tachografu po linkové vrstvě (po ethernetové síti). Jde o zrenovovanou verzi původní aplikace napsanou v jazyce C# (a značkovacím jazyce XAML) v technologii WPF. Aplikace je spustitelná na operačních systémech Windows. (Je nutné podotknout, že program ještě není dokončený, spousta věcí je navrženo pouze hrubě či abstraktně, viz níže)

## Spuštění

Program ke spuštění (Tachograph.exe) lze najít v adresáři s projektem, tedy v adresáři ...\Tachograph\Tachograph\bin\Debug\net6.0-windows.

## Ovládání

Po spuštění aplikace se zobrazí okno se čtyřmi řádky, v prvním najdeme tlačítka na zápis 4 typů parametrů + tlačítko na čtení z tachografu a následný zápis vyčtených dat, zápis vyčtených packetů probíhá do textového souboru v adresáři s projektem (tedy tam, kde se nachází i .exe soubor). Ve druhém řádku se přepínají stránky. Ve třetím řádku jsou vždy zobrazeny dané stránky (jako výchozí je zobrazena stránka Nastavení). Ve stránce Nastavení se dají zapisovat parametry a označovat signály daného typu, nebo ponechat výchozí hodnoty. Čtvrtý řádek dává možnost připsat poznámku (zatím nevyužito) a zobrazuje adresář, kam se ukládají soubory (zatím nevyužito, ale existovala by možnost změny adresáře).

## Nastavení

Nastavení

## Design (XAML)

### MainWindow.xaml

MainWindow se skládá ze 4 řádků: 

- V prvním existují 4 tlačítka na zápis různých typů parametrů (program zatím funguje tak, že ze všech parametrů složí jeden velký packet, který se odešle po stisknutí tlačítka "Nastavit parametry tachografu", ale je třeba tento packet rozdělit do částí podle bloků parametrů - po stisknutí tlačítka "Nastavit parametry tachografu" se pošle pouze blok Parametry tacho, po stisknutí tlačítka "Nastavit parametry vozu" se pošle blok Parametry vozu + Typ Záznamu rychlosti + Krok záznamu(?) + Signály(?) a po stisknutí tlačítka "Nastavit počítadla" se pošle blok Počítadla, módy a typ tachografu se neposílají(?)). Dále je v řádku tlačítko na čtení a zápis z tachografu.

- Ve druhém řádku se přepínají jednotlivé stránky: Nastavování (formulář s parametry), Signály, Poznámky (TBD: Editor na IP adresy Tachografů a jejich komunikační porty).

- Ve třetím řádku jsou stránky výše zmíněného typu a účelu.

- Čtvrtý řádek je pouze pomocný, dává možnost zapsat poznámku (zatím nevyužito) a zobrazuje adresáře na ukládání souborů (zatím neimplementováno).

### SettingsPage.xaml

Hlavní účel této stránky je poskytnutí formuláře na vyplnění parametrů, které se následně zasílají na zápis. Jednotlivé parametry jsou uspořádany do bloků. Existují zde i bloky signálů, které se dají označovat. Na označení (či odznačení) celého bloku signálů zde existují tři tlačítka.

### SignalsPage.xaml

Zatím nevyužitá stránka.

### CommentPage.xaml

Zatím nevyužitá stránka.

## Dekompozice - třídy C#

### MainWindow.xaml.cs

Třída inicializuje objekty pro zápis, čtení a jednotlivé stránky. Poskytuje události vyvolané po stisknutí tlačítek.

### SettingsPage.xaml.cs

Obsahuje metody na vyzvedávání parametrů z okna. Mimo jiné obsahuje metodu na generování tlačítkových bloků s typy signálů, které se dají přepínat do dvou stavů. Bloky signálů se dají hromadně označit třemi tlačítky markingSignalButtons.

### ReadingInterface.cs

Třída slouží jako rozhraní pro čtení dat z tachografu. Je určena pro komunikaci s tachografem prostřednictvím UDP (User Datagram Protocol) a zpracování přijatých dat. Následně zaznamenává tato data do textového souboru.

### WritingInterface.cs

Třída slouží k zajištění komunikace s tachografem a provádění zápisu dat do něj. Hlavním cílem této třídy je navázání spojení s tachografem a odesílání dat včetně kontrolního součtu CRC32.

### TachographRecord.cs

Třída představuje záznam, který má být zapsán do tachografu. Tento záznam obsahuje různé parametry týkající se tachografu, vozidla, počítadla a signálů. Záznam je následně převeden na pole bajtů, které může být použito k zápisu do tachografu.

### TachographParameters.cs

Třída obsahuje dvě vlastnosti, které reprezentují parametry tachografu.

### CarParameters.cs

Třída obsahuje několik vlastností, které reprezentují různé parametry vozidla.

### CounterParameters.cs

Třída obsahuje několik vlastností, které reprezentují různé parametry týkající se počítadel.

### OtherParameters.cs

Třída obsahuje několik vlastností, které reprezentují nepřidělené parametry.

## Nedostatky v programu (co je třeba dodělat či předělat)

## Autor

Jaroslav Kusák