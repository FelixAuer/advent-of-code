namespace Console.Days;

public class Day07 : IDay
{
    private static int FIVE_OF_A_KIND = 7;
    private static int FOUR_OF_A_KIND = 6;
    private static int FULL_HOUSE = 5;
    private static int THREE_OF_A_KIND = 4;
    private static int TWO_PAIRS = 3;
    private static int ONE_PAIR = 2;
    private static int HIGH_CARD = 1;

    public void Solve()
    {
        var input = AoCHelper.ReadLines("07");
        var hands = new List<(string cards, int bid)>();

        foreach (var line in input)
        {
            var vals = line.Split(" ");
            hands.Add((cards: vals[0], bid: int.Parse(vals[1])));
        }

        hands.Sort(new HandComparer());
        var sum = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            sum += (i + 1) * hands[i].bid;
        }

        System.Console.WriteLine(sum);


        hands.Sort(new HandComparer2());
        var sum2 = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            sum2 += (i + 1) * hands[i].bid;
        }

        System.Console.WriteLine(sum2);
    }

    public static int GetHandType((string cards, int bid) hand)
    {
        var count = new Dictionary<char, int>();
        foreach (var card in hand.cards.ToCharArray())
        {
            count.TryAdd(card, 0);
            count[card]++;
        }

        var occs = count.OrderByDescending(pair => pair.Value).Select(pair => pair.Value).ToArray();
        if (occs[0] == 5)
        {
            return FIVE_OF_A_KIND;
        }

        if (occs[0] == 4)
        {
            return FOUR_OF_A_KIND;
        }

        if (occs[0] == 3)
        {
            if (occs[1] == 2)
            {
                return FULL_HOUSE;
            }

            return THREE_OF_A_KIND;
        }

        if (occs[0] == 2)
        {
            if (occs[1] == 2)
            {
                return TWO_PAIRS;
            }

            return ONE_PAIR;
        }

        return HIGH_CARD;
    }

    public static int GetBestHandType((string cards, int bid) hand)
    {
        var max = 0;
        foreach (var card in "23456789TQKA".ToCharArray())
        {
            var newHand = hand with { cards = hand.cards.Replace('J', card) };
            var val = GetHandType(newHand);
            max = Math.Max(max, val);
        }

        return max;
    }


    private class HandComparer : IComparer<(string cards, int bid)>
    {
        private string cardOrder = "23456789TJQKA";

        public int Compare((string cards, int bid) hand1, (string cards, int bid) hand2)
        {
            var type1 = GetHandType(hand1);
            var type2 = GetHandType(hand2);
            var typeComparison = Comparer<int>.Default.Compare(type1, type2);
            if (typeComparison != 0) return typeComparison;

            for (var i = 0; i < hand1.cards.Length; i++)
            {
                var index1 = cardOrder.IndexOf(hand1.cards[i]);
                var index2 = cardOrder.IndexOf(hand2.cards[i]);
                var cardComparison = Comparer<int>.Default.Compare(index1, index2);
                if (cardComparison != 0) return cardComparison;
            }

            return 0;
        }
    }

    private class HandComparer2 : IComparer<(string cards, int bid)>
    {
        private string cardOrder = "J23456789TQKA";

        public int Compare((string cards, int bid) hand1, (string cards, int bid) hand2)
        {
            var type1 = GetBestHandType(hand1);
            var type2 = GetBestHandType(hand2);
            var typeComparison = Comparer<int>.Default.Compare(type1, type2);
            if (typeComparison != 0) return typeComparison;

            for (var i = 0; i < hand1.cards.Length; i++)
            {
                var index1 = cardOrder.IndexOf(hand1.cards[i]);
                var index2 = cardOrder.IndexOf(hand2.cards[i]);
                var cardComparison = Comparer<int>.Default.Compare(index1, index2);
                if (cardComparison != 0) return cardComparison;
            }

            return 0;
        }
    }
}