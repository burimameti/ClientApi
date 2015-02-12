using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System.Text;
//using IntegrationTests.Builder.Entries;
//using IntegrationTests.Builder.Expressions;
//using NUnit.Framework;
//using Representations.Cofamilies;
//using Rob.Core;
//using Rob.Core.Extensions;
//using Services.Client.Cofamilies;

namespace Cofamilies.Client.FluentApi.Context
{
  public class Context : IContext
  {
#if refactor
    // Constructors

    #region Context()
    public Context()
    {
      ExpressionStack = new RobStack<IExpression>();
      Services = new CofamilyServices(Globals.BaseUri);
      SymbolTable = new SymbolTable();
    } 
    #endregion

    // Properties

    private readonly Dictionary<Type, Type> BindingMap = new Dictionary<Type, Type>();
    public RobStack<IExpression> ExpressionStack { get; private set; }

    public CofamilyServices Services { get; private set; }
    public ISymbolTable SymbolTable { get; private set; }

    // Methods

    #region AddBinding<R, E>()
    public void AddBinding<R, E>()
      where R : IRestRepresentation
      where E : IEntry<R>
    {
      BindingMap.Add(typeof(R), typeof(E));
    } 
    #endregion

    #region GetCurrent<T>(int n = -1)
    /// <summary>
    /// Gets the current entry of type T
    /// </summary>
    /// <typeparam name="T">Type of entry</typeparam>
    /// <returns>The entry or null if not found</returns>
    public T GetCurrent<T>(int n = -1) where T : class, IEntry
    {
      return SymbolTable.GetEntry<T>(n);
    }
    #endregion

    #region GetCurrentRequired<T>(int n = -1)
    /// <summary>
    /// Gets the current entry of type T.  Throws exception if not found.
    /// </summary>
    /// <typeparam name="T">Type of entry</typeparam>
    /// <returns>The entry</returns>
    public T GetCurrentRequired<T>(int n = -1) where T : class, IEntry
    {
      var result = GetCurrent<T>(n);
      Assert.IsNotNull(result, string.Format("Current entry for type {0} not found", typeof(T).Name));
      return result;
    } 
    #endregion

    #region GetEntry<T>(string label = null)
    /// <summary>
    /// Finds the entry for label.  If label is null, the current
    /// entry is returned.
    /// </summary>
    /// <typeparam name="T">The type of the entry</typeparam>
    /// <param name="label">The label or null for the current entry</param>
    /// <returns>The entry or null if not found</returns>
    public T GetEntry<T>(string label = null) where T : class, IEntry
    {
      return SymbolTable.GetEntry<T>(label);
    }
    #endregion

    #region GetEntryRequired<T>(string label = null) where T : class, IEntry
    public T GetEntryRequired<T>(string label = null) where T : class, IEntry
    {
      var result = GetEntry<T>(label);

      var stype = typeof(T).Name;
      var slabel = label ?? "(null)";
      Assert.IsNotNull(result, string.Format("Entry not found for type: {0}, label: {1}", stype, slabel));

      return result;
    } 
    #endregion

    #region GetEntryRequired(params Type[] types)
    /// <summary>
    /// Returns the most recent entry assignable from any of the specified types
    /// </summary>
    public IEntry GetEntryRequired(params Type[] types)
    {
      var result = SymbolTable.GetEntry(types);

      var stypes = types.Select(x => x.ToString()).ToList().ToString(", ");
      Assert.IsNotNull(result, string.Format("Entry not found for types: {0}", stypes));

      return result;
    }
    #endregion

    #region GetMostRecentEntryWithLinkRelationshipRequired(string rel)
    /// <summary>
    /// Returns the most recent entry assignable with a link of the specified relationship
    /// </summary>
    public IEntry GetMostRecentEntryWithRequiredLinkRelationship(string rel)
    {
      var result = SymbolTable.GetMostRecentEntryWithLinkRelationship(rel);

      Assert.IsNotNull(result, string.Format("Entry not found for link relationship: {0}", rel));

      return result;
    }
    #endregion

    #region RemoveCurrent<T>(int n = -1)
    /// <summary>
    /// Removes the current entry of type T
    /// </summary>
    /// <typeparam name="T">Type of entry</typeparam>
    /// <returns>The entry or null if not found</returns>
    public void RemoveCurrent<T>(int n = -1) where T : class, IEntry
    {
      SymbolTable.RemoveEntry<T>(n);
    }
    #endregion

    #region Save(IEntry entry)
    public void Save(IEntry entry)
    {
      SymbolTable.MakeCurrent(entry);
    } 
    #endregion

    public void SaveAs<T>(string label, T entry) where T : class, IEntry
    {
      SymbolTable.SaveAs(label, entry);
    }

    public IEntry ToEntry(IRestRepresentation representation)
    {
      var reptype = representation.GetType();
      if (!BindingMap.ContainsKey(reptype))
        throw new KeyNotFoundException("No binding from a representation to an entry was found for type " + reptype.GetFriendlyTypeName());
      
      var entryType = BindingMap[reptype];
      return ReflectionUtility.Construct(entryType, new object[] {this, representation}) as IEntry;
    }

    public void Touch<T>(string label) where T : class, IEntry
    {
      SymbolTable.MakeCurrent<T>(label);
    }

    public void Touch(IEntry entry)
    {
      SymbolTable.MakeCurrent(entry);
    }
#endif
  }
}
