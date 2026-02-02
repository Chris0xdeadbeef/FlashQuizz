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
    public void AddCard(Deck deck, Card card)
    {
        deck.Cards.Add(card);
    }

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
                new Card { Question = "Qui est le dieu Ork frère de Gork ?", Answer = "Mork" },
                new Card { Question = "Quel est le rêve ultime d’un Ork ?", Answer = "Participer à une Waaagh! massive" },
                new Card { Question = "Comment s’appelle leur langue ?", Answer = "Le Low Gothic Orkifié" }
            }
        });

        Decks.Add(new Deck
        {
            Title = "Adeptus Astartes – Chapitres Célèbres",
            Cards =
            {
                new Card { Question = "Quel chapitre est commandé par Marneus Calgar ?", Answer = "Les Ultramarines" },
            new Card { Question = "Quel chapitre arbore des armures noires et des croix blanches ?", Answer = "Les Black Templars" },
            new Card { Question = "Quel chapitre est originaire du monde glacé de Fenris ?", Answer = "Les Space Wolves" },
            new Card { Question = "Quel chapitre excelle dans la guerre furtive ?", Answer = "Les Raven Guard" },
            new Card { Question = "Quel chapitre est célèbre pour son affinité avec le feu ?", Answer = "Les Salamanders" }

            }
        });
        Decks.Add(new Deck
        {
            Title = "Astra Militarum – Régiments",
            Cards =
            {
                new Card { Question = "Quel régiment est originaire de Cadia ?", Answer = "Les Cadiens" },
                new Card { Question = "Quel régiment est connu pour ses troupes légères et rapides ?", Answer = "Les Catachans" },
                new Card { Question = "Quel régiment est célèbre pour ses uniformes rouges ?", Answer = "Les Mordians" },
                new Card { Question = "Quel régiment vient d’un monde désertique ?", Answer = "Les Tallarns" },
                new Card { Question = "Quel régiment est spécialisé dans les blindés ?", Answer = "Les Armageddon Steel Legion" }
            }
        });
        Decks.Add(new Deck
        {
            Title = "Adeptus Custodes – Légion d’Or",
            Cards =
            {
                new Card { Question = "Qui commande les Adeptus Custodes ?", Answer = "Le Capitaine-Général Trajann Valoris" },
            new Card { Question = "Comment se nomme leur arme emblématique à hampe ?", Answer = "La Guardian Spear" },
            new Card { Question = "Quel est leur devoir absolu ?", Answer = "Protéger l’Empereur et le Palais Impérial" },
            new Card { Question = "Comment s’appellent leurs unités rapides montées ?", Answer = "Les Vertus Praetors" },
            new Card { Question = "Quel est leur char antigrav lourd ?", Answer = "Le Caladius Grav-Tank" },
            new Card { Question = "Quel ordre combat aux côtés des Custodes contre les psykers ?", Answer = "Les Sisters of Silence" },
            new Card { Question = "De quel monde sont originaires les Custodes ?", Answer = "Terra" },
            new Card { Question = "Comment s’appelle leur armure dorée légendaire ?", Answer = "L’Armure Auramite" },
            new Card { Question = "Comment nomme-t-on leurs grandes confréries guerrières ?", Answer = "Les Shield Hosts" },
            new Card { Question = "Quelle unité assure la garde rapprochée la plus prestigieuse ?", Answer = "Les Allarus Custodians" },
            new Card { Question = "Quelle différence majeure les sépare des Space Marines ?", Answer = "Ils sont plus puissants et plus rares" },
            new Card { Question = "Quel est leur rôle face aux menaces de la Grande Faille ?", Answer = "Défendre Terra contre les incursions du Warp" },
            new Card { Question = "Comment se nomment leurs dagues énergétiques ?", Answer = "Les Misericordias" },
            new Card { Question = "Comment se caractérise leur style de combat ?", Answer = "Individuellement supérieur à presque tout guerrier de la galaxie" }
            }
        });
        Decks.Add(new Deck
        {
            Title = "Space Marines – Primaris",
            Cards =
            {
                 new Card { Question = "Qui est à l’origine de la création des Space Marines Primaris ?", Answer = "Belisarius Cawl" },
        new Card { Question = "Quel Primarque a autorisé leur déploiement à grande échelle ?", Answer = "Roboute Guilliman" },
        new Card { Question = "Comment se nomme l’armure standard portée par les Primaris ?", Answer = "Mark X Tacticus" },
        new Card { Question = "Quel rôle remplissent les Primaris par rapport aux anciens Space Marines ?", Answer = "Renforcer et moderniser les chapitres" },
        new Card { Question = "Comment appelle-t-on leur variante d’armure lourde ?", Answer = "Gravis" },
        new Card { Question = "Quelle arme à bolts est emblématique des Primaris ?", Answer = "Le Bolt Rifle" },
        new Card { Question = "Quelle unité Primaris est spécialisée dans l’assaut aérien ?", Answer = "Inceptors" },
        new Card { Question = "Quelle unité Primaris excelle dans le tir longue portée ?", Answer = "Hellblasters" },
        new Card { Question = "Quel véhicule blindé sert de transport principal aux Primaris ?", Answer = "Le Repulsor" },
        new Card { Question = "Quel dreadnought symbolise la nouvelle génération Primaris ?", Answer = "Redemptor Dreadnought" },
        new Card { Question = "Quelle unité Primaris est dédiée aux opérations furtives ?", Answer = "Reivers" },
        new Card { Question = "Quel rôle ont joué les Primaris durant la Croisade Indomitus ?", Answer = "Renforcer les mondes impériaux" },
        new Card { Question = "Quel type de lance-flammes lourd utilisent certains Primaris ?", Answer = "Pyreblaster" },
        new Card { Question = "Quelle unité Primaris est spécialisée dans la destruction de blindés ?", Answer = "Eradicators" },
        new Card { Question = "Quel véhicule antigrav léger soutient les forces Primaris ?", Answer = "Invader ATV" },
        new Card { Question = "Quel officier commande les forces Primaris sur le champ de bataille ?", Answer = "Primaris Captain" },
        new Card { Question = "Comment résumer leur doctrine tactique globale ?", Answer = "Polyvalence et supériorité technologique" },
        new Card { Question = "Quelle unité représente l’élite de mêlée Primaris ?", Answer = "Bladeguard Veterans" },
        new Card { Question = "Quelle armure est conçue pour les missions de reconnaissance et de sniper ?", Answer = "Phobos Armor" },
        new Card { Question = "Quelle unité Primaris est dédiée à la reconnaissance avancée ?", Answer = "Infiltrators" },
        new Card { Question = "Quelle unité Primaris est spécialisée dans le sabotage ?", Answer = "Incursors" },
        new Card { Question = "Quel dreadnought Primaris est orienté vers le tir lourd ?", Answer = "Le Brutalis ou Ballistus selon l’armement" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Adepta Sororitas – Foi et Flammes",
            Cards =
            {
                 new Card { Question = "Quel ordre des Sœurs de Bataille est le plus célèbre ?", Answer = "Ordre de Notre-Dame des Martyres" },
        new Card { Question = "Quelle arme symbolise le mieux les Adepta Sororitas ?", Answer = "Le Bolter" },
        new Card { Question = "Quel lance-flammes lourd est couramment utilisé par les Sœurs ?", Answer = "Heavy Flamer" },
        new Card { Question = "Quel véhicule est emblématique de leur foi ardente ?", Answer = "L’Immolator" },
        new Card { Question = "Comment se nomme l’autorité suprême d’un ordre ?", Answer = "La Chanoinesse Suprême" },
        new Card { Question = "Quel est leur rôle fondamental au sein de l’Imperium ?", Answer = "Faire respecter la foi impériale" },
        new Card { Question = "Quelle unité représente leur élite en armure lourde ?", Answer = "Les Celestians" },
        new Card { Question = "Quel ordre est réputé pour ses talents de guérison ?", Answer = "Ordre du Suaire d’Argent" },
        new Card { Question = "Quelle unité est spécialisée dans le tir lourd ?", Answer = "Retributors" },
        new Card { Question = "Quelle unité incarne la pénitence et la mêlée rapide ?", Answer = "Repentia" },
        new Card { Question = "Quelle relique incarne le cœur de leur foi ?", Answer = "Le Simulacrum Imperialis" },
        new Card { Question = "Quel transport blindé utilisent-elles le plus couramment ?", Answer = "Le Rhino" },
        new Card { Question = "Quel rôle ont-elles tenu durant la Croisade Indomitus ?", Answer = "Renforcer la foi dans les zones reconquises" },
        new Card { Question = "Quelle unité volante soutient leurs assauts ?", Answer = "Seraphim" },
        new Card { Question = "Quelle unité excelle dans le tir mobile ?", Answer = "Dominions" },
        new Card { Question = "Quel symbole sacré représente les Adepta Sororitas ?", Answer = "La Fleur de Lys" },
        new Card { Question = "De quel monde sont originaires les Sœurs de Bataille ?", Answer = "Ophelia VII" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Nécrons – Technologie Immortelle",
            Cards =
            {
                new Card { Question = "Comment se nomme le rituel qui a rendu les Nécrons immortels ?", Answer = "La Biotransférence" },
        new Card { Question = "Comment appelle-t-on les dieux stellaires asservis par les Nécrons ?", Answer = "Les C’tan" },
        new Card { Question = "Quel C’tan est surnommé le Dragon du Vide ?", Answer = "Le Nightbringer" },
        new Card { Question = "Quel C’tan serait lié à Mars selon certaines légendes ?", Answer = "Le Dragon du Vide" },
        new Card { Question = "Quel Seigneur Nécron est le plus célèbre stratège ?", Answer = "Imotekh le Seigneur des Tempêtes" },
        new Card { Question = "Quel personnage Nécron est connu pour sa folie et ses collections ?", Answer = "Trazyn l’Infini" },
        new Card { Question = "Quelle est l’unité de base de l’infanterie Nécron ?", Answer = "Le Guerrier Nécron" },
        new Card { Question = "Quelle unité d’élite fournit un tir soutenu aux Nécrons ?", Answer = "Les Immortels" },
        new Card { Question = "Quel appareil volant symbolise leur puissance technologique ?", Answer = "Le Faucheur" },
        new Card { Question = "Quel édifice de guerre massif terrorise les champs de bataille ?", Answer = "Le Monolith" },
        new Card { Question = "Quel construct Canoptek excelle au combat rapproché ?", Answer = "Le Canoptek Wraith" },
        new Card { Question = "Quelle nuée mécanique assure les réparations ?", Answer = "Les Scarabs Canoptek" },
        new Card { Question = "Quelle dynastie est considérée comme la plus puissante ?", Answer = "Sautekh" },
        new Card { Question = "Quelle dynastie est réputée pour son ancienneté ?", Answer = "Nephrekh" },
        new Card { Question = "Quelle dynastie est associée à une iconographie dorée ?", Answer = "Novokh" },
        new Card { Question = "Quelle dynastie privilégie la vitesse et la mobilité ?", Answer = "Nephrekh" },
        new Card { Question = "Quelle unité est spécialisée dans le tir longue portée ?", Answer = "Les Deathmarks" },
        new Card { Question = "Quelle unité Nécron attaque rapidement au corps à corps ?", Answer = "Les Flayed Ones" },
        new Card { Question = "Comment se nomme leur technologie de téléportation ?", Answer = "Les Portails de Translocation" },
        new Card { Question = "Quel est l’objectif ultime des Nécrons ?", Answer = "Reconquérir leurs mondes-tombes" },
        new Card { Question = "Quel lien historique les unit aux Anciens ?", Answer = "Ils ont été manipulés par les C’tan" },
        new Card { Question = "Quelle caste dirige les armées Nécrons ?", Answer = "Overlords" },
        new Card { Question = "Quels savants contrôlent leur technologie avancée ?", Answer = "Crypteks" },
        new Card { Question = "Quelle arme Gauss équipe les guerriers Nécrons ?", Answer = "Le Gauss Flayer" },
        new Card { Question = "Quelle arme lourde Gauss est utilisée contre les blindés ?", Answer = "Le Gauss Cannon" },
        new Card { Question = "Quelle unité combine vitesse et puissance de feu ?", Answer = "Les Tomb Blades" },
        new Card { Question = "Quelle machine est dédiée au siège et à l’annihilation ?", Answer = "Le Doomsday Ark" },
        new Card { Question = "Quelle variante d’Immortels utilise la technologie Tesla ?", Answer = "Les Tesla Immortals" }
        }
        });
        Decks.Add(new Deck
        {
            Title = "Tyranides – Essaims Voraces",
            Cards =
            {
               new Card { Question = "Comment se nomme l’intelligence collective des Tyranides ?", Answer = "L’Esprit de la Ruche" },
        new Card { Question = "Quelle créature tyranide dirige les essaims sur le champ de bataille ?", Answer = "Le Hive Tyrant" },
        new Card { Question = "Quelle créature tyranide est spécialisée dans le tir lourd ?", Answer = "Le Tyrannofex" },
        new Card { Question = "Quelle créature tyranide domine les airs ?", Answer = "Le Harpy" },
        new Card { Question = "Quelle unité tyranide est connue pour sa mêlée rapide ?", Answer = "Les Hormagaunts" },
        new Card { Question = "Quelle unité constitue la base des essaims tyranides ?", Answer = "Les Termagants" },
        new Card { Question = "Quelle créature fournit un puissant soutien psychique ?", Answer = "Le Zoanthrope" },
        new Card { Question = "Quelle créature tyranide est spécialisée dans les attaques psychiques ?", Answer = "Le Neurothrope" },
        new Card { Question = "Quelle créature tyranide est utilisée pour le siège à distance ?", Answer = "Le Exocrine" },
        new Card { Question = "Quel commandant secondaire coordonne les créatures mineures ?", Answer = "Le Tyranid Prime" },
        new Card { Question = "Quelle créature tyranide est un monstre emblématique de mêlée ?", Answer = "Le Carnifex" },
        new Card { Question = "Quelle unité tyranide excelle dans le tir mobile ?", Answer = "Le Hive Guard" },
        new Card { Question = "Quelle unité tyranide forme des nuées dévoreuses ?", Answer = "Les Rippers" },
        new Card { Question = "Quelle créature tyranide attaque depuis les profondeurs ?", Answer = "Le Trygon" },
        new Card { Question = "Quelle créature tyranide est connue pour sa voracité extrême ?", Answer = "Le Haruspex" },
        new Card { Question = "Quelle flotte-ruche tyranide est la plus célèbre ?", Answer = "Behemoth" },
        new Card { Question = "Quelle flotte-ruche est réputée pour sa vitesse et son adaptation ?", Answer = "Kraken" },
        new Card { Question = "Quelle flotte-ruche privilégie la saturation de tir ?", Answer = "Leviathan" },
        new Card { Question = "Quel est l’objectif ultime des Tyranides ?", Answer = "Dévorer toute biomasse" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "T’au – Technologie Avancée",
            Cards =
    {
        new Card { Question = "Comment se nomme l’idéologie qui guide l’Empire T’au ?", Answer = "Le Bien Suprême" },
        new Card { Question = "Quelle caste T’au est responsable des forces militaires ?", Answer = "La Caste du Feu" },
        new Card { Question = "Quelle caste est spécialisée dans la diplomatie ?", Answer = "La Caste de l’Eau" },
        new Card { Question = "Quelle caste développe la technologie T’au ?", Answer = "La Caste de la Terre" },
        new Card { Question = "Quelle caste est chargée du transport et de la logistique ?", Answer = "La Caste de l’Air" },
        new Card { Question = "Quel commandant T’au est devenu une légende renégate ?", Answer = "Commander Farsight" },
        new Card { Question = "Quelle exo-armure lourde est emblématique des T’au ?", Answer = "La XV104 Riptide" },
        new Card { Question = "Quelle exo-armure est réputée pour sa grande mobilité ?", Answer = "La XV86 Coldstar" },
        new Card { Question = "Quel drone est le plus utilisé sur le champ de bataille ?", Answer = "Le Gun Drone" },
        new Card { Question = "Quel transport blindé est utilisé par l’infanterie T’au ?", Answer = "Le Devilfish" },
        new Card { Question = "Quel char T’au est spécialisé dans le tir antichar ?", Answer = "Le Hammerhead" },
        new Card { Question = "Quelle unité T’au fournit un tir longue portée massif ?", Answer = "Les Broadsides" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Drukhari – Cruauté et Vitesse",
            Cards =
    {
        new Card { Question = "Quelle cité obscure sert de capitale aux Drukhari ?", Answer = "Commorragh" },
        new Card { Question = "Comment se nomment les motojets rapides des Drukhari ?", Answer = "Les Reavers" },
        new Card { Question = "Quel transport rapide est emblématique des Drukhari ?", Answer = "Le Raider" },
        new Card { Question = "Quel dirigeant tyrannique règne sur Commorragh ?", Answer = "Asdrubael Vect" },
        new Card { Question = "Quel groupe est spécialisé dans les combats d’arène ?", Answer = "Les Cultes des Hémoncules" },
        new Card { Question = "Comment se nomment les maîtres de la torture Drukhari ?", Answer = "Les Haemonculi" },
        new Card { Question = "Quelle unité Drukhari est spécialisée dans le tir mobile ?", Answer = "Les Scourges" },
        new Card { Question = "Quelle unité représente l’élite de mêlée Drukhari ?", Answer = "Les Incubi" },
        new Card { Question = "Quel transport léger privilégie la vitesse ?", Answer = "Le Venom" },
        new Card { Question = "Quel est l’objectif principal des Drukhari ?", Answer = "Capturer des esclaves pour prolonger leur vie" },
        new Card { Question = "Quel dieu du Chaos menace directement les Drukhari ?", Answer = "Slaanesh" },
        new Card { Question = "Comment se nomme leur technologie basée sur la souffrance ?", Answer = "Les Pain Engines" },
        new Card { Question = "Quel monstre mécanique est créé par les Haemonculi ?", Answer = "Le Talos" },
        new Card { Question = "Quelle unité utilise des armes empoisonnées ?", Answer = "Les Kabalites Warriors" },
        new Card { Question = "Quelle unité excelle dans la mêlée rapide ?", Answer = "Les Wyches" },
        new Card { Question = "Quel est leur style de guerre favori ?", Answer = "Raids éclairs et piraterie" },
        new Card { Question = "Quelle gladiatrice est une figure légendaire ?", Answer = "Lelith Hesperax" },
        new Card { Question = "Quel moyen permet aux Drukhari de se déplacer entre les raids ?", Answer = "Les portails du Warp" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Astra Militarum – Armageddon",
            Cards =
    {
        new Card { Question = "Quel monde est le théâtre des célèbres guerres d’Armageddon ?", Answer = "Armageddon" },
        new Card { Question = "Quel ennemi majeur y a lancé une immense Waaagh! ?", Answer = "Les Orks de Ghazghkull" },
        new Card { Question = "Quel commissaire est devenu une légende sur Armageddon ?", Answer = "Commissaire Yarrick" },
        new Card { Question = "Quel régiment est spécialisé dans la guerre mécanisée ?", Answer = "Steel Legion" },
        new Card { Question = "Quel char est le pilier des blindés impériaux ?", Answer = "Le Leman Russ" },
        new Card { Question = "Quel transport blindé soutient l’infanterie ?", Answer = "Le Chimera" },
        new Card { Question = "Quelle artillerie lourde bombarde à longue distance ?", Answer = "Le Basilisk" },
        new Card { Question = "Quel super-lourd symbolise la puissance impériale ?", Answer = "Le Baneblade" },
        new Card { Question = "Quelle unité représente l’élite vétéran ?", Answer = "Les Veterans" },
        new Card { Question = "Quelle unité est composée de soldats peu entraînés ?", Answer = "Les Conscripts" },
        new Card { Question = "Quel est leur rôle principal sur Armageddon ?", Answer = "Tenir la ligne contre les Orks" },
        new Card { Question = "Quel commandant impérial est associé aux doctrines ?", Answer = "Général Kurov" },
        new Card { Question = "Quelle arme est standard chez les Gardes Impériaux ?", Answer = "Le Lasgun" },
        new Card { Question = "Quelle doctrine de combat utilise la Steel Legion ?", Answer = "Infanterie mécanisée" },
        new Card { Question = "Quelle unité est dédiée à l’anti-char ?", Answer = "Les équipes d’armes lourdes" },
        new Card { Question = "Quelle unité est utilisée pour la reconnaissance ?", Answer = "Les Sentinelles" },
        new Card { Question = "Quel fut leur rôle durant la Troisième Guerre d’Armageddon ?", Answer = "Résister à la Waaagh! de Ghazghkull" },
        new Card { Question = "Quel appareil fournit le soutien aérien ?", Answer = "Le Valkyrie" },
        new Card { Question = "Quelle unité coordonne les ordres sur le terrain ?", Answer = "Company Command Squad" },
        new Card { Question = "Quelle artillerie est capable de frappes multiples ?", Answer = "Le Manticore" },
        new Card { Question = "Quel char est spécialisé dans le nettoyage au lance-flammes ?", Answer = "Le Hellhound" },
        new Card { Question = "Quelle unité fournit un tir statique longue portée ?", Answer = "Les Heavy Weapon Teams" },
        new Card { Question = "Quel est leur rôle global dans l’Imperium ?", Answer = "Défendre les mondes industriels" },
        new Card { Question = "Quel est leur ennemi le plus fréquent ?", Answer = "Les Orks" },
        new Card { Question = "Quel symbole représente l’Astra Militarum ?", Answer = "Le crâne ailé de l’Imperium" },
        new Card { Question = "Quel style de guerre privilégient-ils ?", Answer = "Guerre mécanisée et artillerie" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Space Marines du Chaos – Légions Renégates",
            Cards =
    {
        new Card { Question = "Quel Primarque est à l’origine des Black Legion ?", Answer = "Horus" },
        new Card { Question = "Qui commande aujourd’hui les Black Legion ?", Answer = "Abaddon le Fléau" },
        new Card { Question = "Quel dieu du Chaos est vénéré par les World Eaters ?", Answer = "Khorne" },
        new Card { Question = "Quel dieu est adoré par la Death Guard ?", Answer = "Nurgle" },
        new Card { Question = "Quel dieu est servi par les Thousand Sons ?", Answer = "Tzeentch" },
        new Card { Question = "Quel dieu est suivi par les Emperor’s Children ?", Answer = "Slaanesh" },
        new Card { Question = "Quel est le rôle principal des Cultistes du Chaos ?", Answer = "Chair à canon et soutien" },
        new Card { Question = "Quel démon majeur est associé à Khorne ?", Answer = "Le Buveur de Sang (Khorne)" },
        new Card { Question = "Quel transport blindé utilisent les Space Marines du Chaos ?", Answer = "Le Rhino du Chaos" },
        new Card { Question = "Quel dreadnought est possédé par des démons ?", Answer = "Le Helbrute" },
        new Card { Question = "Quel char est utilisé pour le soutien lourd ?", Answer = "Le Predator du Chaos" },
        new Card { Question = "Quelle unité fournit le tir lourd ?", Answer = "Les Havocs" },
        new Card { Question = "Quelle unité est spécialisée dans la mêlée brutale ?", Answer = "Les Possédés" },
        new Card { Question = "Quel est leur objectif principal dans la galaxie ?", Answer = "Détruire l’Imperium" },
        new Card { Question = "Quelle machine démoniaque marche sur plusieurs pattes ?", Answer = "Le Defiler" },
        new Card { Question = "Quelle unité d’assaut utilise des réacteurs dorsaux ?", Answer = "Les Raptors du Chaos" },
        new Card { Question = "Quelle unité lourde en armure est redoutable en combat ?", Answer = "Les Terminators du Chaos" },
        new Card { Question = "Quel sorcier légendaire sert Tzeentch ?", Answer = "Ahriman" },
        new Card { Question = "Quel royaume du Warp abrite les forces du Chaos ?", Answer = "Le Maelstrom ou l’Œil de la Terreur" },
        new Card { Question = "Quel fut leur rôle durant l’Hérésie d’Horus ?", Answer = "Trahir l’Imperium" },
        new Card { Question = "Quel est leur but ultime ?", Answer = "Renverser l’Empereur" }
    }
        });
        Decks.Add(new Deck
        {
            Title = "Chevaliers Impériaux – Titans Nobles",
            Cards =
    {
        new Card { Question = "Comment se nomment les gigantesques machines de guerre pilotées ?", Answer = "Les Imperial Knights" },
        new Card { Question = "Quel est le plus grand modèle de Chevalier Impérial ?", Answer = "Le Knight Castellan" },
        new Card { Question = "Quel modèle est spécialisé dans la mêlée ?", Answer = "Le Knight Gallant" },
        new Card { Question = "Quel modèle est équilibré entre tir et mêlée ?", Answer = "Le Knight Errant" },
        new Card { Question = "Quel modèle est dédié au tir lourd à distance ?", Answer = "Le Knight Crusader" },
        new Card { Question = "Quel code d’honneur guident les Chevaliers Impériaux ?", Answer = "Le Code Chivalric" },
        new Card { Question = "Quel est leur rôle principal au sein de l’Imperium ?", Answer = "Soutien lourd et destruction de fortifications" },
        new Card { Question = "D’où proviennent généralement les Chevaliers Impériaux ?", Answer = "Un Knight World féodal" },
        new Card { Question = "Quels ennemis leur sont les plus équivalents ?", Answer = "Les Titans du Chaos" }
    }
        });
    }
}
