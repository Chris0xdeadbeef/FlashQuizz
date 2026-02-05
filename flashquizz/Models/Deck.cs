using System.Collections.ObjectModel;

namespace flashquizz.Models;

/// <summary>
/// Représente un deck de cartes.
/// Contient un titre et une liste observable de cartes.
/// </summary>
public class Deck
{
    /// <summary>
    /// Titre du deck (ex : "Histoire", "Anglais", "Biologie").
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Liste observable des cartes du deck.
    /// ObservableCollection permet la mise à jour automatique de l'UI.
    /// </summary>
    public ObservableCollection<Card> Cards { get; set; } = [];

    /// <summary>
    /// Retourne le nombre de cartes sous forme de texte (ex : "5 cartes").
    /// Utile pour l'affichage dans l'interface.
    /// </summary>
    public string NbCards => $"{Cards.Count} cartes";

    /// <summary>
    /// Indique si le deck est vide (aucune carte).
    /// </summary>
    public bool IsEmpty => Cards == null || Cards.Count == 0;
}
