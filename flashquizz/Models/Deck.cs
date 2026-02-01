using System.Collections.ObjectModel;

namespace flashquizz.Models;

public class Deck
{
    public required string Title { get; set; }
    public ObservableCollection<Card> Cards { get; set; } = [];
    public string NbCards => $"{Cards.Count.ToString()} cartes";
    public bool IsEmpty => Cards == null || Cards.Count == 0;

}
