using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Doc.Net.Lyn
{
    public class Namespace
    {
        private readonly INamespaceSymbol symbolNamespace;
        private readonly IList<Namespace> namespaces;
        private readonly IList<TypeMember> typeMembers;
        public Namespace(INamespaceSymbol symbolNamespace)
        {
            this.symbolNamespace = symbolNamespace;
            this.namespaces = new List<Namespace>(this.symbolNamespace.GetNamespaceMembers().Select(z => new Namespace(z)));
            this.typeMembers = new List<TypeMember>(this.symbolNamespace.GetTypeMembers().Select(z => new TypeMember(z)));
        }
    }
}
