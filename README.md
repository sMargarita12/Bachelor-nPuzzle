# n-Puzzle Solver (WinForms, .NET Framework 4.7.2)

## Übersicht

Dieses Projekt ist eine Windows-Desktopanwendung (WinForms) auf Basis von .NET Framework 4.7.2.

Es ermöglicht das Erzeugen und Lösen des klassischen n Puzzle (Schiebepuzzle, z. B. 8 Puzzle / 15 Puzzle) mit verschiedenen Suchverfahren und Heuristiken. Ergebnisse und Laufzeit-/Suchmetriken werden in der Oberfläche angezeigt.

---

## Funktionen

• Erzeugt neue n Puzzle-Spielfelder in mehreren Größen

• Löst Puzzles mit wählbaren Methoden (abhängig vom Projektumfang), z. B.:
- A* (A-Star)
- IDA* (Iterative Deepening A*)
- Bidirektional / BA*

• Unterstützt auswählbare Heuristiken, z. B.:
- Hamming
- Manhattan-Distanz
- Linear Conflict
- Pattern Database (PDB)

• Zeigt Auswertung/Metriken an, u. a.:
- benötigte Zeit
- Lösungstiefe
- Anzahl besuchter Knoten/Zustände
- Speicherverbrauch

• Optional: Experiment-/Testfenster zum Vergleichen von Heuristiken/Methoden

---

## Installation (Windows + Visual Studio)

Folge diesen Schritten, um das Projekt einzurichten und zu starten:

### Voraussetzungen installieren
- Windows 10/11
- Visual Studio 2019 oder Visual Studio 2022
- .NET Framework 4.7.2 Developer Pack (wichtig zum Bauen von net472)

### Quellcode beziehen
- Repository klonen oder als ZIP herunterladen und entpacken.

### Solution öffnen
- Die *.sln Datei in Visual Studio öffnen.

### Wiederherstellen / Build
- Visual Studio stellt Pakete meist automatisch wieder her.
- Build über:  
  Build → Build Solution

---

## Verwendung

### Startprojekt festlegen
Solution Explorer → Rechtsklick auf das WinForms-Projekt → Als Startprojekt festlegen

### In der Anwendung
- Puzzle generieren (Größe auswählen)
- Heuristik auswählen
- Lösungsmethode auswählen
- Solver starten und Metriken sowie Zielzustand ansehen

#### Hinweise zur Laufzeit
Die Laufzeit hängt stark von der gewählten Spielfeldgröße, der Heuristik, dem Suchverfahren und der konkreten Startkonfiguration ab.
Das 8-Puzzle wird in der Regel sehr schnell gelöst.
Beim 15-Puzzle können einfache Konfigurationen ebenfalls relativ schnell gelöst werden.
Für schwierige oder ungünstige Startkonfigurationen kann die Berechnung beim 15-Puzzle jedoch deutlich länger dauern und im Einzelfall bis zu 20 Minuten beanspruchen.

---

## Ausgabe

Je nach Projektkonfiguration erfolgt die Ausgabe hauptsächlich in der UI:

• Ziel-/Endzustand wird visuell angezeigt

• Ergebnisse / Statistiken wie z. B.:
- Laufzeit
- Lösungstiefe
- Anzahl besuchter Knoten
