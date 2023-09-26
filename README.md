# Tachografový nastavovač a vyčítač - Dokumentace

## O programu

Tahle dokumentace popisuje program na vyčítání a zápis do Tachografu po linkové vrstvě (po ethernetové síti). Jde o zrenovovanou verzi původní aplikace napsanou v jazyce C# (a značkovacím jazyce XAML) v technologii WPF. (Je nutné podotknout, že program ještě není dokončený, spousta věcí je navrženo pouze hrubě či abstraktně, viz níže)

## Spuštění

Program ke spuštění (Tachograph.exe) lze najít v adresáři s projektem, tedy v adresáři ...\Tachograph\Tachograph\bin\Debug\net6.0-windows.

## Ovládání

Ovládání

## Nastavení

Nastavení

## Dekompozice - třídy C#

### MainWindow.xaml.cs



### SettingsPage.xaml.cs

Settings page

### ReadingInterface.cs

Reading interface

### WritingInterface.cs

Writing interface

### TachographRecord.cs

Tachograph record

### TachographParameters.cs

Tachograph parametry

### CarParameters.cs

Parametry vozu

### CounterParameters.cs

Počítadla parametry

### OtherParameters.cs

Ostatní parametry

## Design (XAML)

### MainWindow.xaml

MainWindow se skládá ze 4 řádků, v prvním existují 4 tlačítka na zápis různých typů parametrů (program zatím funguje tak, že ze všech parametrů složí jeden velký packet, který se odešle po stisknutí tlačítka "Nastavit parametry tachografu", ale je třeba tento packet rozdělit do částí podle bloků parametrů - po stisknutí tlačítka "Nastavit parametry tachografu" se pošle pouze blok Parametry tacho, po stisknutí tlačítka "Nastavit parametry vozu" se pošle blok Parametry vozu + Typ Záznamu rychlosti + Krok záznamu(?) + Signály(?) a po stisknutí tlačítka "Nastavit počítadla" se pošle blok Počítadla, módy a typ tachografu se neposílají(?).)

### SettingsPage.xaml

Lorem ipsum

### SignalsPage.xaml

Lorem ipsum

### CommentPage.xaml

Lorem ipsum

## Nedostatky v programu (co je třeba dodělat či předělat)

## Autor

Jaroslav Kusák
