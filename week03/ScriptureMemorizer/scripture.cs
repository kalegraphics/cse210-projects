using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        foreach (var word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords()
    {
        int count = 3; // number of words to hide per round
        int hidden = 0;

        while (hidden < count && _words.Exists(w => !w.IsHidden()))
        {
            int index = _random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hidden++;
            }
        }
    }

    public string GetDisplayText()
    {
        string text = _reference.GetDisplayText() + " ";

        foreach (var word in _words)
        {
            text += word.GetDisplayText() + " ";
        }

        return text.Trim();
    }

    public bool AllWordsHidden()
    {
        return _words.TrueForAll(w => w.IsHidden());
    }
}
