using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
//using IntegrationTests.Builder.Entries;
//using Representations.Cofamilies;

namespace Cofamilies.Client.FluentApi.Context
{
  public class SymbolTable : ISymbolTable
  {
#if refactor
    // Constants

    public const string CurrentLabelKey = "$^#__current__$#@";
    public const string FeedPrefixKey = "$#&__feed__$#@";

    // Properties

    readonly Dictionary<string, IEntry> EntryTable = new Dictionary<string, IEntry>();
    readonly Dictionary<Type, object> FeedTable = new Dictionary<Type, object>();

    // An ordered list of all entries.  The most "current" entry is at the end.
    private readonly List<IEntry> Entries = new List<IEntry>();

    // Methods

    #region CurrentFeed<T>()
    public FeedEntry<T> CurrentFeed<T>() where T : class, IRestRepresentation
    {
      return GetFeed<T>();
    } 
    #endregion

    #region GetEntry<T>(int n)
    public T GetEntry<T>(int n) where T : class, IEntry
    {
      if (n < 0) n = -n;
      if (n == 0) n = 1;

      for (int i = Entries.Count - 1; i >= 0; i--)
      {
        if (!(Entries[i] is T))
          continue;

        if (--n != 0)
          continue;

        return Entries[i] as T;
      }
      return null;
    }
    #endregion

    #region GetEntry<T>(string label = null)
    /// <summary>
    /// If the label is null, the most recent entry of type T
    /// is returned.
    /// 
    /// If the label is specified, a symbol table lookup is
    /// returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    public T GetEntry<T>(string label = null) where T : class, IEntry
    {
      if (label == null)
        return Entries.LastOrDefault(x => x is T) as T;

      IEntry result;
      if (EntryTable.TryGetValue(label, out result))
        return result as T;

      return null;
    } 
    #endregion

    public IEntry GetMostRecentEntryWithLinkRelationship(string rel)
    {
      return Entries.LastOrDefault(x => x.HasRelationship(rel));
    }

    #region Entry<T>(T entry)
    public void Entry<T>(T entry) where T : class, IEntry
    {
      SaveAs(null, entry);
    } 
    #endregion

    #region SaveAs<T>(string label, T entry)
    /// <summary>
    /// Saves a new entry, making it current.
    /// 
    /// If a label is specified, then the entry is added
    /// or updated in the symbol table.
    /// </summary>
    /// <typeparam name="T">The entry type</typeparam>
    /// <param name="label">The symbol table key</param>
    /// <param name="entry">The entry</param>
    public void SaveAs<T>(string label, T entry) where T : class, IEntry
    {
      // Current

      MakeCurrent(entry);

      if (label == null || label == CurrentLabelKey)
        return;

      // Specific Label

      if (EntryTable.ContainsKey(label))
        EntryTable.Remove(label);

      EntryTable.Add(label, entry);
    } 
    #endregion

    #region GetCurrent<T>()
    public T GetCurrent<T>() where T : class, IEntry
    {
      return Entries.LastOrDefault(x => x is T) as T;
    }
    #endregion

    #region GetEntry(params Type[] types)
    public IEntry GetEntry(params Type[] types)
    {
      foreach (var entry in Entries)
      {
        var etype = entry.GetType();
        if (types.Any(etype.IsAssignableFrom))
          return entry;
      }
      return default(IEntry);
    } 
    #endregion

    #region GetFeedTable<T>() where T : IDriverEntry
    private Dictionary<string, FeedEntry<T>> GetFeedTable<T>() where T : class, IRestRepresentation
    {
      var key = typeof(T);
      object result = null;
      if (FeedTable.TryGetValue(key, out result))
        return result as Dictionary<string, FeedEntry<T>>;

      result = new Dictionary<string, FeedEntry<T>>();
      FeedTable.Add(key, result);
      return result as Dictionary<string, FeedEntry<T>>;
    }
    #endregion

    #region GetFeed<T>(string label = null)
    public FeedEntry<T> GetFeed<T>(string label = null) where T : class, IRestRepresentation
    {
      return GetEntry<FeedEntry<T>>(label);
    } 
    #endregion

    #region Feed<T>(string label, FeedEntry<T> feed)
    public void Feed<T>(string label, FeedEntry<T> feed) where T : class, IRestRepresentation
    {
      SaveAs(label, feed);
    } 
    #endregion

    #region MakeCurrent<T>(string label)
    /// <summary>
    /// Finds the entry of type T, specified by label, current for T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    public void MakeCurrent<T>(string label) where T : class, IEntry
    {
      var entry = GetEntry<T>(label);
      if (entry != null)
        MakeCurrent(entry);
    } 
    #endregion

    #region MakeCurrent<T>(T entry)
    public void MakeCurrent<T>(T entry) where T : class, IEntry
    {
      Entries.RemoveAll(x => x == entry);
      Entries.Add(entry);
    }
    #endregion

    #region RemoveEntry<T>(int n)
    public void RemoveEntry<T>(int n) where T : class, IEntry
    {
      if (n < 0) n = -n;
      if (n == 0) n = 1;

      for (int i = Entries.Count - 1; i >= 0; i--)
      {
        if (!(Entries[i] is T))
          continue;

        if (--n != 0)
          continue;

        Entries.RemoveAt(i);
      }
    }
    #endregion
#endif
  }
}
