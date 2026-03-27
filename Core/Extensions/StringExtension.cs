namespace Qui.Core.Extensions;

public static class StringExtension
{
    public static string ReplaceFirst(this string text, string searchFragment, string toReplaceFragmet)
    {
        int pos = text.IndexOf(searchFragment);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + toReplaceFragmet + text.Substring(pos + searchFragment.Length);
    }
}
