using flashquizz.Models;
using System.Collections.ObjectModel;

namespace flashquizz.Services;

public class DeckService
{
    public ObservableCollection<Deck> Decks { get; private set; } = [];

    public void AddDeck(Deck deck)
    {
        Decks.Add(deck);
    }
}
