// STYLE SHEET EXAMPLE
// Basierend auf der Vorlage von Unity (https://github.com/thomasjacobsen-unity/Unity-Code-Style-Guide)

// Auszug der wichtigsten Konventionen 
// Der orginale Style Guide ist deutlich umfangreicher
// und enthält etliche Konzepte die nicht behandelt werden

// Eigene Anpassungen sind erlaubt
// Konsistente und konsequente Anwendung 


// ### BENENNUNGEN ###
// o Verwendung des Pascal Case (z.B. AnimalKeeperController, RemainingLifes) wenn nicht anders angegeben
// o Verwendung des Camel Case (z.B. animalKeeperController, remainingLifes) für private und lokale Variablen, sowie Parametern
// o Dateiname und Klassennamen müssen übereinstimmen


// ### FORMATIERUNGEN ###
// o Nicht zu lange Zeilen (Empfehlung zwischen 80 - 120 Zeichen pro Zeile)
// o Ein Leerzeichen vor Bedingungen (z.B. if (a > b))
// o Ein Leerzeichen zwischen den Argumenten einer Methode (z.B. SetPosition(0, 1, 0);)
// o Keine Leerzeichen innerhalb von Klammern (z.B. waypoints[i])
// o Öffende geschweifte Klammer in neue Zeile (Allman); Alternativ: K&R

using UnityEngine;

// ### KLASSEN (und Structs) ###
// o Bezeichnung mit einem Nomen oder Nominalpharse
// o Keine Präfixe verwenden

public class Styles : MonoBehaviour
{
    // ### ATTRIBUTE / FELDER / EIGENSCHAFTEN ###
    // o Vermeidung von Sonderzeichen
    // o Bezeichnung mit einem Nomen / Booleans mit einem Verb beginnen
    // o Öffentliche Felder (public) im Pascal Case
    // o Private Variablen (private) im Camel Case
    // o Öffentliche Felder (public) zuerst
    // o Sprechende Bezeichnungen, keine Abkürzungen
    public float JumpStrength;

    // o Nur Lese-Zugriff
    public float RunSpeed { get; private set; }

    // o Unterstrich für private Felder zu Beginn (Unterscheidung von lokalen Variablen)
    private int _remainingHealthPoints;

    // o Booleans stellen eine Frage, welche sich mit wahr (true) oder falsch (false)
    // beantworten lässt
    private bool _isTimeLeft;
    
    // ### Methoden ###
    // o Bezeichnung mit einem Verb beginnen
    public void SetDamage(int damage) 
    {
        // Reduzierde verbleibende Gesundheit
        _remainingHealthPoints -= damage;
    }

    // o Methoden die einen Boolean zurückgeben stellen eine Frage
    public bool IsGameRunning()
    {
        // Reduzierde verbleibende Gesundheit
        return _isTimeLeft && _remainingHealthPoints > 0;
    }

}
