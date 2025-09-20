# Rechner für österreichische Familienbeihilfe

Quick-&-Dirty-made Tool, um das effektive Einkommen bei Bezug von Familienbeihilfe unter Berücksichtigung der Zuverdienstgrenze (und potenzieller Rückzahlungen) für Teilzeitangestellte zu berechnen.

- Aktuell werden lediglich die Werte für **2025** unterstützt.
- **Sonderzahlungen** hab ich approximiert (erstaunlich genau).
- Es wird **keine vollständige Lohn-/Gehaltsverrechnung** durchgeführt.  
  Das Tool dient mir dazu, ungefähr zu wissen, wie viel ich verdienen kann,  
  ohne die Familienbeihilfe zu verlieren – diese Rechnung kann aber stark  
  von der Realität abweichen.

## Disclaimer ⚠️

Dieses Tool ist nur als unverbindliche Hilfe gedacht.
Es liefert keine Steuerberatung und ersetzt keine Beratung durch eine Steuerberaterin oder einen Steuerberater.
Alle Werte sind ohne Gewähr und können von den tatsächlichen Beträgen abweichen.
Ich übernehme keine Verantwortung dafür, dass die Berechnungen richtig oder vollständig sind.
Für eine genaue Berechnung deiner persönlichen Situation wende dich bitte an
eine Steuerberaterin / einen Steuerberater oder das Finanzamt.

## Tech

Blazor WebAssembly (.NET 9) mit Bootstrap; Blazor WebAssembly, um sie auf GitHub Pages als Static Site hosten zu können.
