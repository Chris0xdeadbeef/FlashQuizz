using flashquizz.Models;
using System.Collections.ObjectModel;

namespace flashquizz.Services;

public class DeckService
{
    public ObservableCollection<Deck> Decks { get; private set; } = [];

    public DeckService()
    {
        // Deck 1 : Space Marines (5 cartes)
        Decks.Add(new Deck
        {
            Title = "Space Marines – Initiation",
            Cards =
            {
                new Card { Question = "Qui dirige les Space Marines ?", Answer = "L’Empereur de l’Humanité" },
                new Card { Question = "Comment s’appelle leur armure moderne ?", Answer = "Armure Mark X Primaris" },
                new Card { Question = "Quel est leur cri de guerre emblématique ?", Answer = "Pour l’Empereur !" },
                new Card { Question = "Quel chapitre est connu pour sa discipline ?", Answer = "Les Ultramarines" },
                new Card { Question = "Quel est le rôle d’un Apothicaire ?", Answer = "Récupérer les glandes progénoïdes et soigner les frères" }
            }
        });

        // Deck 2 : Orks (10 cartes)
        Decks.Add(new Deck
        {
            Title = "Orks – Waaagh! Avancée",
            Cards =
            {
                new Card { Question = "Comment les Orks appellent leur énergie psychique ?", Answer = "Le Waaagh!" },
                new Card { Question = "Comment s’appellent les chefs Orks ?", Answer = "Les Warboss" },
                new Card { Question = "Pourquoi leurs machines fonctionnent-elles ?", Answer = "Parce qu’ils croient qu’elles fonctionnent" },
                new Card { Question = "Comment s’appellent les ingénieurs Orks ?", Answer = "Les Mekboyz" },
                new Card { Question = "Quelle couleur rend les Orks plus rapides ?", Answer = "Le rouge" },
                new Card { Question = "Qui sont les spécialistes des explosifs ?", Answer = "Les Tankbustas" },
                new Card { Question = "Quel est le dieu Ork de la brutalité ?", Answer = "Gork" },
                new Card { Question = "Et celui de la ruse brutale ?", Answer = "Mork" },
                new Card { Question = "Quel est le rêve ultime d’un Ork ?", Answer = "Participer à une Waaagh! massive" },
                new Card { Question = "Comment s’appelle leur langue ?", Answer = "Le Low Gothic Orkifié" }
            }
        });
    }

    public void AddDeck(Deck deck)
    {
        Decks.Add(deck);
    }

    public void AddCard(Deck deck, Card card)
    {
        deck.Cards.Add(card);
    }
}
