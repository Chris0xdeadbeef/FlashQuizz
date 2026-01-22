using System.Collections.ObjectModel;

namespace flashquizz.Models;

public class Deck
{
    public required string Title { get; set; }
    public ObservableCollection<Card> Cards { get; set; } = [];    
}
