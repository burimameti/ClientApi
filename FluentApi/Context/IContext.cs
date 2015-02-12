using System;
using System.Collections.Generic;

namespace Cofamilies.Client.FluentApi.Context
{
  public interface IContext
  {
#if refactoring
    // Properties

    RobStack<IExpression> ExpressionStack { get; }

    CofamilyServices Services { get; }
    ISymbolTable SymbolTable { get; }
    //Stack<IEntry> Stack { get; }

    // Methods

    T GetCurrent<T>(int n = -1) where T : class, IEntry;
    T GetCurrentRequired<T>(int n = -1) where T : class, IEntry;
    T GetEntry<T>(string label = null) where T : class, IEntry;
    T GetEntryRequired<T>(string label = null) where T : class, IEntry;
    IEntry GetEntryRequired(params Type[] types);
    IEntry GetMostRecentEntryWithRequiredLinkRelationship(string link);
    void Save(IEntry entry);
    void SaveAs<T>(string label, T entry) where T : class, IEntry;
    void Touch(IEntry entry);
    void Touch<T>(string label) where T : class, IEntry;

    IEntry ToEntry(IRestRepresentation representation);
    
    // Methods

    void AddBinding<R, E>()
      where R : IRestRepresentation
      where E : IEntry<R>;
  #endif
  }
}
