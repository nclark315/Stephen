using System.Collections.Generic;

public static class WordValidator
{
    private static HashSet<string> validWords = new HashSet<string> { "CAT", "DOG", "RAT", "HAT" };

    public static bool IsCorrectWord(string word)
    {
        return validWords.Contains(word.ToUpper());
    }
}
