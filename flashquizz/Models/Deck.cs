using System.Collections.ObjectModel;

namespace flashquizz.Models;

public class Deck
{
    public required string Titre { get; set; }
    public ObservableCollection<Carte> Cartes { get; set; } = [];
}
