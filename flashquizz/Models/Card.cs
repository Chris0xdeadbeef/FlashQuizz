namespace flashquizz.Models;

/// <summary>
/// Représente une carte de révision contenant une question et une réponse.
/// Utilisée dans les decks pour l'apprentissage.
/// </summary>
public class Card
{
    /// <summary>
    /// Texte de la question affichée sur la face avant de la carte.
    /// </summary>
    public required string Question { get; set; }

    /// <summary>
    /// Texte de la réponse affichée sur la face arrière de la carte.
    /// </summary>
    public required string Answer { get; set; }
}
