KelnerSimulator2014
===================

++ograniczona ilość naczyń  
--dosiadanie do stołu :: patrzymy na stół a nie klientów

(opcja) podmiot (opcja) czynność (opcja + opcja)

(wszyscy) kelnerzy (numer) podaj do stołu (stół + danie)
                           sprzątnij ze stołu (stół)
                           odbierz zamówienie ze stołu (stół)
                           zmywaj
                           zamiataj przy stole (stół)

Grzegorz :: funkcja parsująca input do ciągu polecenia do kolejki poleceń :: DONE  
            zamiana poleceń na rozkaz, wywołanie (chwilowo pustych) funkcji  
            lista żarcia  
            prosty generator środowiska restauracji (żeby testować interpreter)  
            ai kelnerów ::  zanim pobierze rozkaz sprawdza czy jest aktualny i :  
                            do stołu jedno danie podaje tylko jeden kelner  
                            dany kafelek/stół zamiata/sprząta tylko jeden kelner  
                            nie zmywaj jeśli brak naczyń brudnych  
                            jeśli brakuje naczyn na danie to nie zmywaj tylko powiedz (lenistwo++)  
                            od stołu zamówienia odbiera tylko jeden kelner  
